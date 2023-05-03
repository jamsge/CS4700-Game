using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public PlayerData playerData;
    public float speed;
    public float damage;
    public float baseHealth;
    public float health;
    public float detectionDistance; //might be interpreted differently by different types of enemies
    public float idleMoveArea;

    void Start()
    {
        health = baseHealth;
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

    }
}
