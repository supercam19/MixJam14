using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEBehavior : MonoBehaviour {

    public void SpawnAOE(Vector3 position, float damage = 2.5f, float radius = 2, float duration = 0.5f, bool isFriendly = false) {
        transform.localScale = new Vector3(radius * 2, radius * 2, 1);
        Collider2D[] nearby = Physics2D.OverlapCircleAll(position, radius);
        foreach (Collider2D hit in nearby) {
            if (hit.gameObject.CompareTag("Enemy") && isFriendly) {
                hit.gameObject.GetComponent<EnemyBehavior>().TakeDamage(damage);
            } else if (hit.gameObject.CompareTag("Player") && !isFriendly) {
                hit.gameObject.GetComponent<PlayerBehavior>().TakeDamage(damage);
            }
        }
        SoundManager.Play(gameObject, "explosion_generic");
        Invoke(nameof(Despawn), duration);
    }

    private void Despawn() {
        Destroy(gameObject);
    }
}
