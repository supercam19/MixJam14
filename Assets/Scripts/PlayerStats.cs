using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    public float health = 100;
    public float maxHealth = 100;
    public float speed = 400;
    public float sprintMultiplier = 1.5f;
    public float damage = 10;
    public bool visible = true;
    public bool invincible = false;
    public bool sprinting = false;
    public float[] abilityCooldownReduction = { 1.0f, 1.0f, 1.0f, 1.0f };
    public float damageReduction = 1.0f;
    public float firstHitMultiplier = 1.0f;
    public int bonusGoldPerKill = 0;
    public bool cloaked = false;
    public float sprintDamageReduction = 1.0f;
}
