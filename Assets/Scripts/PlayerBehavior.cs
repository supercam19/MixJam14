using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
    private PlayerStats stats;
    private HealthBarController healthBar;
    private GameObject gameOver;

    void Start() {
        stats = GetComponent<PlayerStats>();
        healthBar = FindObjectOfType<HealthBarController>();
        gameOver = GameObject.Find("GameOver");
        gameOver.SetActive(false);
    }
    public void TakeDamage(float damage) {
        if (stats.invincible) return;
        if (Random.value > stats.wontDodgeChance) {
            damage = 0;
            SoundManager.Play(gameObject, "block_attack", pitchVariance: 0.1f);
        }
        stats.health -= damage * stats.damageReduction * stats.sprintDamageReduction;
        healthBar.SetHealth(stats.health, stats.maxHealth);
        if (stats.health <= 0) {
            Die();
        }
    }

    void Update() {
        stats.health = Mathf.Min(stats.health + stats.regenerationRate * Time.deltaTime, stats.maxHealth);
        healthBar.SetHealth(stats.health, stats.maxHealth);
    }

    private void Die() {
        if (stats.cheatDeaths > 0) {
            stats.cheatDeaths--;
            stats.health = stats.maxHealth;
            healthBar.SetHealth(stats.health, stats.maxHealth);
            GetComponent<Inventory>().RemoveItem("Phoenix's Feather", 1);
            SoundManager.Play(gameObject, "phoenix_feather_use");
            return;
        }
        GameManager.paused = true;
        Time.timeScale = 0;
        gameOver.SetActive(true);
    }
}
