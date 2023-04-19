using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Weapon
{
    public string weaponName;
    public readonly int MAX_AMMO = 100;
    public int currentAmmo;

    public Flamethrower(){
        this.weaponName = "Flamethrower";
        this.currentAmmo = this.MAX_AMMO;
    } 
    public IEnumerator UseWeapon()
    {
        return null;
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
