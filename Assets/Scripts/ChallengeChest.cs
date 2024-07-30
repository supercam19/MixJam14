using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChallengeChest : InteractableObject {
    private BoundsInt spawnableRoomArea;
    private List<Vector3> unavailableSpawns = new List<Vector3>();
    private List<GameObject> enemies = new List<GameObject>();
    private SpriteRenderer sr;
    private bool opened;
    private bool completed;

    public void Setup(BoundsInt spawnableArea) {
        spawnableRoomArea = spawnableArea;
    }
    
    public override void Interact() {
        EnemyTable enemyTable = EnemyTable.LoadFromJSON(Resources.Load<TextAsset>("Data/LootTables/enemy_spawns").ToString());
        int enemyCount = Random.Range(6, 10);
        for (int i = 0; i < enemyCount; i++) {
            int x = 0;
            Vector3 spawnPos = new Vector3Int(Random.Range(spawnableRoomArea.x, spawnableRoomArea.xMax),
                Random.Range(spawnableRoomArea.y, spawnableRoomArea.yMax), 0);
            while (unavailableSpawns.Contains(spawnPos) && Vector2.Distance(new Vector2(spawnPos.x, spawnPos.y), transform.position) < 2 && x < 1000) {
                spawnPos = new Vector3(Random.Range(spawnableRoomArea.x, spawnableRoomArea.xMax), Random.Range(spawnableRoomArea.y, spawnableRoomArea.yMax), 0);
                x++;
            }

            if (x < 1000) {
                unavailableSpawns.Add(spawnPos);
            }
        }

        foreach (Vector3 pos in unavailableSpawns) {
            enemies.Add(Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/" + enemyTable.GetRandomEnemyWeighed()), pos,
                Quaternion.identity));
            enemies.Last().GetComponent<EnemyBehavior>().SpawnInvinvibility(1.25f);
        }

        sr.sprite = Resources.LoadAll<Sprite>("Textures/textures")[10];
        GetComponent<BoxCollider2D>().enabled = false;
        opened = true;
        SoundManager.Play(gameObject, "phoenix_feather_use");
    }
    
    public override void DrawTip() {
        InteractableTip.DrawTip("Challenge", transform.position + new Vector3(0, 1, 0));
    }

    void Update() {
        if (!opened || completed) return;
        bool allDead = true;
        foreach (GameObject enemy in enemies) {
            if (enemy != null) {
                allDead = false;
                break;
            }
        }

        if (allDead) {
            Reward();
            sr.sprite = Resources.LoadAll<Sprite>("Textures/textures")[13];
        }
    }

    private void Reward() {
        completed = true;
        GameManager.SetBalance(GameManager.money + 50 + GameManager.level * 10);
        Instantiate(Resources.Load<GameObject>("Prefabs/Objects/ItemEntity"), transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity).GetComponent<ItemEntity>().SetItem(ItemRoller.RollItem(2));
    }

    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }
}
