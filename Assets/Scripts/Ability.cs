using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability {
    public string name;
    public float baseCooldown;
    public byte keyAssociation;
    public GameObject prefab;
    public float cooldown = 0;
    public float lastUseTime = 0;

    public Ability(string name, float baseCooldown, byte keyAssociation, GameObject prefab) {
        this.name = name;
        this.baseCooldown = baseCooldown;
        this.keyAssociation = keyAssociation;
        this.prefab = prefab;
    }

    public bool Equals(string name) {
        return this.name.Equals(name);
    }
}
