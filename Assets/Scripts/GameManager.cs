using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static bool paused = false;
    public static int level = 1;
    public static int money = 0;

    private GameObject pauseMenu;
    private BinaryPartitionDungeon generator;
    private GameObject player;
    private static Text hudMoney;
    private static Text hudFloor;

    public static List<EnemyBehavior> activeEnemies = new List<EnemyBehavior>();
    public static List<ChestBehavior> activeChests = new List<ChestBehavior>();
    

    void Start() {
        pauseMenu = GameObject.Find("PauseMenu");
        generator = GameObject.Find("DungeonGenerator").GetComponent<BinaryPartitionDungeon>();
        player = GameObject.Find("Player");
        hudMoney = GameObject.Find("HUDMoneyText").GetComponent<Text>();
        hudFloor = GameObject.Find("HUDFloorText").GetComponent<Text>();
        generator.Generate();
        InteractableTip.Initialize();
        player.transform.position = new Vector3(generator.randomSpawnPoint.x, generator.randomSpawnPoint.y, 0);
        SetBalance(1000);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            paused = !paused;
            pauseMenu.SetActive(paused);
        }
    }

    public static void LevelCleanup() {
        for (int i = 0; i < activeEnemies.Count; i++) {
            Destroy(activeEnemies[i].gameObject);
        }
        activeEnemies.Clear();
        foreach (ChestBehavior chest in activeChests) {
            Destroy(chest.gameObject);
        }
        activeChests.Clear();
    }

    public static void SetBalance(int balance) {
        money = balance;
        hudMoney.text = "$" + money;
    }

    public void NextFloor() {
        level++;
        hudFloor.text = "Floor " + level;
        generator.Generate();
    }
}
