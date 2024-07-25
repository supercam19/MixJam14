using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyTable {
    private float smallGunnerDrone;

    public float[] enemyChances;

    void Start() {
        enemyChances = new float[1] {
            smallGunnerDrone
        };
    }

    public static EnemyTable LoadFromJSON(string jsonString) {
        return JsonUtility.FromJson<EnemyTable>(jsonString);
    }
}
