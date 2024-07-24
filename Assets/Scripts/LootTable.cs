using UnityEngine;

[System.Serializable]
public class LootTable {
    public float commonChance;
    public float rareChance;
    public float legendaryChance;
    
    public static LootTable CreateFromJSON(string jsonString) {
        return JsonUtility.FromJson<LootTable>(jsonString);
    }

    public int RollRarity() {
        float roll = Random.Range(0f, 1f);
        if (roll < commonChance) {
            return 0;
        }
        else if (roll < commonChance + rareChance) {
            return 1;
        }
        else {
            return 2;
        }
    }
}
