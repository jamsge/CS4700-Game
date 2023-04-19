using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon currentWeapon; 
    //public float maxAmmo = 0; //moved to weapon class
    //public float currentAmmo = 0;
    public Weapon[] weapons;
    public GameObject player;

    void Start()
    {
        weapons = new Weapon[5];
        player = GameObject.Find("Player");
        weapons[0] = new Flamethrower();
        currentWeapon = weapons[0]; 
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") >= 0.1)
        {
            SwitchWeapon();
        }   
        player = GameObject.Find("Player");
    }

    void SwitchWeapon()
    {
        print("switching"); //debug
    }
}
