using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Inherits from rat because of similar behavior
public class Insect : Rat
{
    public float attackPrepareTime;
    public override void MoveTowardsPlayer()
    {
        Vector3 moveDirection = player.transform.position - t.position;
        moveDirection.Normalize();
        rb.velocity = (Vector2)moveDirection * chaseSpeed;

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
        var offset = 90f;
        Vector2 direction = player.transform.position - t.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
        t.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }

    public override void OnDrawGizmos()
    {
        if (t != null)
        {
            Gizmos.DrawLine(t.position, player.transform.position);
        }
    }
}
