using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMonster : MonoBehaviour
{    
    public static float waterBallDamage; //static so player scripts can access this damage
    public EnemyController ec;
    public float meleeAttackDamage;
    public float meleeAttackCooldown;
    public float meleeAttackRange;
    public float rangedAttackDamage;
    public float rangedAttackCooldown;
    public float rangedAttackRange;
    public GameObject rangedAttackObject;
    public float rangedAttackObjectSpeed;
    public float chaseSpeed;
    Transform t;
    Rigidbody2D rb;
    Collider2D coll;
    GameObject player;
    PlayerData playerData;
    bool playerDetected;
    bool attackingMelee;
    bool attackingRanged;
    float positionDiff;

    void Start()
    {
        waterBallDamage = rangedAttackDamage;

        t = ec.t;
        rb = ec.rb;
        coll = gameObject.GetComponent<Collider2D>();
        player = ec.player;
        playerData = ec.playerData;
    }

    void Update()
    {
        positionDiff = Vector3.Distance(player.transform.position, t.position);
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
        bool inMeleeAttackRange = positionDiff <= meleeAttackRange;
        bool inRangedAttackRange = positionDiff <= rangedAttackRange;
        if (inMeleeAttackRange && !(attackingMelee || attackingRanged))
        {
            StartCoroutine(MeleeAttack());
        }
        else if (inRangedAttackRange && !(attackingRanged || attackingMelee))
        {
            StartCoroutine(RangedAttack());
        }
        else if (!(attackingMelee || attackingRanged))
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
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

    IEnumerator MeleeAttack()
    {
        attackingMelee = true;
        //high damage attack to the side
        Vector2 origin = t.position + t.TransformDirection(Vector2.right * meleeAttackRange / 2);
        print("Attacking melee"); //debug
        RaycastHit2D hit = Physics2D.CircleCast(origin, meleeAttackRange, t.TransformDirection(Vector2.right), 0, 1 << 6);
        if (hit)
        {
            print("PLAYER HIT"); //debug
            playerData.takeHit((int)meleeAttackDamage);
        }
        yield return new WaitForSeconds(meleeAttackCooldown);
        attackingMelee = false;
    }

    IEnumerator RangedAttack()
    {
        attackingRanged = true;
        Instantiate(rangedAttackObject, t.position, Quaternion.Euler(new Vector3(0,0,0)));
        yield return new WaitForSeconds(rangedAttackCooldown);
        attackingRanged = false;
    }
}
