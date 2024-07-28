using UnityEngine;

public class PlayerAbilities : MonoBehaviour {
    [HideInInspector] public Ability leftMouseAbility = new Ability("Magic Bolt", 0.25f, LEFT_MOUSE, null);
    [HideInInspector] public Ability rightMouseAbility = new Ability("Fireball", 3.0f, RIGHT_MOUSE, null);
    [HideInInspector] public Ability rKeyAbility = new Ability("Lightning Strike", 15.0f, R_KEY, null);
    [HideInInspector] public Ability spaceBarAbility = new Ability("Dash", 1.5f, SPACE_BAR, null);

    private PlayerController controller;
    private PlayerStats stats;
    
    private const byte LEFT_MOUSE = 0;
    private const byte RIGHT_MOUSE = 1;
    private const byte R_KEY = 2;
    private const byte SPACE_BAR = 3;

    [HideInInspector] public Ability[] abilities;
    private float[] abilityCooldownReductions = new float[4];
    
    private string[] magicBoltSounds = new string[] {"magic_bolt_1", "magic_bolt_2", "magic_bolt_3", "magic_bolt_4"};
    
    public void OnLeftMouse() {
        if (Time.time - leftMouseAbility.cooldown > leftMouseAbility.lastUseTime) {
            if (leftMouseAbility.Equals("Magic Bolt")) {
                ProjectileBehavior bolt = Instantiate(leftMouseAbility.prefab, transform.position, Quaternion.identity)
                    .GetComponent<ProjectileBehavior>();
                bolt.Fire((Utility.GetMousePosition() - transform.position).normalized);
                SoundManager.Play(gameObject, magicBoltSounds);
                Use(leftMouseAbility);
            }
        }
    }

    public void OnRightMouse() {
        if (Time.time - rightMouseAbility.cooldown > rightMouseAbility.lastUseTime) {
            if (rightMouseAbility.Equals("Fireball")) {
                ProjectileBehavior fireball =
                    Instantiate(rightMouseAbility.prefab, transform.position, Quaternion.identity)
                        .GetComponent<ProjectileBehavior>();
                fireball.Fire((Utility.GetMousePosition() - transform.position).normalized);
                SoundManager.Play(gameObject, "fireball_summon");
                Use(rightMouseAbility);
            }
        }
    }

    public void OnSpaceBar() {
        if (Time.time - spaceBarAbility.cooldown > spaceBarAbility.lastUseTime) {
            if (spaceBarAbility.Equals("Dash")) {
                StartCoroutine(controller.Dash(0.15f));
                Use(spaceBarAbility);
            }
        }
    }

    public void OnRKey() {
        if (Time.time - rKeyAbility.cooldown > rKeyAbility.lastUseTime) {
            Instantiate(Resources.Load<GameObject>("Prefabs/Projectiles/LightningBolt"), Utility.GetLocalMouse(),
                Quaternion.identity).GetComponent<LightningBolt>().Strike(5);
            Use(rKeyAbility);
        }
    }

    void Start() {
        abilities = new Ability[] { leftMouseAbility, rightMouseAbility, rKeyAbility, spaceBarAbility };
        stats = GetComponent<PlayerStats>();
        abilityCooldownReductions = new float[] {stats.leftMouseAbilityCooldownReduction, stats.rightMouseAbilityCooldownReduction, stats.rKeyAbilityCooldownReduction, stats.spaceBarAbilityCooldownReduction};
        controller = GetComponent<PlayerController>();
        leftMouseAbility.prefab = Resources.Load<GameObject>("Prefabs/Projectiles/MagicBoltProjectile");
        rightMouseAbility.prefab = Resources.Load<GameObject>("Prefabs/Projectiles/FireballProjectile");
    }
    
    private void Use(Ability ability) {
        ability.lastUseTime = Time.time;
        ability.cooldown = ability.baseCooldown * abilityCooldownReductions[ability.keyAssociation];
    }

    public void Reset() {
        leftMouseAbility.lastUseTime = 0;
        rightMouseAbility.lastUseTime = 0;
        rKeyAbility.lastUseTime = 0;
        spaceBarAbility.lastUseTime = 0;
    }
}
