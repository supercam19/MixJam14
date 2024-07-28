using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    [SerializeField] private float _health = 100;

    public float health {
        get => _health; 
        set {_health = value;
            healthBar.SetHealth(_health, maxHealth);
        }
    }
    [SerializeField] private float _maxHealth = 100;

    public float maxHealth {
        get => _maxHealth;
        set {
            _maxHealth = value;
            healthBar.SetHealth(health, _maxHealth);
        }
    }

    [SerializeField] private float _speed = 400;
    public float speed { get => _speed; set => _speed = value; }
    [SerializeField] private float _sprintMultiplier = 1.5f;
    public float sprintMultiplier { get => _sprintMultiplier; set => _sprintMultiplier = value; }
    [SerializeField] private bool _sprinting = false;
    public bool sprinting { get => _sprinting; set => _sprinting = value; }
    [SerializeField] private float _damage = 20;
    public float damage { get => _damage; set => _damage = value; }
    [SerializeField] private bool _visible = true;
    public bool visible { get => _visible; set => _visible = value; }
    [SerializeField] private bool _invincible = false;
    public bool invincible { get => _invincible; set => _invincible = value; }
    [SerializeField] private float _leftMouseAbilityCooldownReduction = 1;
    public float leftMouseAbilityCooldownReduction { get => _leftMouseAbilityCooldownReduction; set => _leftMouseAbilityCooldownReduction = value; }
    [SerializeField] private float _rightMouseAbilityCooldownReduction = 1;
    public float rightMouseAbilityCooldownReduction { get => _rightMouseAbilityCooldownReduction; set => _rightMouseAbilityCooldownReduction = value; }
    [SerializeField] private float _spaceBarAbilityCooldownReduction = 1;
    public float spaceBarAbilityCooldownReduction { get => _spaceBarAbilityCooldownReduction; set => _spaceBarAbilityCooldownReduction = value; }
    [SerializeField] private float _rKeyAbilityCooldownReduction = 1;
    public float rKeyAbilityCooldownReduction { get => _rKeyAbilityCooldownReduction; set => _rKeyAbilityCooldownReduction = value; }
    [SerializeField] private float _damageReduction = 1;
    public float damageReduction { get => _damageReduction; set => _damageReduction = value; }
    [SerializeField] private float _firstHitMultiplier = 1;
    public float firstHitMultiplier { get => _firstHitMultiplier; set => _firstHitMultiplier = value; }
    [SerializeField] private float _bonusGoldPerKill = 0;
    public float bonusGoldPerKill { get => _bonusGoldPerKill; set => _bonusGoldPerKill = value; }
    [SerializeField] private float _cloaked = 0;
    public float cloaked { get => _cloaked; set => _cloaked = value; }
    [SerializeField] private float _sprintDamageReduction = 1;
    public float sprintDamageReduction { get => _sprintDamageReduction; set => _sprintDamageReduction = value; }
    [SerializeField] private float _regenerationRate = 0.5f;
    public float regenerationRate { get => _regenerationRate; set => _regenerationRate = value; }




    private HealthBarController healthBar;

    void Start() {
        healthBar = FindObjectOfType<HealthBarController>();
    }

    public void Reset() {
        health = 100;
        maxHealth = 100;
        speed = 400;
        sprintMultiplier = 1.5f;
        sprinting = false;
        damage = 20;
        visible = true;
        invincible = false;
        leftMouseAbilityCooldownReduction = 1;
        rightMouseAbilityCooldownReduction = 1;
        spaceBarAbilityCooldownReduction = 1;
        rKeyAbilityCooldownReduction = 1;
        damageReduction = 1;
        firstHitMultiplier = 1;
        bonusGoldPerKill = 0;
        cloaked = 0;
        sprintDamageReduction = 1;
        regenerationRate = 0.5f;
    }

}
