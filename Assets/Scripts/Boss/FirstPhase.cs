using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPhase : MonoBehaviour
{   
    GameObject player;
    PlayerData playerData;
    public float attackCooldown; 
    public float attackPrepareTime;
    public float attackHeight;
    public float attackRange;
    public float attackKnockbackStrength;
    public int attackDamage;
    public float playerMovementDisableDuration; //needs to be more than 0, otherwise knockback doesn't work
    bool attacking = false;
    Transform t;
    Rigidbody2D rb;

    void Start()
    {
        player = gameObject.GetComponent<BossController>().player;
        playerData = gameObject.GetComponent<BossController>().playerData;
        t = gameObject.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!attacking)
        {
            StartCoroutine(Attack());
        }

        //debug
    }

    IEnumerator Attack()
    {
        attacking = true;
        //add attack
        rb.constraints = RigidbodyConstraints2D.FreezeAll; //stop movement when attacking
        Vector3 attackDirection = player.transform.position - t.position; //calculate attack direction
        Debug.DrawRay(t.position, attackDirection.normalized * attackRange, Color.red, attackPrepareTime); //debug

        yield return new WaitForSeconds(attackPrepareTime);
        RaycastHit2D hit = Physics2D.CircleCast(t.position, attackHeight, attackDirection.normalized, attackRange, 1 << 6); //6 is player layer

        if (hit)
        {
            playerData.takeHit(attackDamage); //damage player
            Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>(); //get player's rigidbody
            print("BOSS HIT PLAYER"); //debug
            player.GetComponent<PlayerMovementController>().enabled = false; //disable player movement during knockback
            playerRB.velocity = (Vector2)t.TransformDirection(Vector3.right) * attackKnockbackStrength; //knock back player
            yield return new WaitForSeconds(playerMovementDisableDuration); //
            player.GetComponent<PlayerMovementController>().enabled = true; //enable player movement again
        }

        yield return new WaitForSeconds(1);
        rb.constraints = RigidbodyConstraints2D.None; //resume movement

        yield return new WaitForSeconds(attackCooldown); //cooldown
        attacking = false;
    }

/*     void OnDrawGizmos()
    {

    } */
}
