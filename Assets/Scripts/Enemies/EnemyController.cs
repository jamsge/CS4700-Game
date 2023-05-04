using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public PlayerData playerData;
    public float idleSpeed;
    public float damage;
    public float baseHealth;
    public float health;
    public float detectionDistance; //might be interpreted differently by different types of enemies
    public float idleMoveAreaBounds;

    public Transform t;
    public Rigidbody2D rb;
    Vector3 initialPosition;
//    bool returnedToInitialPosition = false;

    void Awake()
    {
        t = gameObject.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        health = baseHealth;
        initialPosition = t.position;
    }

    void Update()
    {
        if (health <= 0)
        {
            OnDeath();
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
    }

    void OnDeath()
    {
        Destroy(gameObject);
        //tba maybe item drops
    }   

    public void Idle()
    {
/*         if (Mathf.Abs(initialPosition.y - t.position.y) > 0.5f)
            returnedToInitialPosition = false;
        if (!returnedToInitialPosition)
        {
            Vector3 direction = initialPosition - t.position;
            direction.Normalize();
            rb.velocity = direction * idleSpeed;
            if (Vector3.Distance(initialPosition, t.position) < 0.1f)
                returnedToInitialPosition = true;
            //t.Translate(Vector3.up * idleSpeed * Time.deltaTime * 1.2f);
        } */

        float rightBound = initialPosition.x + idleMoveAreaBounds;
        float leftBound = initialPosition.x - idleMoveAreaBounds;
        if (t.position.x >= rightBound)
        {
            t.rotation = Quaternion.Euler(new Vector3(0,180,0));
        }
        else if (t.position.x <= leftBound)
        {
            t.rotation = Quaternion.Euler(new Vector3(0,0,0));
        }
        rb.velocity = (Vector2)t.TransformDirection(Vector3.right) * idleSpeed;
    }
}
