using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUIItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public static GameObject tooltip;
    public const float SPACING = 62.2f;
    [HideInInspector] public Item item;

    public void Setup(Item item, int inventoryPlace) {
        this.item = item;
        GetComponent<Image>().sprite = item.LoadIcon();
        Text textCount = GetComponentInChildren<Text>();
        textCount.text = item.count.ToString();
        item.inventoryItemCount = textCount;
        transform.position += new Vector3(SPACING * inventoryPlace, 0, 0);
    }
    
    public void OnPointerEnter(PointerEventData eventData) {
        tooltip.SetActive(true);
        tooltip.transform.position = transform.position + new Vector3(0, -50, 0);
        tooltip.transform.GetChild(0).GetComponent<Image>().sprite = GetComponent<Image>().sprite;
        tooltip.transform.GetChild(1).GetComponent<Text>().text = item.name;
        tooltip.transform.GetChild(1).GetComponent<Text>().color = item.GetRarityColor();
        tooltip.transform.GetChild(2).GetComponent<Text>().text = item.description;
    }
    
    public void OnPointerExit(PointerEventData eventData) {
        tooltip.SetActive(false);
    }
}
