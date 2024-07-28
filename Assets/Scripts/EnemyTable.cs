using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyTable {
    public float smallGunnerDrone = 0.9f;
    public float bomberDrone = 0.1f;

    public string GetRandomEnemyWeighed() {
        float rng = Random.value;
        if (rng < smallGunnerDrone) return "SmallGunnerDrone";
        if (rng < smallGunnerDrone + bomberDrone) return "BomberDrone";
        return "SmallGunnerDrone";
    }

    public static EnemyTable LoadFromJSON(string jsonString) {
        return JsonUtility.FromJson<EnemyTable>(jsonString);
    }
}
