using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemEntity : InteractableObject {
    public Item item;
    private SpriteRenderer sr;
    private GameObject tooltip;
    private bool markedForDestroy = false;
    private Inventory inv;
    private Sprite icon;
    private Vector3 startPos;
    private int frames;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        tooltip = GameObject.Find("Tooltip");
        inv = GameObject.Find("Player").GetComponent<Inventory>();
        sr.sprite = icon;
        startPos = transform.position;
    }

    void Update() {
        transform.position = startPos + new Vector3(0, 0.25f * Mathf.Sin((float)frames / 300), 0);
        if (markedForDestroy) Destroy(gameObject);
        frames++;
    }

    public override void Interact() {
        markedForDestroy = true;
        inv.AddItem(item);
    }

    public void SetItem(Item item) {
        this.item = item;
        icon = item.LoadIcon();
    }
    
    public override void DrawTip() {
        InteractableTip.DrawTip("Collect", startPos);
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
