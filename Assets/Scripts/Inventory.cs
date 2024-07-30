using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public List<Item> items = new List<Item>();

    private PlayerStats stats;
    private GameObject uiItem;
    private GameObject inventoryUI;
    
    void Start() {
        stats = GetComponent<PlayerStats>();
        uiItem = Resources.Load<GameObject>("Prefabs/Objects/InventoryItem");
        inventoryUI = GameObject.Find("Inventory");
    }

    public void AddItem(Item item) {
        if (!item.stackable && items.Contains(item)) {
            AddItem(ItemRoller.ReRoll(item.GetRarityInt()));
        }
        else {
            if (items.Contains(item)) {
                items[items.IndexOf(item)].SetCount(items[items.IndexOf(item)].count + 1);
            }
            else {
                item.SetCount(1);
                items.Add(item);
                Instantiate(uiItem, inventoryUI.transform).GetComponent<InventoryUIItem>().Setup(item, items.Count - 1);
            }
            DoAction(item);
        }
    }
    
    [ContextMenu("RollChallenge")]
    public void RollChallenge() {
        AddItem(ItemRoller.RollItem(2));
    }

    public void RemoveItem(string name, int count) {
        int index = -1;
        for (int i = 0; i < items.Count; i++) {
            if (items[i].name.Equals(name)) {
                index = i;
                break;
            }
        }

        if (index > -1) {
            if (items[index].count > count) {
                items[index].SetCount(items[index].count - count);
            }
            else {
                items.Remove(items[index]);
                for (int i = index; i < inventoryUI.transform.childCount; i++) {
                    inventoryUI.transform.GetChild(i).transform.position = (Vector2)inventoryUI.transform.GetChild(i).transform.position - new Vector2(InventoryUIItem.SPACING, 0);
                }
                Debug.Log("Index of item: " + index);
                Destroy(inventoryUI.transform.GetChild(index).gameObject);
            }
        }
    }

    private void DoAction(Item item) {
        if (item.function == null) {
            Debug.LogWarning("Item " + item.name + " has no function");
            return;
        }
        if (item.function.operation.Equals("set")) {
            stats.GetType().GetProperty(item.function.name).SetValue(stats, item.function.change, null);
        }
        else if (item.function.operation.Equals("add")) {
            stats.GetType().GetProperty(item.function.name).SetValue(stats, (float)stats.GetType().GetProperty(item.function.name).GetValue(stats, null) + item.function.change, null);
        }
        else if (item.function.operation.Equals("multiply")) {
            stats.GetType().GetProperty(item.function.name).SetValue(stats, (float)stats.GetType().GetProperty(item.function.name).GetValue(stats, null) * item.function.change, null);
        }
        else {
            Debug.LogWarning("Invalid operation for function on item " + item.name);
        }
    }
}
