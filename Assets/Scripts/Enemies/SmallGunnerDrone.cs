using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGunnerDrone : EnemyBehavior {
    private GameObject bulletPrefab;
    private static string[] shootSounds = { "machine_gun_fire_1", "machine_gun_fire_2", "machine_gun_fire_3" };
    public SmallGunnerDrone() {
        health = 50;
        maxHealth = 50;
        speed = 2f;
        canFly = true;
        ranged = true;
        attackRange = 5;
        attackCooldown = 0.2f;
        sightRange = 15;
        followRange = 25;
        selfPreservation = 0.1f;
    }

    void Start() {
        Initialize();
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Projectiles/Bullet");
    }

    protected override void Attack() {
        ProjectileBehavior pb = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<ProjectileBehavior>();
        pb.Fire((player.transform.position - transform.position).normalized, 5000);
        SoundManager.Play(gameObject, shootSounds);
    }
}
