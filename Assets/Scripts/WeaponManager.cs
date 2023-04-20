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
    public event Action onWeaponUse;

    //Weapon stats - can be modified in unity
    //FLAMETHROWER
    [Header("Flamethrower Stats")]
    public float flamethrowerDamage = 3;
    public float flamethrowerRange;
    public int flamethrowerCooldown = 1;
    public int flamethrowerAmmoUsage = 10;
    //visualizng flamethrower use for debug purposes
    //temporarily create a non-physical circle object in front of player when flamethrower is used
    public GameObject ftVisualization;

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
        //debug
        ftVisualization.GetComponent<Renderer>().enabled = false;

        //Temporary until we make weapon inventory and more weapons
        weapons = new List<Weapon>(5);
        player = GameObject.Find("Player");
        weapons.Add(new Flamethrower());
        currentWeapon = weapons[0]; 
    }

    void Update()
    {
        //debug
        ftVisualization.transform.localScale = new Vector3(flamethrowerRange, 1f, 1f);
        ftVisualization.transform.position = player.transform.position + player.transform.TransformDirection(new Vector3(flamethrowerRange / 2, 0, 0));

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
            if (currentWeaponIndex < weapons.Count && (weapons[currentWeaponIndex] != null))
            {
                currentWeapon = weapons[currentWeaponIndex];
            }
        }
    }

    void UseWeapon(){
        currentWeapon.UseWeapon(player.transform);
        onWeaponUse?.Invoke();
    }
}
