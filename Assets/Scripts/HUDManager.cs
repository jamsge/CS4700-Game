using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public PlayerData playerData;
    public TextMeshProUGUI healthText;


    void Start(){
        playerData.onPlayerHealthSet += updateHealth;
        playerData.onPlayerHit += updateHealth;
        updateHealth(playerData.health);
    }

    void updateHealth(int health){
        healthText.text = health.ToString();
    }

    void updateHealth(){
        healthText.text = playerData.health.ToString();
    }
}
