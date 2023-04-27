using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAxe : Weapon
{
    public static int instanceCount = 0;

    private string weaponName;
    private float attackSpeed;
    private float damage;
    private float attackRadius;
    
    private bool onCooldown = false;

    public FireAxe()
    {
        this.weaponName = "Fire Axe";
        this.attackSpeed = WeaponManager.instance.fireAxeAttackSpeed;
        this.damage = WeaponManager.instance.fireAxeDamage;
        this.attackRadius = WeaponManager.instance.fireAxeAttackRadius;
        instanceCount += 1;
    }

    public void UseWeapon(Transform playerTransform)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && (!onCooldown))
        {
            WeaponManager.instance.StartCoroutine(Use(playerTransform));
        }
    }
    private IEnumerator Use(Transform t)
    {
        //put on cooldown
        onCooldown = true;

        //now only works on enemies, need to add obstacle layer
        RaycastHit2D hit = Physics2D.CircleCast(new Vector3(t.position.x, t.position.y), attackRadius, t.TransformDirection(Vector2.right), 0, 1 << 3);
        if (hit)
        {
            //deal damage
            hit.collider.gameObject.GetComponent<EnemyController>().health -= damage;
            Debug.Log("HIT"); //debug
        }
        yield return new WaitForSeconds(10 / attackSpeed); //higher attack speed => lower cooldown, this might be changed later
        onCooldown = false;
    }

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

    public void Upgrade(){}
}
