using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRoller : MonoBehaviour {
    private const int COMMON = 0;
    private const int RARE = 1;
    private const int LEGENDARY = 2;

    private static LootTable[] lootTables = new LootTable[3];
    private static string[] lootTableResources = { "wood_chest", "gold_chest", "challenge_chest" };

    private static Item[] allItems;
    private static List<Item> commonItems;
    private static List<Item> rareItems;
    private static List<Item> legendaryItems;
    private static List<Item>[] itemsByRarity = { commonItems, rareItems, legendaryItems };

    public static Item RollItem(int chestType) {
        int rarity = lootTables[chestType].RollRarity();
        return itemsByRarity[rarity][Random.Range(0, itemsByRarity[rarity].Count)];
    }

    public static Item ReRoll(int rarity) {
        return itemsByRarity[rarity][Random.Range(0, itemsByRarity[rarity].Count)];
    }

    void Start() {
        for (int i = 0; i < 3; i++) {
            lootTables[i] = LootTable.CreateFromJSON(Resources.Load<TextAsset>("Data/LootTables/" + lootTableResources[i]).ToString());
        }
        TextAsset[] allItemJSON = Resources.LoadAll<TextAsset>("Data/Items");
        allItems = new Item[allItemJSON.Length];
        for (int i = 0; i < allItemJSON.Length; i++) {
            allItems[i] = Item.CreateFromJSON(allItemJSON[i].ToString());
        }
        commonItems = new List<Item>();
        rareItems = new List<Item>();
        legendaryItems = new List<Item>();
        foreach (Item item in allItems) {
            if (item.rarity.Equals("common")) {
                commonItems.Add(item);
            }
            else if (item.rarity.Equals("rare")) {
                rareItems.Add(item);
            }
            else if (item.rarity.Equals("legendary")) {
                legendaryItems.Add(item);
            }
        }
    }
}
