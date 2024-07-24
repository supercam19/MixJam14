using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour {
    protected float health = 100;
    protected float maxHealth = 100;
    protected float speed = 400;
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
    protected Rigidbody2D rb;

    private float lastTickTime;

    void Start() {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        playerTransform = player.transform;
        playerBehavior = player.GetComponent<PlayerBehavior>();
        rb = GetComponent<Rigidbody2D>();
        lastTickTime = Random.Range(0, 100);
        agent.stoppingDistance = attackRange;
        agent.speed = speed;
    }

    void Update() {
        if (Time.time - lastTickTime > 0) {
            Tick();
            lastTickTime = Time.time;
        }
    }

    protected virtual void Tick() {
        if (followingPlayer) { 
            if (health / maxHealth < selfPreservation) {
                Vector3 awayVector = transform.position - playerTransform.position;
                MoveTo(awayVector * 15);
            }
            else if (Vector2.Distance(transform.position, playerTransform.position) > followRange) {
                followingPlayer = false;
                agent.isStopped = true;
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
            transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
        }
    }

    protected virtual void Attack() {
        
    }

    public virtual void TakeDamage(float damage) {
        hasBeenHit = true;
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    protected virtual void Die() {
        Destroy(gameObject);
    }
}
