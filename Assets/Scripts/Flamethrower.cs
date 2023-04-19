using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Weapon
{
    public string weaponID;
    public int maxAmmo;
    public GameObject player;
    public Flamethrower(){
        this.weaponID = "ft";
        this.maxAmmo = 100;
    } 
    public IEnumerator UseWeapon()
    {
        return null;
    }
}
