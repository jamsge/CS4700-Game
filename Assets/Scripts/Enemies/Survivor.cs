using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survivor : MonoBehaviour
{    
    public EnemyController ec;
    public float attackDamage;
    public float attackRange;
    public float attackCooldown;
    public float chaseSpeed;
    public float chaseTime;
    public float playerStunDuration;
    Transform t;
    Rigidbody2D rb;
    Collider2D coll;
    GameObject player;
    PlayerData playerData;
    bool playerDetected;
    float positionDiff;
    bool onCooldown = false;
    Vector3 chaseDirection;
    public float initAttackRange;
    bool damageDealt = false;

    void Start()
    {
        initAttackRange = attackRange;
        t = ec.t;
        rb = ec.rb;
        coll = gameObject.GetComponent<Collider2D>();
        player = ec.player;
        playerData = ec.playerData;
    }

    void Update()
    {
        float chaseDirectionX = player.transform.position.x - t.position.x;
        chaseDirection = new Vector3(chaseDirectionX, 0, 0);
        chaseDirection.Normalize();

        SetAttackRange();
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
        StartCoroutine(Attack());
        if (!onCooldown)
        {
            MoveTowardsPlayer();
            StartCoroutine(Cooldown());
        }
    }

/* 
    IEnumerator Chase()
    {
        chasing = true;
        Vector3 direction = new Vector3(player.transform.position.x - t.position.x, 0, 0);
        direction.Normalize();
        rb.velocity = direction * chaseSpeed;
        yield return new WaitForSeconds(chaseTime);
        //rb.velocity = new Vector3(0,0,0);
        yield return new WaitForSeconds(attackCooldown);
        chasing = false;
    } */

    void MoveTowardsPlayer()
    {
        rb.velocity = (Vector2)chaseDirection * chaseSpeed;

        //face player
        if (chaseDirection.x == 1f)
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
        Vector2 originLeft = new Vector2(t.position.x - coll.bounds.extents.x, t.position.y);
        Vector2 originRight = new Vector2(t.position.x + coll.bounds.extents.x, t.position.y);
        RaycastHit2D hitLeft = Physics2D.CircleCast(originLeft, attackRange/2, t.TransformDirection(Vector3.left), 0, 1 << 6);
        RaycastHit2D hitRight = Physics2D.CircleCast(originRight, attackRange/2, t.TransformDirection(Vector3.right), 0, 1 << 6);
        if (hitLeft || hitRight)
        {
            print("PLAYER HIT"); //debug
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            if (!damageDealt)
            {
                playerData.takeHit((int)attackDamage);
                damageDealt = true;
            }
            yield return new WaitForSeconds(playerStunDuration);
            damageDealt = false;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }
    }

    IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        playerDetected = false;
        onCooldown = false;
    }

    void SetAttackRange()
    {
        if (Mathf.Abs(rb.velocity.x) > ec.idleSpeed)
        {
            attackRange = initAttackRange;
        }
        else
        {
            attackRange = 0;
        }
    }
}
