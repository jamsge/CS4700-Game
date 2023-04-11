using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] public PlayerData playerData;
    [SerializeField] public float maxSpeed = 10f;
    private float jumpHeight = 7f;

    float moveDirection = 0;
    bool isGrounded = true;
    BoxCollider2D g;
    BoxCollider2D mainCollider;
    Rigidbody2D r2d;
    Transform t;
    Transform ct;
    
    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        t = GetComponent<Transform>();
        g = GetComponentInChildren<BoxCollider2D>();
        mainCollider = GetComponent<BoxCollider2D>();
        ct = Camera.main.GetComponent<Transform>();
        print(g);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
        } else {
            moveDirection = 0;
        }

        if (moveDirection != 0)
        {
            if (moveDirection > 0)
            {
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
            }
            if (moveDirection < 0)
            {
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
        }

        if (t.position.x > ct.position.x){
            ct.position = new Vector3(t.position.x, ct.position.y, ct.position.z);
        }

    }

    void FixedUpdate()
    {
        r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);

        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != mainCollider)
                {
                    isGrounded = true;
                    break;
                }
            }
        }
    }
}
