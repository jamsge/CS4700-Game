using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMonster : MonoBehaviour
{   
    public Sprite idleSprite;
    public Sprite attackSprite;
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
    SpriteRenderer rend;
    GameObject player;
    PlayerData playerData;
    bool playerDetected;
    bool attackingMelee;
    bool attackingRanged;
    float positionDiff;

    [SerializeField] private AudioSource detectSoundEffect;

    float soundTimer = 0;
    bool soundPlayed = false;
    void Start()
    {
        waterBallDamage = rangedAttackDamage;
        rend = gameObject.GetComponent<SpriteRenderer>();
        t = ec.t;
        rb = ec.rb;
        coll = gameObject.GetComponent<Collider2D>();
        player = ec.player;
        playerData = ec.playerData;
    }

    void Update()
    {
        //check if player detected
        positionDiff = Mathf.Abs(player.transform.position.x - t.position.x);
        if (positionDiff <= ec.detectionDistance && (Mathf.Abs(player.transform.position.y - t.position.y) <= 1))
        {
            playerDetected = true;
        }
        else
        {
            playerDetected = false;
        }

        if (soundPlayed)
        {
            soundTimer += Time.deltaTime;
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
        
        if (!inMeleeAttackRange)
        {
            MoveTowardsPlayer();
        }
        else
        {
            rb.velocity = new Vector3(0,0,0);
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
        rend.sprite = attackSprite;
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
        yield return new WaitForSeconds(0.5f);
        rend.sprite = idleSprite;
        yield return new WaitForSeconds(meleeAttackCooldown);
        attackingMelee = false;
    }

    IEnumerator RangedAttack()
    {
        rend.sprite = attackSprite;
        if (detectSoundEffect != null && (soundTimer >= 15 || !soundPlayed))
        {
            soundPlayed = true;
            soundTimer = 0;
            detectSoundEffect.Play();
        }
        attackingRanged = true;
        Instantiate(rangedAttackObject, t.position, Quaternion.Euler(new Vector3(0,0,0)));
        yield return new WaitForSeconds(0.5f);
        rend.sprite = idleSprite;
        yield return new WaitForSeconds(rangedAttackCooldown);
        attackingRanged = false;
    }
}
