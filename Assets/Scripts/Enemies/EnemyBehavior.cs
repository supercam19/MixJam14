using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour {
    protected float health = 100;
    protected float maxHealth = 100;
    protected float speed = 100;
    protected float damage = 10;
    protected bool canFly = false;
    protected bool ranged = false;
    protected float attackRange = 1;
    protected float attackCooldown = 1;
    protected int burstShots = 1;
    protected float burstCooldown = 0;
    protected float sightRange = 10;
    protected float followRange = 20;
    protected bool followingPlayer;
    protected float selfPreservation = 1;
    protected bool hasBeenHit = false;
    protected int difficulty = 1;

    protected float lastAttackTime = 0;
    protected GameObject player;
    protected NavMeshAgent agent;
    protected Transform playerTransform;
    protected PlayerBehavior playerBehavior;
    protected PlayerStats stats;
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected Animator animator;

    private float lastTickTime;
    private const float tickRate = 0.1f;
    protected bool moving = false;
    private Vector3 destination;
    private int shotsThisBurst;
    private bool retreat = false;
    private float originalSightRange;
    private bool freeze = false;
    private float invincibilityTime = 0;
    
    protected static string[] hitSounds = {"robot_damage_1", "robot_damage_2", "robot_damage_3", "robot_damage_4", "robot_damage_5"};

    protected void Initialize() {
        player = GameObject.Find("Player");
        playerTransform = player.transform;
        playerBehavior = player.GetComponent<PlayerBehavior>();
        stats = player.GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        lastTickTime = Random.Range(0, 0.1f);
        if (!canFly) {
            agent = GetComponent<NavMeshAgent>();
            agent.stoppingDistance = attackRange;
            agent.speed = speed;
        }
        originalSightRange = sightRange;
        damage += 2 * GameManager.level;
        health += 10 * GameManager.level;
        maxHealth = health;
    }

    protected virtual void Update() {
        if (GameManager.paused || Time.time < invincibilityTime) return;
        if (Time.time - lastTickTime > tickRate) {
            Tick();
            lastTickTime = Time.time;
        }
        if (moving)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(destination.x, destination.y, 0), speed * Time.deltaTime);
        if (!canFly)
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    protected virtual void Tick() {
        if (stats.cloaked == 1 && !hasBeenHit) {
            sightRange = 0;
        }
        else {
            sightRange = originalSightRange;
        }
        moving = false;
        if (followingPlayer) {
            float playerDistance = Vector2.Distance(transform.position, playerTransform.position);
            if (playerDistance < selfPreservation) {
                Vector3 awayVector = transform.position - playerTransform.position;
                MoveTo(awayVector.normalized * (attackRange - selfPreservation));
            }
            else if (playerDistance > followRange) {
                followingPlayer = false;
            }
            else if (playerDistance < attackRange && CanAttack()) {
                InitiateAttack();
                lastAttackTime = Time.time;
            }
            else {
                MoveTo(playerTransform.position);
            }
        }
        else if (Vector2.Distance(transform.position, playerTransform.position) < sightRange) {
            followingPlayer = true;
        }
        else {
            if (Random.value < 0.05f)
                Wander();
        }
    }

    private void Wander() {
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        MoveTo(transform.position + randomDirection * 5);
    }

    protected bool CanAttack() {
        if (shotsThisBurst < burstShots)
            return Time.time - lastAttackTime > attackCooldown;
        if (Time.time - lastAttackTime > burstCooldown) {
            shotsThisBurst = 0;
            return true;
        }

        return false;
    }

    protected virtual void MoveTo(Vector3 position) {
        if (!canFly) {
            agent.isStopped = false;
            agent.SetDestination(position);
        }
        else {
            moving = true;
            if (Vector2.Distance(transform.position, position) > attackRange)
                destination = position + (transform.position - position).normalized * attackRange;
        }
        sr.flipX = position.x > transform.position.x;
    }

    private void InitiateAttack() {
        shotsThisBurst++;
        Attack();
    }

    protected virtual void Attack() {
        
    }

    public virtual void TakeDamage(float damage) {
        if (Time.time < invincibilityTime) return;
        if (!hasBeenHit) {
            damage *= stats.firstHitMultiplier;
            hasBeenHit = true;
        }
        SoundManager.Play(gameObject, hitSounds, pitchVariance: 0.1f);
        health -= damage;
        if (health <= 0) {
            Die();
        }
        else {
            Utility.FlashSprite(sr, Color.red, 0.5f);
        }
    }

    protected virtual void Die() {
        GameManager.SetBalance(GameManager.money + Random.Range(GameManager.level, GameManager.level * difficulty) + (int)stats.bonusGoldPerKill + 5);
        GameManager.activeEnemies.Remove(this);
        GameManager.killsThisFloor++;
        Destroy(gameObject);
    }

    protected bool IsMoving() {
        return moving;
    }

    protected void FreezeMovement() {
        
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (other.gameObject.GetComponent<PlayerController>().dashing) {
                TakeDamage(other.gameObject.GetComponent<PlayerStats>().damage);
            }
        }
    }

    public void SpawnInvinvibility(float duration) {
        invincibilityTime = Time.time + duration;
    }
}
