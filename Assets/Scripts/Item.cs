using UnityEngine;

[System.Serializable]
public class Item {
    public string name;
    public string description;
    public string icon;
    public string rarity;
    public bool stackable = true;
    public ItemModifier function;

    public static Item CreateFromJSON(string jsonString) {
        return JsonUtility.FromJson<Item>(jsonString);
    }
}
