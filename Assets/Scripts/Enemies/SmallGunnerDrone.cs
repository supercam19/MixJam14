using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGunnerDrone : EnemyBehavior {
    private GameObject bulletPrefab;
    private static string[] shootSounds = { "machine_gun_fire_1", "machine_gun_fire_2", "machine_gun_fire_3" };
    public SmallGunnerDrone() {
        health = 50;
        damage = 2;
        maxHealth = 50;
        speed = 2f;
        canFly = true;
        ranged = true;
        attackRange = 5;
        attackCooldown = 0.2f;
        burstShots = 3;
        burstCooldown = 1.4f;
        sightRange = 10;
        followRange = 25;
        selfPreservation = 0.1f;
    }

    void Start() {
        Initialize();
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Projectiles/Bullet");
    }

    protected override void Attack() {
        ProjectileBehavior pb = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<ProjectileBehavior>();
        pb.Fire(player.transform.position - transform.position);
        pb.damageMultiplier = damage;
        SoundManager.Play(gameObject, shootSounds);
    }

    protected override void Die() {
        AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/small_gunner_drone_death"), transform.position, SoundManager.sfxVolume * SoundManager.masterVolume);
        base.Die();
    }
    
}
