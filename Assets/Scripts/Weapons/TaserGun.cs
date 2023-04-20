using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserGun : Weapon
{
    //make sure never more than 1
    public static int instanceCount = 0;

    //weapon stats
    public string weaponName;
    public readonly int MAX_AMMO = 5;
    public int currentAmmo;
    public float damage;
    public float range;
    public float duration;
    public int ammoUsage;

    private bool onCooldown = false;

    public TaserGun()
    {
        this.weaponName = "Taser Gun";
        this.currentAmmo = this.MAX_AMMO;
        this.damage = WeaponManager.instance.taserGunDamage;
        this.range = WeaponManager.instance.taserGunRange;
        this.duration = WeaponManager.instance.taserGunDuration;
        this.ammoUsage = WeaponManager.instance.taserGunAmmoUsage;
    }

    public void UseWeapon(Transform playerTransform){} //TBA

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
