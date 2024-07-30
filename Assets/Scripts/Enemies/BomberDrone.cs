using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberDrone : EnemyBehavior {
    private GameObject explosionPrefab;
    private bool isFriendly;
    private Vector3 lockPosition = Vector2.zero;
    public BomberDrone() {
        health = 25;
        maxHealth = 25;
        speed = 8;
        damage = 65;
        canFly = false;
        ranged = false;
        attackRange = 1;
        attackCooldown = 0;
        selfPreservation = 0;
        difficulty = 2;
    }

    void Start() {
        Initialize();
        explosionPrefab = Resources.Load<GameObject>("Prefabs/Projectiles/ExplosionEffect");
    }

    protected override void Update() {
        if (lockPosition != Vector3.zero) transform.position = lockPosition;
        base.Update();
        animator.SetBool("moving", agent.velocity != Vector3.zero);
    }

    protected override void Attack() {
        agent.isStopped = true;
        lockPosition = transform.position;
        FreezeMovement();
        rb.velocity = Vector2.zero;
        agent.isStopped = true;
        isFriendly = false;
        SoundManager.Play(gameObject, "timebomb");
        Invoke(nameof(BlowUp), 1.25f);
    }

    protected override void Die() {
        agent.isStopped = true;
        lockPosition = transform.position;
        FreezeMovement();
        rb.velocity = Vector2.zero;
        agent.isStopped = true;
        isFriendly = true;
        SoundManager.Play(gameObject, "timebomb");
        Utility.FlashSprite(sr, Color.red, 1.25f);
        Invoke(nameof(BlowUp), 1.25f);
    }

    private void BlowUp() {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity).GetComponent<AOEBehavior>()
            .SpawnAOE(transform.position, damage, 2, 0.5f, isFriendly);
        base.Die();
    }
}
