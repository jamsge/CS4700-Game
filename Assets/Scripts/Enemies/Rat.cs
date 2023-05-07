using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    public EnemyController ec;
    public float attackDamage;
    public float attackRange;
    public float attackCooldown;
    public float chaseSpeed;
    bool playerDetected;

    [HideInInspector]
    public Transform t;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public PlayerData playerData;
    [HideInInspector]
    public float positionDiff; //difference in positions of player and this enemy
    [HideInInspector]
    public bool attacking = false;
    void Start()
    {
        t = ec.t;
        rb = ec.rb;
        player = ec.player;
        playerData = ec.playerData;
    }

    void Update()
    {
        //check if player detected
        positionDiff = Vector3.Distance(player.transform.position, t.position);
        if (positionDiff <= ec.detectionDistance)
        {
            playerDetected = true;
        }
        else
        {
            playerDetected = false;
        }

        if (playerDetected)
        {
            OnDetection();
        }
        else
        {
            ec.Idle();
        }
    }

    void OnDetection()
    {
        bool inAttackRange = positionDiff <= attackRange;
        //check if in attack range, if not, move towards player
        if (inAttackRange && !attacking)
        {
            StartCoroutine(Attack());
        }
        else if (!attacking)
        {
            MoveTowardsPlayer();
        }
    }

    public virtual void MoveTowardsPlayer()
    {
        Vector3 moveDirection = player.transform.position - t.position;
        moveDirection.y = 0;
        moveDirection.Normalize();
        rb.velocity = (Vector2)moveDirection * chaseSpeed;

        //face player
        if (moveDirection.x == 1)
        {
            t.rotation = Quaternion.Euler(new Vector3(0,0,0));
        }
        else
        {
            t.rotation = Quaternion.Euler(new Vector3(0,180,0));
        }
    }

    public virtual IEnumerator Attack()
    {
        attacking = true;
        //quick attack to the side
        RaycastHit2D hit = Physics2D.Raycast(t.position, t.TransformDirection(Vector3.right), attackRange, 1 << 6); //6 is player layer
        print("RAT ATTACKS"); //debug
        if (hit)
        {
            print("PLAYER HIT"); //debug
            playerData.takeHit((int)attackDamage);
        }
        yield return new WaitForSeconds(attackCooldown);
        attacking = false;
    }

    //visualize attack range
    public virtual void OnDrawGizmos()
    {
        if (t != null)
            Gizmos.DrawLine(t.position, t.position + t.TransformDirection(new Vector3(attackRange, 0, 0)));
    }

}
