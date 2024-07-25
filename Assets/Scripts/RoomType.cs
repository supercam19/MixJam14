using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomType {
    public float roomChance;
    public float spawnMinimum;
    public float spawnMaximum;
    public float enemyChance;
    public float woodChestChance;
    public float goldChestChance;
    public float challengeChance;
    
    public static RoomType LoadFromJSON(string jsonString) {
        return JsonUtility.FromJson<RoomType>(jsonString);
    }
}
