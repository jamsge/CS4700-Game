using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    public EnemyController ec;
    public float attackDamage;
    public float attackRange;
    public float attackCooldown;
    bool playerDetected;
    Transform t;
    Rigidbody2D rb;
    GameObject player;
    PlayerData playerData;
    float positionDiff; //difference in positions of player and this enemy\
    bool attacking = false;
    void Start()
    {
        t = ec.gameObject.GetComponent<Transform>();
        rb = ec.gameObject.GetComponent<Rigidbody2D>();
        player = ec.player;
        playerData = ec.playerData;
    }

    void Update()
    {
        //check if player detected
        positionDiff = Mathf.Abs(player.transform.position.x - t.position.x);
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

    void MoveTowardsPlayer()
    {
        Vector3 moveDirection = player.transform.position - t.position;
        moveDirection.y = 0;
        moveDirection.Normalize();
        rb.velocity = (Vector2)moveDirection * ec.speed;

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

    IEnumerator Attack()
    {
        attacking = true;
        //quick attack to the side
        RaycastHit2D hit = Physics2D.Raycast(t.position, Vector3.right, attackRange, 1 << 6); //6 is player layer
        print("RAT ATTACKS"); //debug
        if (hit)
        {
            playerData.takeHit((int)attackDamage);
        }
        yield return new WaitForSeconds(attackCooldown);
        attacking = false;
    }

    //visualize attack range
    void OnDrawGizmos()
    {
        if (t != null)
            Gizmos.DrawLine(t.position, t.position + t.TransformDirection(new Vector3(attackRange, 0, 0)));
    }

}
