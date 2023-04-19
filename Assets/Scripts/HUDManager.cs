using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public PlayerData playerData;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI currentAmmoText;
    public TextMeshProUGUI maxAmmoText;


    void Start(){
        // subscribe to health events with corresponding functions
        playerData.onPlayerHealthSet += updateHealth;
        playerData.onPlayerHit += updateHealth;
        WeaponManager.instance.onWeaponSwitch += updateWeaponData;

        // Initialize HUD text values
        updateHealth(playerData.health);
        updateWeaponData();
    }

    void updateHealth(int health){
        healthText.text = health.ToString();
    }
    
    void updateHealth(){
        healthText.text = playerData.health.ToString();
    }

    void updateWeaponData()
    {
        currentAmmoText.text = WeaponManager.instance.currentWeapon.GetCurrentAmmo()+"";
        maxAmmoText.text = "/ " + WeaponManager.instance.currentWeapon.GetMaxAmmo();
    }
}
