using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEBehavior : MonoBehaviour {
    public float damage = 10;
    public float radius = 1;
    public float duration = 1;
    public bool isFriendly = false;

    public void SpawnAOE(Vector3 position) {
        Collider2D[] nearby = Physics2D.OverlapCircleAll(position, radius);
        foreach (Collider2D hit in nearby) {
            if (hit.gameObject.CompareTag("Enemy") && isFriendly) {
                hit.gameObject.GetComponent<EnemyBehavior>().TakeDamage(damage);
            } else if (hit.gameObject.CompareTag("Player") && !isFriendly) {
                hit.gameObject.GetComponent<PlayerBehavior>().TakeDamage(damage);
            }
        }
        Invoke(nameof(Despawn), duration);
    }

    private void Despawn() {
        Destroy(gameObject);
    }
}
