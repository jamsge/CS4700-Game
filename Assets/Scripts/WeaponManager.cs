using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponManager : MonoBehaviour
{
    public PlayerData playerData;
    public static WeaponManager instance;
    public Weapon currentWeapon; 
    //public float maxAmmo = 0; //moved to weapon class
    //public float currentAmmo = 0;
    public Weapon[] weapons;
    public GameObject player;
    public event Action onWeaponSwitch;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        weapons = new Weapon[5];
        player = GameObject.Find("Player");
        weapons[0] = new Flamethrower();
        currentWeapon = weapons[0]; 
    }

    void Update()
    {
        player = GameObject.Find("Player");
        float input = Input.GetAxis("Mouse ScrollWheel");
        if (input != 0)
        {
            SwitchWeapon(input);
        }   
    }

    void SwitchWeapon(float input)
    {
        onWeaponSwitch?.Invoke();
        int currentWeaponIndex = Array.IndexOf(weapons, currentWeapon);
        if (input > 0)
        {
            currentWeaponIndex -= 1;
            if (currentWeaponIndex >= 0 && (weapons[currentWeaponIndex] != null))
            {
                currentWeapon = weapons[currentWeaponIndex];
            }
        }
        else if (input < 0)
        {
            currentWeaponIndex += 1;
            if (currentWeaponIndex <= 4 && (weapons[currentWeaponIndex] != null))
            {
                currentWeapon = weapons[currentWeaponIndex];
            }
        }
    }
}
