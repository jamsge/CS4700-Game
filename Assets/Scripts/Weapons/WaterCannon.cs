using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCannon : Weapon
{
    public static int instanceCount = 0;
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
    private float knockbackStrength;
    private int cooldown;
    private int ammoUsage;
    private int currentAmmo;
    private readonly int MAX_AMMO = 10; //tbd
    private string weaponName;

    private bool onCooldown = false;

    public WaterCannon()
    {
        this.currentAmmo = this.MAX_AMMO;
        this.damage = WeaponManager.instance.waterCannonDamage;
        this.range = WeaponManager.instance.waterCannonRange;
        this.cooldown = WeaponManager.instance.waterCannonCooldown;
        this.ammoUsage = WeaponManager.instance.waterCannonAmmoUsage;
        this.knockbackStrength = WeaponManager.instance.waterCannonKnockbackStrength;
        this.weaponName = "Water Cannon";
        instanceCount += 1;
    }

    public void UseWeapon(Transform playerTransform)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !onCooldown)
        {
            RaycastHit2D hit = Physics2D.CircleCast(playerTransform.position + Vector3.right, 1f, playerTransform.TransformDirection(Vector2.right), range, 1 << 3);
            if (hit)
            {
                Debug.Log("HIT"); //debug
                //damage and ammo use
                hit.collider.gameObject.GetComponent<EnemyController>().health -= damage;
                currentAmmo -= ammoUsage;
                //knock back enemy
                Rigidbody2D enemyRB = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                Vector3 knockbackDirection = playerTransform.TransformDirection(Vector3.right);
                enemyRB.velocity = knockbackDirection * knockbackStrength;
                //put on cooldown
                WeaponManager.instance.StartCoroutine(PutOnCooldown());
            }
        }
    }

    private IEnumerator PutOnCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
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

    public void Upgrade()
    {
        this.damage = WeaponManager.instance.waterCannonDamageU;
        this.range = WeaponManager.instance.waterCannonRangeU;
        this.cooldown = WeaponManager.instance.waterCannonCooldownU;
        this.ammoUsage = WeaponManager.instance.waterCannonAmmoUsageU;
        this.knockbackStrength = WeaponManager.instance.waterCannonKnockbackStrengthU;
        WeaponManager.instance.waterCannonUpgraded = true;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public float GetDamage()
    {
        return this.damage;
    }
}
