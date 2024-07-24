using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
    private PlayerStats stats;

    void Start() {
        stats = GetComponent<PlayerStats>();
    }
    public void TakeDamage(float damage) {
        stats.health -= damage;
        if (stats.health <= 0) {
            Die();
        }
    }

    private void Die() {
        GameManager.paused = true;
        Debug.Log("You dead");
    }
}
