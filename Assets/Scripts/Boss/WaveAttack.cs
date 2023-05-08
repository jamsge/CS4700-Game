using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAttack : MonoBehaviour
{    
    GameObject player;
    PlayerData playerData;
    SecondPhase s;
    float attackRange;
    float attackHeight;
    float attackDamage;
    float attackLaunchStrength;
    Transform t;
    Collider2D coll;

    void Start()
    {
        player = gameObject.GetComponent<BossController>().player;
        playerData = gameObject.GetComponent<BossController>().playerData;
        s = gameObject.GetComponent<SecondPhase>();
        attackDamage = s.attackDamage;
        attackHeight = s.attackHeight;
        attackRange = s.attackRange;
        attackLaunchStrength = s.attackLaunchStrength;
        t = gameObject.GetComponent<Transform>();
        coll = gameObject.GetComponent<Collider2D>();
    }

    void Update()
    {
        Vector2 rayOrigin = new Vector2(t.position.x, t.position.y - coll.bounds.extents.y);
        bool backDown = Physics2D.Raycast(rayOrigin, Vector2.down, 0.1f, 1 << 7);
        if (backDown)
        {
            print("wave attack");
            Vector2 originLeft = new Vector2(t.position.x - coll.bounds.extents.x, t.position.y - coll.bounds.extents.y + attackHeight/2);
            Vector2 originRight = new Vector2(t.position.x - coll.bounds.extents.x, t.position.y - coll.bounds.extents.y + attackHeight/2);
            RaycastHit2D hitLeft = Physics2D.CircleCast(originLeft, attackHeight, Vector2.left, attackRange, 1 << 6);
            RaycastHit2D hitRight = Physics2D.CircleCast(originRight, attackHeight, Vector2.right, attackRange, 1 << 6);
            if (hitLeft || hitRight)
            {
                print("player hit");
                Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();
                playerRB.velocity = Vector3.up * attackLaunchStrength;
                playerData.takeHit((int)attackDamage);
            }
            s.attackCounter++;
            gameObject.GetComponent<WaveAttack>().enabled = false;
        }
    }
}
