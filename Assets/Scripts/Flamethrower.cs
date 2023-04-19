using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Base weapon: Flamethrower
other weapon classes might look pretty much the same with
only the UseWeapon method different
*/
public class Flamethrower : Weapon
{
    public string weaponName;
    public readonly int MAX_AMMO = 100;
    public int currentAmmo;

    public Flamethrower(){
        this.weaponName = "Flamethrower";
        this.currentAmmo = this.MAX_AMMO;
    } 
    public void UseWeapon(Vector2 playerPosition)
    {
        // Used when holding left mouse button
        // deals damage OVER TIME (some damage every interval)
        // stops when button released
    }

    public int GetMaxAmmo()
    {
        return this.MAX_AMMO;
    }

    public void SetCurrentAmmo(int ammo)
    {
        this.currentAmmo = ammo;
    }

    public int GetCurrentAmmo()
    {
        return this.currentAmmo;
    }

    public string GetWeaponName()
    {
        return this.weaponName;
    }
}
