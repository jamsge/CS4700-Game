using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPhase : MonoBehaviour
{   
    public GameObject player;
    public float attackCooldown; 
    public float attackHeight;
    public float attackRange;
    public float attackKnockbackStrength;
    bool attacking = false;
    Transform t;
    Rigidbody2D rb;

    void Start()
    {
        t = gameObject.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!attacking)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        attacking = true;
        //add attack
        rb.constraints = RigidbodyConstraints2D.FreezeAll; //stop movement when attacking

        Vector3 attackDirection = player.transform.position - t.position; //FIX ATTACK DIRECTION //ADD DEBUG DRAWRAYS
        //attackDirection.Normalize();
        RaycastHit2D hit = Physics2D.CircleCast(t.position, attackHeight, t.TransformDirection(Vector3.right), attackRange, 1 >> 6); //6 is player layer
        if (hit)
        {
            Rigidbody2D playerRB = hit.collider.gameObject.GetComponent<Rigidbody2D>();
            playerRB.velocity = t.TransformDirection(Vector3.left) * attackKnockbackStrength;
        }
        yield return new WaitForSeconds(1);
        rb.constraints = RigidbodyConstraints2D.None; //resume movement

        yield return new WaitForSeconds(attackCooldown); //cooldown
        attacking = false;
    }

    void OnDrawGizmos()
    {

    }
}
