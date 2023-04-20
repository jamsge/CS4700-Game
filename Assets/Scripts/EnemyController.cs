using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public float health = 10f;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
