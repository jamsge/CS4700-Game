using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCannon : Weapon
{
    /*
    stats:
    - damage
    - range
    - knockback strength
    - cooldown
    - ammo and ammo usage
    */
    private float damage;
    private float range;
    //add knockback strength
    private int cooldown;
    private int ammoUsage;
    private int currentAmmo;
    private readonly int MAX_AMMO = 10; //tbd
    private string weaponName;

    public WaterCannon()
    {
        this.currentAmmo = this.MAX_AMMO;
        this.damage = WeaponManager.instance.waterCannonDamage;
        this.range = WeaponManager.instance.waterCannonRange;
        this.cooldown = WeaponManager.instance.waterCannonCooldown;
        this.ammoUsage = WeaponManager.instance.waterCannonAmmoUsage;
        this.weaponName = "Water Cannon";
    }

    public void UseWeapon(Transform playerTransform){} //tba

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
