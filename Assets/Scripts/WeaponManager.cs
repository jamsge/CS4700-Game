using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public string currentWeapon = "ft"; //Flamethrower weapon code
    public float maxAmmo = 100; //Can be changed when weapon type changes
    public float currentAmmo = 100; 
    void Start()
    {
        
    }

    void Update()
    {
        switch (currentWeapon)
        {
            case "ft":
                maxAmmo = 100;
                break;
            default:
                maxAmmo = 0;
                break;
        }
    }

    void SwitchWeapon()
    {

    }
}
