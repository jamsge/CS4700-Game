using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insect : Rat
{
    public float attackPrepareTime;
    public override void MoveTowardsPlayer()
    {
        Vector3 moveDirection = player.transform.position - t.position;
        moveDirection.Normalize();
        rb.velocity = (Vector2)moveDirection * ec.speed;

        //add facing player
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

    public override void OnDrawGizmos()
    {
        //add gizmo later
    }
}
