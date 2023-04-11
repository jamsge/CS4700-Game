using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public PlayerData playerData;
    public TextMeshProUGUI healthText;


    void Start(){
        // subscribe to health events with corresponding functions
        playerData.onPlayerHealthSet += updateHealth;
        playerData.onPlayerHit += updateHealth;

        // Initialize HUD text values
        updateHealth(playerData.health);
    }

    void updateHealth(int health){
        healthText.text = health.ToString();
    }
    
    void updateHealth(){
        healthText.text = playerData.health.ToString();
    }
}
