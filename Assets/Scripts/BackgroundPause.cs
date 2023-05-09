using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPause : MonoBehaviour
{
    public static bool isPaused;
    public GameObject player;
    public GameObject shop;
    public AudioSource ft;
    public AudioSource fa;
    public AudioSource tg;
    public AudioSource wc;
    void Start()
    {
       isPaused = false;
    }

    void Update()
    {
        if (shop.active)
        {
            isPaused = true;
            Time.timeScale = 0;
            player.GetComponent<PlayerMovementController>().enabled = false;
            player.GetComponent<PlayerCombatController>().enabled = false;
            ft.Stop();
            fa.Stop();
            tg.Stop();
            wc.Stop();
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1.0f;
            player.GetComponent<PlayerMovementController>().enabled = true;
            player.GetComponent<PlayerCombatController>().enabled = true;
        }
    }
}
