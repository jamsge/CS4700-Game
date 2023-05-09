using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPhase : MonoBehaviour
{    
    public float timeUntilNextSpawn;
    public GameObject waterMonster;
    public Vector3 spawnLocation;
    bool spawning = false;
    Transform t;

    void Start()
    {
        t = gameObject.GetComponent<Transform>();   
    }

    void Update()
    {
        if (!spawning)
        {
            StartCoroutine(SpawnWaterMonster());
        }
    }

    IEnumerator SpawnWaterMonster()
    {
        spawning = true;
        yield return new WaitForSeconds(timeUntilNextSpawn);
        Instantiate(waterMonster, spawnLocation, Quaternion.Euler(new Vector3(0,0,0)));
        spawning = false;
    }
}
