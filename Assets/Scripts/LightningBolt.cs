using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : MonoBehaviour {
    private SpriteRenderer sr;
    private List<SpriteRenderer> stems;
    
    public void Strike(float damageMultiplier) {
        stems = new List<SpriteRenderer>();
        sr = GetComponent<SpriteRenderer>();
        GameObject stemPrefab = Resources.Load<GameObject>("Prefabs/Projectiles/Stem");
        PlayerStats stats = GameObject.Find("Player").GetComponent<PlayerStats>();
        float stemY = transform.position.y;
        while (Camera.main.WorldToViewportPoint(new Vector3(transform.position.x, stemY, transform.position.z)).y < 1) {
            stemY++;
            stems.Add(Instantiate(stemPrefab, new Vector3(transform.position.x, stemY, transform.position.z),
                Quaternion.identity).GetComponent<SpriteRenderer>());
        }
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.2f);
        foreach (Collider2D hit in hits) {
            if (hit.gameObject.CompareTag("Enemy")) {
                hit.GetComponent<EnemyBehavior>().TakeDamage(stats.damage * damageMultiplier);
            }
        }
    }

    void Update() {
        if (sr.color.a > 0.05f) {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - 0.005f);
            foreach (SpriteRenderer stem in stems) {
                stem.color = sr.color;
            }
        } else {
            foreach (SpriteRenderer stem in stems) {
                Destroy(stem.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
