using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionDetection : MonoBehaviour
{   
    Transform t;
    Rigidbody2D rb;
    Collider2D coll;

    void Start()
    {
        t = gameObject.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        coll = gameObject.GetComponent<Collider2D>();
    }

    void Update()
    {
        Vector3 origin = t.position + new Vector3(coll.bounds.extents.x, 0, 0);
        bool collided = Physics2D.Raycast(origin, t.TransformDirection(Vector3.right), 0.1f, 1 << 0); //default layer
        if (collided)
        {
            rb.velocity *= -1f; //reverse velocity on collision with wall
            t.Rotate(new Vector3(0,180,0), Space.Self); //reverse rotation
            gameObject.GetComponent<EnemyCollisionDetection>().enabled = false; //disable this script to avoid bugging (getting stuck in wall)
        }
    }
}
