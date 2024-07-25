using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGunnerDrone : EnemyBehavior {
    private GameObject bulletPrefab;
    public SmallGunnerDrone() {
        health = 50;
        maxHealth = 50;
        speed = 300;
        canFly = true;
        ranged = true;
        attackRange = 5;
        attackCooldown = 0.2f;
        sightRange = 15;
        followRange = 25;
        selfPreservation = 0.1f;
    }

    void Start() {
        Debug.Log("Health: " + health);
        Initialize();
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Projectiles/Bullet");
    }

    protected override void Attack() {
        ProjectileBehavior pb = Instantiate(bulletPrefab, transform.position, Utility.RotateToPoint(transform.position, playerTransform.position)).GetComponent<ProjectileBehavior>();
        pb.Fire((player.transform.position - transform.position).normalized);
    }
}
