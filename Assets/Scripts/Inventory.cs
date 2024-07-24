using System.Reflection;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public List<Item> items = new List<Item>();

    private PlayerStats stats;
    
    void Start() {
        stats = GetComponent<PlayerStats>();
    }

    public void AddItem(Item item) {
        if (!item.stackable && items.Contains(item)) {
            
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
