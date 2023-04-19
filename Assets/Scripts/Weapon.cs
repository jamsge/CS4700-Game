using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Weapon
{
    IEnumerator UseWeapon();
    int GetMaxAmmo();
    void SetCurrentAmmo(int ammo);
    int GetCurrentAmmo();
    string GetWeaponName();
}
