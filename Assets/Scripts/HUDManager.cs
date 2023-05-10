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
    public TextMeshProUGUI weaponNameText;
    [SerializeField] private RectTransform fillRectTransform;

    void Start()
    {
        // Subscribe to health events with corresponding functions
        playerData.onPlayerHealthSet += OnPlayerHealthSet;
        playerData.onPlayerHit += OnPlayerHealthChange;
        WeaponManager.instance.onWeaponSwitch += UpdateWeaponData;
        WeaponManager.instance.onWeaponUse += UpdateWeaponData;

        // Initialize HUD text values
        OnPlayerHealthSet(playerData.health);
        UpdateWeaponData();
    }

    void OnPlayerHealthSet(int health)
    {
        healthText.text = health.ToString();

        // Adjust fill amount based on health percentage
        float fillAmount = (float)health / playerData.baseHealth;
        fillRectTransform.localScale = new Vector3(fillAmount, 1f, 1f);
    }

    void OnPlayerHealthChange()
    {
        OnPlayerHealthSet(playerData.health);
    }

    void UpdateWeaponData()
    {
        currentAmmoText.text = WeaponManager.instance.currentWeapon.GetCurrentAmmo().ToString();
        maxAmmoText.text = "/ " + WeaponManager.instance.currentWeapon.GetMaxAmmo().ToString();
        weaponNameText.text = WeaponManager.instance.currentWeapon.GetWeaponName();
    }
}
