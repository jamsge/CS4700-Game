using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAxe : Weapon
{
    public static int instanceCount = 0;
    public string weaponName;
    public float attackSpeed;
    public float damage;
    
    private bool onCooldown = false;

    public FireAxe()
    {
        this.weaponName = "Fire Axe";
        this.attackSpeed = WeaponManager.instance.fireAxeAttackSpeed;
        this.damage = WeaponManager.instance.fireAxeDamage;
        instanceCount += 1;
    }

    public void UseWeapon(Transform playerTransform){}

    public string GetWeaponName()
    {
        return this.weaponName;
    }

    public int GetMaxAmmo()
    {
        //Not implemented
        return 0;
    }

    public void SetCurrentAmmo(int ammo)
    {
        //Not implemented
    }

    public int GetCurrentAmmo()
    {
        //Not implemented
        return 0;
    }
}
