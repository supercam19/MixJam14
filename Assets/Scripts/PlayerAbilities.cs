using UnityEngine;

public class PlayerAbilities : MonoBehaviour {
    [HideInInspector] public Ability leftMouseAbility = new Ability("Magic Bolt", 0.25f, LEFT_MOUSE, null);
    [HideInInspector] public Ability rightMouseAbility = new Ability("Fireball", 3.0f, RIGHT_MOUSE, null);
    [HideInInspector] public Ability rKeyAbility = new Ability("Lightning Strike", 15.0f, R_KEY, null);
    [HideInInspector] public Ability spaceBarAbility = new Ability("Dash", 1.5f, SPACE_BAR, null);

    private PlayerController controller;
    private GameObject magicBoltPrefab;
    private PlayerStats stats;
    
    private const byte LEFT_MOUSE = 0;
    private const byte RIGHT_MOUSE = 1;
    private const byte R_KEY = 2;
    private const byte SPACE_BAR = 3;
    
    public void OnLeftMouse() {
        if (leftMouseAbility.Equals("Magic Bolt")) {
            
        }
    }

    public void OnRightMouse() {
        
    }

    public void OnSpaceBar() {
        if (spaceBarAbility.Equals("Dash")) {
            StartCoroutine(controller.Dash(0.15f));
        }
    }

    public void OnRKey() {
        
    }

    void Start() {
        controller = GetComponent<PlayerController>();
        stats = GetComponent<PlayerStats>();
        leftMouseAbility.prefab = Resources.Load<GameObject>("Prefabs/Projectiles/MagicBoltProjectile");
    }
    
    private void Use(Ability ability) {
        ability.lastUseTime = Time.time;
        ability.cooldown = ability.baseCooldown * stats.abilityCooldownReduction[ability.keyAssociation];
    }
}
