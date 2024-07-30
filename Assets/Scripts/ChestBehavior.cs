using UnityEngine.UI;
using UnityEngine;

public class ChestBehavior : InteractableObject {
    [HideInInspector] public const int WOOD = 0;
    [HideInInspector] public const int GOLD = 1;
    
    private SpriteRenderer sr;
    private BoxCollider2D col;
    private bool opened = false;
    private int price = 0;
    public int type = WOOD;
    private Sprite closedSprite;
    private Sprite openSprite;

    private static bool initialized = false;
    private static Sprite woodChest;
    private static Sprite openedWoodChest;
    private static Sprite goldChest;
    private static Sprite openedGoldChest;
    private static GameObject itemEntityPrefab;

    void Start() {
        if (!initialized) {
            Sprite[] sprites = Resources.LoadAll<Sprite>("Textures/textures");
            woodChest = sprites[15];
            openedWoodChest = sprites[16];
            goldChest = sprites[7];
            openedGoldChest = sprites[8];
            itemEntityPrefab = Resources.Load<GameObject>("Prefabs/Objects/ItemEntity");
            initialized = true;
        }
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        price = SetCost();
        if (type == WOOD) {
            closedSprite = woodChest;
            openSprite = openedWoodChest;
        }
        else {
            closedSprite = goldChest;
            openSprite = openedGoldChest;
        }
        sr.sprite = closedSprite;
    }

    public override void Interact() {
        if (!opened) {
            if (GameManager.money >= price) {
                opened = true;
                GameManager.SetBalance(GameManager.money - price);
                sr.sprite = openSprite;
                Instantiate(itemEntityPrefab,
                    new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z),
                    Quaternion.identity).GetComponent<ItemEntity>().SetItem(ItemRoller.RollItem(type));
                col.enabled = false;
                SoundManager.Play(gameObject, "chest_open", pitchVariance: 0.1f);
            }
            else {
                SoundManager.Play(gameObject, "deny");
            }
        }
    }

    public override void DrawTip() {
        InteractableTip.DrawTip($"Open <color={PriceColor()}>${price}</color>", new Vector3(transform.position.x, transform.position.y + 1, transform.position.z));
    }

    public int GetPrice() {
        if (opened) return -1;
        return price;
    }

    private int SetCost() {
        return (5 * GameManager.level) + (20 * (type + 1));
    }

    private string PriceColor() {
        if (GameManager.money >= price) return "green";
        return "red";
    }
}
