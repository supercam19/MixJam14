using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
    private PlayerStats stats;
    private HealthBarController healthBar;

    void Start() {
        stats = GetComponent<PlayerStats>();
        healthBar = FindObjectOfType<HealthBarController>();
    }
    public void TakeDamage(float damage) {
        stats.health -= damage * stats.damageReduction * stats.sprintDamageReduction;
        healthBar.SetHealth(stats.health, stats.maxHealth);
        if (stats.health <= 0) {
            Die();
        }
    }

    private void Die() {
        GameManager.paused = true;
        Debug.Log("You dead");
    }
}
