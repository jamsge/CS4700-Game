using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] public PlayerData playerData;
    [SerializeField] public float defaultMaxSpeed;
    [SerializeField] public float sprintMaxSpeed;
    [SerializeField] public float dashSpeed = 20f;
    [SerializeField] public float dashDistance = 2f;
    [SerializeField] public float dashCooldown = 1f;
    [SerializeField] public float jumpHeight = 7f;
    //public Camera cam;
    private float maxSpeed;
    private bool dashOnCooldown = false;

    float moveDirection = 0;
    bool isGrounded = true;
    BoxCollider2D g;
    BoxCollider2D mainCollider;
    Rigidbody2D r2d;
    Transform t;
    bool dashing = false;
    
    // Start is called before the first frame update
    void Start()
    {
        defaultMaxSpeed = playerData.defaultMaxSpeed;
        sprintMaxSpeed = 2 * defaultMaxSpeed;
        maxSpeed = defaultMaxSpeed;
        r2d = GetComponent<Rigidbody2D>();
        t = GetComponent<Transform>();
        g = GetComponentInChildren<BoxCollider2D>();
        mainCollider = GetComponent<BoxCollider2D>();
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

        //sprint
        sprintMaxSpeed = 2 * defaultMaxSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            maxSpeed = sprintMaxSpeed;
        }
        else
        {
            maxSpeed = defaultMaxSpeed;
        }

        //dash
        if (Input.GetKeyDown(KeyCode.LeftControl) && !dashOnCooldown)
        {
            StartCoroutine(Dash());
        }

        if (moveDirection != 0)
        {
            if (moveDirection > 0)
            {
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
                t.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            if (moveDirection < 0)
            {
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
                t.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
        }
    }

    void FixedUpdate()
    {
        if (!dashing) // check whether currently in a dash
        {
            r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);
        }

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

    IEnumerator Dash()
    {
        float dashTime = dashDistance / 10; //dashDistance determines how long this coroutine waits before returning to base velocity
        Vector2 oldVelocity = r2d.velocity;
        r2d.velocity = r2d.velocity.normalized * dashSpeed;
        dashing = true;
        yield return new WaitForSeconds(dashTime);
        r2d.velocity = oldVelocity;
        dashing = false;
        dashOnCooldown = true;
        yield return new WaitForSeconds(dashCooldown);
        dashOnCooldown = false;
    }
}
