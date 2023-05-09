using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : MonoBehaviour
{    
    GameObject waterMonster;
    EnemyController ec;
    WaterMonster wm;
    GameObject player;
    PlayerData playerData;
    Vector3 initialPosition;
    Rigidbody2D rb;
    void Awake()
    {
        waterMonster = GameObject.Find("WaterMonster");
        ec = waterMonster.GetComponent<EnemyController>();
        wm = waterMonster.GetComponent<WaterMonster>();
    }
    void Start()
    {
        player = ec.player;
        playerData = ec.playerData;
        initialPosition = transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(Shoot());
    }
   
    void Update()
    {
        if (Vector3.Distance(transform.position, initialPosition) >= wm.rangedAttackRange)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.1f);
        rb.velocity = waterMonster.transform.TransformDirection(Vector2.right) * wm.rangedAttackObjectSpeed;
    }
}
