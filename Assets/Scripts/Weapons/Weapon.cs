using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Interface for weapon objects
*/
public interface Weapon
{
    void UseWeapon(Transform playerTransform, AudioSource sound);
    int GetMaxAmmo();
    void SetCurrentAmmo(int ammo);
    int GetCurrentAmmo();
    string GetWeaponName();
    void Upgrade();
    void SetDamage(float damage);
    float GetDamage();
}
