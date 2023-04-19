using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Interface for weapon objects
*/
public interface Weapon
{
    void UseWeapon(Vector2 playerPosition);
    int GetMaxAmmo();
    void SetCurrentAmmo(int ammo);
    int GetCurrentAmmo();
    string GetWeaponName();
}
