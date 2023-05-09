using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Inherits from rat because of similar behavior
public class Insect : Rat
{
    Vector3 moveDirection;
    SpriteRenderer rend;
    public float attackPrepareTime;
    public override void MoveTowardsPlayer()
    {
        moveDirection = player.transform.position - t.position;
        moveDirection.Normalize();
        rb.velocity = (Vector2)moveDirection * chaseSpeed;
        rend = gameObject.GetComponent<SpriteRenderer>();

        RotateTowardsPlayer();
    }

    public override IEnumerator Attack()
    {
        attacking = true;
        //quick attack towards player

        //calc attack direction
        Vector3 direction = player.transform.position - t.position;
        direction.Normalize();
        //prepare for attack (so player has time to avoid)
        yield return new WaitForSeconds(attackPrepareTime);
        animator.SetBool("attacking", true);
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("attacking", false);
        RaycastHit2D hit = Physics2D.Raycast(t.position, direction, attackRange, 1 << 6); //6 is player layer
        print("INSECT ATTACKS"); //debug
        if (hit)
        {
            print("PLAYER HIT"); //debug
            playerData.takeHit((int)attackDamage);
        }
        yield return new WaitForSeconds(attackCooldown);
        attacking = false;
    }

    void RotateTowardsPlayer()
    {
        if ((player.transform.position.x < t.position.x))
        {
            t.rotation = Quaternion.Euler(new Vector3(0,180,0));
        }
        else
        {
            t.rotation = Quaternion.Euler(new Vector3(0,0,0));
        }   
    }

    public override void OnDrawGizmos()
    {
        if (t != null)
        {
            Gizmos.DrawLine(t.position, player.transform.position);
        }
    }
}
