using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
/*     public enum Phase
    {
        First,Second,Third
    } */

    public float baseHealth;
    public float health;

    public FirstPhase phase1;
    public SecondPhase phase2;
    public ThirdPhase phase3;

    // Start is called before the first frame update
    void Start()
    {
        health = baseHealth;
        phase2.enabled = false;
        phase3.enabled = false;
    }

    // Update switches boss phases and checks health
    void Update()
    {
        if ((health <= (baseHealth * 0.7f)) && (health > (baseHealth * 0.3f)))
        {
            phase1.enabled = false;
            phase2.enabled = true;
            phase3.enabled = false;
        }
        else if (health <= (baseHealth * 0.3f))
        {
            phase1.enabled = false;
            phase2.enabled = false;
            phase3.enabled = true;
        }
        else
        {
            phase1.enabled = true;
            phase2.enabled = false;
            phase3.enabled = false;
        }

        if (health <= 0)
        {
            OnDeath();
        }
    }

    void OnDeath()
    {
        //TBA
    }
}
