using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour {
    public bool isFriendly = false;
    public float speed = 1000;
    public float damageMultiplier = 1;
    public bool isAOE = false;
    public GameObject aoePrefab;

    private Rigidbody2D rb;
    private PlayerStats stats;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        stats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }
    
    public void Fire(Vector3 direction, float speed = -1) {
        if (speed == -1) speed = this.speed;
        rb.velocity = direction * (speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (isAOE) {
            GetComponent<AOEBehavior>().SpawnAOE(transform.position);
        }
        else if (other.gameObject.CompareTag("Player") && !isFriendly) {
            other.gameObject.GetComponent<PlayerBehavior>().TakeDamage(stats.damage * damageMultiplier);
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
