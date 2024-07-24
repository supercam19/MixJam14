using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static bool paused = false;

    private GameObject pauseMenu;
    private BinaryPartitionDungeon generator;
    private GameObject player;

    void Start() {
        pauseMenu = GameObject.Find("PauseMenu");
        generator = GameObject.Find("DungeonGenerator").GetComponent<BinaryPartitionDungeon>();
        player = GameObject.Find("Player");

        generator.Generate();
        player.transform.position = new Vector3(generator.randomSpawnPoint.x, generator.randomSpawnPoint.y, 0);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            paused = !paused;
            pauseMenu.SetActive(paused);
        }
    }
}
