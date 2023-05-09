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
    public GameObject shopButton;
    public GameObject invButton;
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
            shopButton.active = false;
            invButton.active = false;
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1.0f;
            player.GetComponent<PlayerMovementController>().enabled = true;
            player.GetComponent<PlayerCombatController>().enabled = true;
            shopButton.active = true;
            invButton.active = true;
        }
    }
}
