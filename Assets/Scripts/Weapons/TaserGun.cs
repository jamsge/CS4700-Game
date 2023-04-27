using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserGun : Weapon
{
    //make sure never more than 1
    public static int instanceCount = 0;

    //weapon stats
    private string weaponName;
    private readonly int MAX_AMMO = 5;
    private int currentAmmo;
    private float damage;
    private float range;
    private float cooldown;
    private float stunDuration;
    private int ammoUsage;

    private bool onCooldown = false;

    public TaserGun()
    {
        this.weaponName = "Taser Gun";
        this.currentAmmo = this.MAX_AMMO;
        this.damage = WeaponManager.instance.taserGunDamage;
        this.range = WeaponManager.instance.taserGunRange;
        this.stunDuration = WeaponManager.instance.taserGunStunDuration;
        this.ammoUsage = WeaponManager.instance.taserGunAmmoUsage;
        instanceCount += 1;
    }

    public void UseWeapon(Transform playerTransform)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && (!onCooldown))
        {
            currentAmmo -= ammoUsage;
            //cast a long ray
            RaycastHit2D hit = Physics2D.Raycast(playerTransform.position, playerTransform.TransformDirection(Vector2.right), range, 1 << 3);
            if (hit)
            {
                Debug.Log("HIT"); //debug
                // deal damage
                hit.collider.gameObject.GetComponent<EnemyController>().health -= damage;
                //stun enemy
                WeaponManager.instance.StartCoroutine(StunEnemy(hit)); //tba
                // put on cooldonw
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

    //enemy stun
    private IEnumerator StunEnemy(RaycastHit2D hit)
    {
        Rigidbody2D enemyRB = hit.collider.gameObject.GetComponent<Rigidbody2D>();
        enemyRB.constraints = RigidbodyConstraints2D.FreezeAll; //freeze enemy position and rotation (only movement for now)
        yield return new WaitForSeconds(stunDuration);
        enemyRB.constraints = RigidbodyConstraints2D.None;
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
