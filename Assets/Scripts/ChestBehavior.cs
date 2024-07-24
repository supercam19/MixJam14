using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehavior : MonoBehaviour {
    private SpriteRenderer sr;
    private Collider col;
    private bool opened = false;

    private static bool initialized = false;
    private static Sprite woodChest;
    private static Sprite openedWoodChest;
    private static Sprite goldChest;
    private static Sprite openedGoldChest;

    void Start() {
        if (!initialized) {
            Sprite[] sprites = Resources.LoadAll<Sprite>("Textures/textures");
            woodChest = sprites[15];
            openedWoodChest = sprites[16];
            goldChest = sprites[7];
            openedGoldChest = sprites[8];
        }
    }
}
