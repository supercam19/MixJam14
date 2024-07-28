using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour {
    public bool isFriendly = false;
    public float speed = 2500;
    public float damageMultiplier = 1;
    public bool isAOE = false;
    public GameObject aoePrefab;

    private Rigidbody2D rb;
    private PlayerStats stats;
    private Vector3 movement;
    private float lifeTime = 5;
    private float startTime;
    
    public void Fire(Vector3 direction, float speed = -1) {
        if (speed == -1) speed = this.speed;
        transform.rotation = Utility.RotateToPoint(transform.position, transform.position + direction);
        movement = direction * speed;
        startTime = Time.time;
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        stats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    void Update() {
        rb.velocity = movement * Time.deltaTime;
        if (Time.time - startTime > lifeTime) {
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<ProjectileBehavior>() != null || other.gameObject.CompareTag("Chest")) return;
        if (isAOE) {
            Instantiate(aoePrefab, transform.position, Quaternion.identity).GetComponent<AOEBehavior>().SpawnAOE(transform.position, stats.damage * damageMultiplier, 1.5f, 0.5f, isFriendly);
        }
        else if (other.gameObject.CompareTag("Player") && !isFriendly) {
            other.gameObject.GetComponent<PlayerBehavior>().TakeDamage(damageMultiplier);
        }
        else if (other.gameObject.CompareTag("Enemy") && isFriendly) {
            other.gameObject.GetComponent<EnemyBehavior>().TakeDamage(stats.damage * damageMultiplier);
        }

        Die();
    }


    private void Die() {
        Destroy(gameObject);
    }
}
