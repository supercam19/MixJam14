using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static bool paused = false;
    public static int level = 1;
    public static int money = 0;
    public static GameManager instance;

    private GameObject pauseMenu;
    private BinaryPartitionDungeon generator;
    private GameObject player;
    private static Text hudMoney;
    private static Text hudFloor;

    public static List<EnemyBehavior> activeEnemies = new List<EnemyBehavior>();
    public static List<GameObject> activeChests = new List<GameObject>();
    
    public static int killsThisFloor = 0;
    public static int killsRequired;
    

    void Start() {
        instance = this;
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
        generator = GameObject.Find("DungeonGenerator").GetComponent<BinaryPartitionDungeon>();
        player = GameObject.Find("Player");
        hudMoney = GameObject.Find("HUDMoneyText").GetComponent<Text>();
        hudFloor = GameObject.Find("HUDFloorText").GetComponent<Text>();
        InventoryUIItem.tooltip = GameObject.Find("Tooltip");
        InventoryUIItem.tooltip.SetActive(false);
        generator.Generate();
        killsRequired = activeEnemies.Count;
        ItemRoller.Initialize();
        InteractableTip.Initialize();
        player.transform.position = new Vector3(generator.randomSpawnPoint.x, generator.randomSpawnPoint.y, 0);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseButton();
        }
    }

    public void PauseButton() {
        paused = !paused;
        Time.timeScale = paused ? 0 : 1;
        pauseMenu.SetActive(paused);
    }

    public void EndGame() {
        Application.Quit();
    }

    public static void LevelCleanup() {
        for (int i = 0; i < activeEnemies.Count; i++) {
            Destroy(activeEnemies[i].gameObject);
        }
        activeEnemies.Clear();
        foreach (GameObject chest in activeChests) {
            Destroy(chest.gameObject);
        }
        activeChests.Clear();
    }

    public static void SetBalance(int balance) {
        money = balance;
        hudMoney.text = "$" + money;
    }

    public void NextFloor() {
        SetFloor(level + 1);
    }

    private void SetFloor(int newFloor) {
        level = newFloor;
        hudFloor.text = "Floor " + level;
        generator.Generate();
        killsRequired = activeEnemies.Count;
        player.transform.position = new Vector3(generator.randomSpawnPoint.x, generator.randomSpawnPoint.y, 0);
    }

    public void RestartGame() {
        SetBalance(0);
        LevelCleanup();
        player.GetComponent<PlayerStats>().Reset();
        player.GetComponent<PlayerAbilities>().Reset();
        Transform uiInventory = GameObject.Find("Inventory").transform;
        for (int i = 0; i < uiInventory.childCount; i++) {
            Destroy(uiInventory.GetChild(i).gameObject);
        }
        player.GetComponent<Inventory>().items.Clear();
        SetFloor(0);
        Time.timeScale = 1;
        paused = false;
        GameObject.Find("GameOver").SetActive(false);
    }
}
