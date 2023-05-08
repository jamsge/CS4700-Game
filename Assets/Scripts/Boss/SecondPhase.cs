using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPhase : MonoBehaviour
{
    public float jumpHeight;
    public float attackRange;
    public float attackHeight;
    public float attackDamage;
    public float attackLaunchStrength;
    public float attackCooldown;
    bool attacking = false;
    bool isGrounded;
    Transform t;
    Rigidbody2D rb;
    Collider2D coll;
    public int attackCounter = 0;

    void Start()
    {
        t = gameObject.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        coll = gameObject.GetComponent<Collider2D>();
    }

    void Update()
    {
        if (attackCounter < 3)
        {
            CheckGrounded();
            if (isGrounded && !attacking)
            {
                StartCoroutine(Attack());
            }
        }
        else
        {
            attackCounter = 0;
            gameObject.GetComponent<FirstPhase>().enabled = true;
            gameObject.GetComponent<SecondPhase>().enabled = false;
        }
    }

    IEnumerator Attack()
    {
        attacking = true;
        print("Attacking"); //debug
        rb.velocity = new Vector2(0, jumpHeight);
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<WaveAttack>().enabled = true;
        yield return new WaitForSeconds(attackCooldown);
        attacking = false;
    }

    void CheckGrounded()
    {
        Vector2 rayOrigin = new Vector2(t.position.x, t.position.y - coll.bounds.extents.y);
        isGrounded = Physics2D.Raycast(rayOrigin, Vector2.down, 0.1f, 1 << 7); //7 is floor layer - has to be on the ground to jump
    }
}
