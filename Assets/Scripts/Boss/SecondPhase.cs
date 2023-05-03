using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPhase : MonoBehaviour
{
    GameObject player;
    PlayerData playerData;
    public float jumpHeight;
    public float attackRange;
    public float attackHeight;
    public float attackDamage;
    public float attackLaunchStrength;
    public float attackCooldown;
    public float playerMovementDisableDuration; //needs to be more than 0
    bool attacking = false;
    bool isGrounded;
    Transform t;
    Rigidbody2D rb;
    Collider2D coll;

    void Start()
    {
        player = gameObject.GetComponent<BossController>().player;
        playerData = gameObject.GetComponent<BossController>().playerData;
        t = gameObject.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        coll = gameObject.GetComponent<Collider2D>();
    }

    void Update()
    {
        CheckGrounded();
        if (isGrounded && !attacking)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        attacking = true;
        print("Attacking"); //debug
        rb.velocity = new Vector2(0, jumpHeight);
        yield return new WaitForSeconds(attackCooldown);
        attacking = false;
    }

    void CheckGrounded()
    {
        Vector2 rayOrigin = new Vector2(t.position.x, t.position.y - coll.bounds.extents.y);
        isGrounded = Physics2D.Raycast(rayOrigin, Vector2.down, 0.1f, 1 << 7); //7 is floor layer - has to be on the ground to jump
    }
}
