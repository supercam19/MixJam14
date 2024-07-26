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
    protected float sightRange = 10;
    protected float followRange = 20;
    protected bool followingPlayer;
    protected float selfPreservation;
    protected bool hasBeenHit = false;

    protected float lastAttackTime = 0;
    protected GameObject player;
    protected NavMeshAgent agent;
    protected Transform playerTransform;
    protected PlayerBehavior playerBehavior;
    protected PlayerStats stats;
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;

    private float lastTickTime;
    private const float tickRate = 0.1f;
    private bool moving = false;
    private Vector3 destination;

    protected void Initialize() {
        player = GameObject.Find("Player");
        playerTransform = player.transform;
        playerBehavior = player.GetComponent<PlayerBehavior>();
        stats = player.GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        lastTickTime = Random.Range(0, 0.1f);
        if (!canFly) {
            agent = GetComponent<NavMeshAgent>();
            agent.stoppingDistance = attackRange;
            agent.speed = speed;
        }
    }

    void Update() {
        if (Time.time - lastTickTime > tickRate) {
            Tick();
            lastTickTime = Time.time;
        }
        if (moving)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(destination.x, destination.y, 0), speed * Time.deltaTime);
    }

    protected virtual void Tick() {
        moving = false;
        if (followingPlayer) { 
            if (health / maxHealth < selfPreservation) {
                Vector3 awayVector = transform.position - playerTransform.position;
                MoveTo(awayVector * 15);
            }
            else if (Vector2.Distance(transform.position, playerTransform.position) > followRange) {
                followingPlayer = false;
            }
            else if (Vector2.Distance(transform.position, playerTransform.position) < attackRange && CanAttack()) {
                Attack();
                lastAttackTime = Time.time;
            }
            else {
                MoveTo(playerTransform.position);
            }
        }
        else if (Vector2.Distance(transform.position, playerTransform.position) < sightRange) {
            followingPlayer = true;
        }
        
    }

    protected bool CanAttack() {
        return Time.time - lastAttackTime > attackCooldown;
    }

    protected virtual void MoveTo(Vector3 position) {
        if (!canFly) {
            agent.isStopped = false;
            agent.SetDestination(position);
        }
        else {
            moving = true;
            destination = position + (transform.position - position).normalized * attackRange;
        }
        sr.flipX = position.x > transform.position.x;
    }

    protected virtual void Attack() {
        
    }

    public virtual void TakeDamage(float damage) {
        if (!hasBeenHit) {
            damage *= stats.firstHitMultiplier;
            hasBeenHit = true;
        }
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    protected virtual void Die() {
        GameManager.activeEnemies.Remove(this);
        Destroy(gameObject);
    }
}
