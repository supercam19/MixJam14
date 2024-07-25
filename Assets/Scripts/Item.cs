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

    public Sprite LoadIcon() {
        return Resources.Load<Sprite>("Textures/Icons/" + icon);
    }
    
    public Color32 GetRarityColor() {
        switch (rarity) {
            case "common":
                return new Color32(255, 255, 255, 255);
            case "rare":
                return new Color32(5, 184, 11, 255);
            case "legendary":
                return new Color32(242, 238, 0, 255);
            default:
                return new Color32(255, 255, 255, 255);
        }
    }
}
