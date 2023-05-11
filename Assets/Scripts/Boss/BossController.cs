using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
/*     public enum Phase
    {
        First,Second,Third
    } */

    public GameObject player;
    public PlayerData playerData;

    public float baseHealth;
    public float health;
    public float speed;

    public FirstPhase phase1;
    public SecondPhase phase2;
    public ThirdPhase phase3;

    Transform t;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        health = baseHealth;
        phase1.enabled = true;
        phase2.enabled = false;
        phase3.enabled = false;
        t = gameObject.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update switches boss phases, checks health, and makes boss move towards player
    void Update()
    {
        t.Translate(Vector3.right * speed * Time.deltaTime);

        //Face player
        if (player.transform.position.x > t.position.x)
        {
            t.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else
        {
            t.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }

        if (health <= 0)
        {
            OnDeath();
        }

/*         //check for phase 3
        if (health <= (baseHealth * 0.3f))
        {
            phase3.enabled = true;
        } */
    }

    void OnDeath()
    {
        Destroy(gameObject);
    }
}
