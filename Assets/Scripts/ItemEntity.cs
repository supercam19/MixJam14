using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemEntity : MonoBehaviour {
    public Item item;
    private SpriteRenderer sr;
    private GameObject tooltip;
    private bool markedForDestroy = false;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        tooltip = GameObject.Find("Tooltip");
    }

    void Update() {
        transform.position = new Vector3(transform.position.x,
            transform.position.y + 0.5f * Mathf.Sin(Time.time / 500),
            transform.position.z);
        if (markedForDestroy) Destroy(gameObject);
    }

    public Item Collect() {
        markedForDestroy = true;
        return item;
    }

    public void SetItem(Item item) {
        this.item = item;
        sr.sprite = item.LoadIcon();
    }
    
    public void DrawInteractable() {
        InteractableTip.DrawTip("Collect", new Vector3(transform.position.x + transform.position.y + 1, transform.position.z));
    }

    public void DrawTooltip() {
        tooltip.SetActive(true);
        tooltip.transform.GetChild(0).GetComponent<Image>().sprite = item.LoadIcon();
        Text title = tooltip.transform.GetChild(1).GetComponent<Text>();
        title.text = item.name;
        title.color = item.GetRarityColor();
        tooltip.transform.GetChild(2).GetComponent<Text>().text = item.description;
    }
    
    public void HideTooltip() {
        tooltip.SetActive(false);
    }
}
