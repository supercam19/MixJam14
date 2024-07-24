using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb;
    private PlayerAbilities abilities;
    private PlayerStats stats;
    private Animator animator;
    private SpriteRenderer sr;

    private float sprintingChange = 1;
    private bool dashing = false;
    private Vector2 movingDirection;
    private float lastNonZeroX = 1;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        abilities = GetComponent<PlayerAbilities>();
        stats = GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) abilities.OnLeftMouse();
        if (Input.GetMouseButtonDown(1)) abilities.OnRightMouse();
        if (Input.GetKeyDown(KeyCode.Space)) abilities.OnSpaceBar();
        if (Input.GetKeyDown(KeyCode.R)) abilities.OnRKey();

        if (Input.GetKey(KeyCode.LeftShift)) {
            stats.sprinting = true;
            sprintingChange = stats.sprintMultiplier;
        }
        else {
            stats.sprinting = false;
            sprintingChange = 1;
        }
        
        animator.SetBool("idle", movingDirection == Vector2.zero);
        if (movingDirection.x != 0) lastNonZeroX = movingDirection.x;
        sr.flipX = lastNonZeroX < 0;
    }

    void FixedUpdate() {
        if (!GameManager.paused) {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            movingDirection = new Vector2(horizontal, vertical).normalized;
            if (!dashing)
                rb.velocity = movingDirection * (stats.speed * Time.deltaTime * sprintingChange);
        }
    }

    public IEnumerator Dash(float dashTime) {
        float startTime = Time.time;
        dashing = true;
        while (Time.time < startTime + dashTime) {
            rb.velocity = movingDirection * (100 * stats.speed * Time.deltaTime);
            yield return null;
        }

        dashing = false;
    }
}
