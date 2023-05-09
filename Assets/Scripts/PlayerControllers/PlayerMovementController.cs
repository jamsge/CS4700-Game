using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementController : MonoBehaviour
{
    public Animator animator;
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
    SpriteRenderer rend;
    bool dashing = false;
    [SerializeField] private AudioSource walkingSoundEffect;
    [SerializeField] private AudioSource dashSoundEffect;
    bool walkingSoundPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        playerData.onPlayerHit += ChangeColor;
        defaultMaxSpeed = playerData.defaultMaxSpeed;
        sprintMaxSpeed = 2 * defaultMaxSpeed;
        maxSpeed = defaultMaxSpeed;
        r2d = GetComponent<Rigidbody2D>();
        t = GetComponent<Transform>();
        g = GetComponentInChildren<BoxCollider2D>();
        mainCollider = GetComponent<BoxCollider2D>();
        print(g);
        rend = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
        //flip sprite to face correct direction
        if (moveDirection == -1)
        {
            rend.flipX = true;
        }
        else if (moveDirection == 1)
        {
            rend.flipX = false;
        }

        defaultMaxSpeed = playerData.defaultMaxSpeed; //update speed in case boost is used

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            if (!walkingSoundPlaying && walkingSoundEffect != null)
            {
                if (isGrounded) {
                    walkingSoundEffect.Play();
                    walkingSoundPlaying = true;
                }
            }
                
            moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;

            animator.SetBool("walking", true);

        }
        else {
            if(walkingSoundEffect != null)
                walkingSoundEffect.Stop();
            walkingSoundPlaying = false;
            moveDirection = 0;
            animator.SetBool("walking", false);
        }

        if (!isGrounded) {
            if (walkingSoundEffect != null)
                walkingSoundEffect.Stop();
            walkingSoundPlaying = false;
        }

        //sprint
        sprintMaxSpeed = 2 * defaultMaxSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            walkingSoundEffect.pitch = 1.2f;
            maxSpeed = sprintMaxSpeed;
            if (r2d.velocity.x > 0)
                animator.SetBool("sprinting", true);
        }
        else
        {
            walkingSoundEffect.pitch = 1.0f;
            maxSpeed = defaultMaxSpeed;
            animator.SetBool("sprinting", false);
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
        if(dashSoundEffect != null)
            dashSoundEffect.Play();
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

    void ChangeColor()
    {
        StartCoroutine(TurnRed());
    }

    IEnumerator TurnRed()
    {
        rend.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        rend.color = Color.white;
    }

/*     void CheckAnimation()
    {
        if (Mathf.Abs(r2d.velocity.x) >= (sprintMaxSpeed))
        {
            animator.SetBool("sprinting", true);
            animator.SetBool("walking", false);
        }
        else if (Mathf.Abs(r2d.velocity.x) > 0)
        {
            animator.SetBool("sprinting", false);
            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("sprinting", false);
            animator.SetBool("walking", false);
        }
    } */
}
