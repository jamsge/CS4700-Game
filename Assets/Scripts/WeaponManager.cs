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
    public List<Weapon> weapons; //keep weapon loadout as an array
    public GameObject player;
    public event Action onWeaponSwitch;

    //Weapon damage and range - can be modified in unity
    //FLAMETHROWER
    public float flamethrowerDamage;
    public float flamethrowerRange;
    //more weapons tba 

    void Awake()
    {   //This makes sure there is always one instance of WeaponManager
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
        //Temporary until we make weapon inventory and more weapons
        weapons = new List<Weapon>();
        player = GameObject.Find("Player");
        weapons.Add(new Flamethrower());
        currentWeapon = weapons[0]; 
    }

    void Update()
    {
        player = GameObject.Find("Player"); //player's position might be needed when using a weapon
        float input = Input.GetAxis("Mouse ScrollWheel");  //switch weapons with scroll
        if (input != 0)
        {
            SwitchWeapon(input);
        }
        UseWeapon(); //keeps calling UseWeapon, input will be checked in each weapon's class   
    }

    void SwitchWeapon(float input)
    {
        onWeaponSwitch?.Invoke();
        int currentWeaponIndex = weapons.IndexOf(currentWeapon);
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
            if (currentWeaponIndex <= weapons.Count && (weapons[currentWeaponIndex] != null))
            {
                currentWeapon = weapons[currentWeaponIndex];
            }
        }
    }

    void UseWeapon(){
        currentWeapon.UseWeapon(player.transform);
    }
}
