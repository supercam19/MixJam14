using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PopulateDungeon : MonoBehaviour {
    private RoomType[] roomTypes;
    private EnemyTable enemyTypes;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject chestPrefab;
    [SerializeField] private Tilemap tilemap;
    
    
    public void Populate(BoundsInt[] rooms) {
        Start();
        int x = 0;
        foreach (BoundsInt room in rooms) {
            int roomType = SelectRoomType();
            int spawns = (int)Random.Range(roomTypes[roomType].spawnMinimum, roomTypes[roomType].spawnMaximum);
            List<Vector3> occupied = new List<Vector3>();
            for (int i = 0; i < spawns; i++) {
                float rng = Random.value;
                Vector3 spawnpoint;
                while (true) {
                    x++;
                    spawnpoint = PickRandomTile(room);
                    if (!occupied.Contains(spawnpoint)) {
                        occupied.Add(spawnpoint);
                        break;
                    }
                }
                if (rng < roomTypes[roomType].enemyChance) {
                    SpawnRandomEnemy(spawnpoint);
                }
                else if (rng < roomTypes[roomType].woodChestChance + roomTypes[roomType].enemyChance) {
                    ChestBehavior chest = Instantiate(chestPrefab, spawnpoint, Quaternion.identity)
                        .GetComponent<ChestBehavior>();
                    chest.type = ChestBehavior.WOOD;
                    GameManager.activeChests.Add(chest);
                }
                else if (rng < roomTypes[roomType].goldChestChance + roomTypes[roomType].woodChestChance + roomTypes[roomType].enemyChance) {
                    ChestBehavior chest = Instantiate(chestPrefab, spawnpoint, Quaternion.identity)
                        .GetComponent<ChestBehavior>();
                    chest.type = ChestBehavior.GOLD;
                    GameManager.activeChests.Add(chest);
                }
                else {
                    Debug.Log("Implement - Challenge chest");
                }
            }
        }
    }

    void Start() {
        TextAsset[] roomTypesJson = Resources.LoadAll<TextAsset>("Data/Rooms");
        roomTypes = new RoomType[roomTypesJson.Length];
        for (int i = 0; i < roomTypesJson.Length; i++) {
            roomTypes[i] = RoomType.LoadFromJSON(roomTypesJson[i].ToString());
        }
        enemyTypes = EnemyTable.LoadFromJSON(Resources.Load<TextAsset>("Data/LootTables/enemy_spawns").ToString());
        
    }

    private int SelectRoomType() {
        float rng = Random.value;
        float[] chances = new float[roomTypes.Length];
        for (int i = 0; i < roomTypes.Length; i++) {
            chances[i] = roomTypes[i].roomChance;
        }
        for (int i = 0; i < chances.Length; i++) {
            if (rng < chances[i]) {
                return i;
            }
            rng -= chances[i];
        }

        return -1;
    }

    private Vector3 PickRandomTile(BoundsInt area) {
        return tilemap.CellToWorld(new Vector3Int(Random.Range(area.x + 2, area.xMax - 2), Random.Range(area.y + 2, area.yMax - 2), 0)) + new Vector3(0.5f, 0.5f, 0);
    }

    private void SpawnRandomEnemy(Vector3 spawnpoint) {
        GameManager.activeEnemies.Add(Instantiate(enemyPrefab, spawnpoint, Quaternion.identity).GetComponent<EnemyBehavior>());
    }
}
