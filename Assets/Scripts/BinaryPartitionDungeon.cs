using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using NavMeshPlus.Components;
using Debug = UnityEngine.Debug;
using UnityEngine.Tilemaps;

public class BinaryPartitionDungeon : MonoBehaviour {
    [SerializeField] private BoundsInt dungeonArea;
    [SerializeField] private TilemapVisualizer tilemapVisualizer;
    [SerializeField] private int minRoomSize;
    [SerializeField] private int maxPhysicalRoomSize;
    [SerializeField] private int minPhysicalRoomSize;
    [SerializeField] private NavMeshSurface navMesh;
    
    private HashSet<Vector2Int> floorPositions;
    [HideInInspector] public Vector2Int randomSpawnPoint;
    
    [ContextMenu("Generate")]
    public void Generate() {
        tilemapVisualizer.Clear();
        GameManager.LevelCleanup();
        GenerateDungeon();
    }

    private void GenerateDungeon() {
        BoundsInt[] rooms = BinarySpacePartitioning(dungeonArea);
        floorPositions = new HashSet<Vector2Int>();
        BoundsInt[] physicalRooms = new BoundsInt[rooms.Length];
        for (int i = 0; i < rooms.Length; i++) {
            physicalRooms[i] = GenerateRoom(rooms[i], floorPositions);
        }
        for (int i = 0; i < physicalRooms.Length; i++) {
            int closestIndex = GetClosestRoomIndex(physicalRooms, i);
            GenerateCorridor(Compress(physicalRooms[i].center), Compress(physicalRooms[closestIndex].center), floorPositions);
        }

        randomSpawnPoint = Compress(physicalRooms[Random.Range(0, physicalRooms.Length)].center);
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        navMesh.BuildNavMesh();
        GetComponent<PopulateDungeon>().Populate(physicalRooms);
    }
    
    private BoundsInt[] BinarySpacePartitioning(BoundsInt room) {
        if (room.size.x <= minRoomSize || room.size.y <= minRoomSize) {
            return new BoundsInt[] { room };
        }
        bool horizontal = Random.value > 0.5f;
        BoundsInt[] newRooms = horizontal ? SplitHorizontal(room) : SplitVertical(room);
        BoundsInt[] left = BinarySpacePartitioning(newRooms[0]);
        BoundsInt[] right = BinarySpacePartitioning(newRooms[1]);
        BoundsInt[] allRooms = new BoundsInt[left.Length + right.Length];
        Array.Copy(left, allRooms, left.Length);
        Array.Copy(right, 0, allRooms, left.Length, right.Length);
        return allRooms;
    }

    private BoundsInt GenerateRoom(BoundsInt room, HashSet<Vector2Int> floor) {
        int width = Random.Range(minPhysicalRoomSize, maxPhysicalRoomSize);
        int height = Random.Range(minPhysicalRoomSize, maxPhysicalRoomSize);
        int xSpace = room.size.x - width;
        int ySpace = room.size.y - height;
        int x = Random.Range(0, xSpace);
        int y = Random.Range(0, ySpace);
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                Vector2Int pos = new Vector2Int(room.x + x + i, room.y + y + j);
                floor.Add(pos);
            }
        }

        return new BoundsInt(new Vector3Int(room.x + x, room.y + y, room.z),
            new Vector3Int(width, height, room.size.z));
    }

    private void GenerateCorridor(Vector2Int start, Vector2Int end, HashSet<Vector2Int> floor) {
        while (start.x != end.x) {
            FillArea(new Vector2Int(1, 4), start, floor);
            start.x += start.x < end.x ? 1 : -1;
        }
        // Filling hallway corners
        FillArea(new Vector2Int(4, 4), start, floor);
        while (start.y != end.y) {
            FillArea(new Vector2Int(4, 1), start, floor);
            start.y += start.y < end.y ? 1 : -1;
        }
    }

    private int GetClosestRoomIndex(BoundsInt[] rooms, int currentIndex) {
        // To simplify, assume that we start search from index 0, so we don't need to consider
        // any rooms before currentIndex
        float minDistance = float.MaxValue;
        int nearestIndex = 0;
        for (int i = currentIndex + 1; i < rooms.Length; i++) {
            BoundsInt room = rooms[i];
            float distance = Vector2.Distance(room.center, rooms[currentIndex].center);
            if (distance < minDistance) {
                minDistance = distance;
                nearestIndex = i;
            }
        }

        return nearestIndex;
    }

    private BoundsInt[] SplitHorizontal(BoundsInt room) {
        int split = Random.Range(minRoomSize, room.size.x - minRoomSize);
        return new BoundsInt[] {new BoundsInt(new Vector3Int(room.x, room.y, room.z), new Vector3Int(split, room.size.y, room.size.z)),
                                new BoundsInt(new Vector3Int(room.x + split, room.y, room.z), new Vector3Int(room.size.x - split, room.size.y, room.size.z))};
    }
    
    private BoundsInt[] SplitVertical(BoundsInt room) {
        int split = Random.Range(minRoomSize, room.size.y - minRoomSize);
        return new BoundsInt[] {new BoundsInt(new Vector3Int(room.x, room.y, room.z), new Vector3Int(room.size.x, split, room.size.z)),
                                new BoundsInt(new Vector3Int(room.x, room.y + split, room.z), new Vector3Int(room.size.x, room.size.y - split, room.size.z))};
    }

    private void FillArea(Vector2Int area, Vector2Int center, HashSet<Vector2Int> floor) {
        Vector2Int cornerPos = new Vector2Int(center.x - area.x / 2, center.y - area.y / 2);
        for (int i = 0; i < area.x; i++) {
            for (int j = 0; j < area.y; j++) {
                floor.Add(new Vector2Int(cornerPos.x + i, cornerPos.y + j));
            }
        }
        
    }

    private Vector2Int Compress(Vector3 a) {
        return new Vector2Int((int)a.x, (int)a.y);
    }
}
