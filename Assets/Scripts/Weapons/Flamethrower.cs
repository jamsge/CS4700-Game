using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
Base weapon: Flamethrower
other weapon classes might look pretty much the same with
only the UseWeapon method different
*/

public class Flamethrower : Weapon
{
        //make sure never more than 1
        public static int instanceCount = 0;
        
        //weapon stats
        private string weaponName;
        private readonly int MAX_AMMO = 100;
        private int currentAmmo;
        private float damage;
        private float range;
        private int cooldown; //'cooldown' is the interval of time when flamethrower doesn't deal damage
        private int ammoUsage;

        private bool onCooldown = false;

    private bool soundPlaying = false;

        public Flamethrower(){
            this.weaponName = "Flamethrower";
            this.currentAmmo = this.MAX_AMMO;
            this.damage = WeaponManager.instance.flamethrowerDamage; //get weapon damage from WeaponManager
            this.range = WeaponManager.instance.flamethrowerRange; //get range from WeaponManager
            this.cooldown = WeaponManager.instance.flamethrowerCooldown; //get cooldown from WeaponManager
            this.ammoUsage = WeaponManager.instance.flamethrowerAmmoUsage;
            instanceCount += 1;
        }
    public void UseWeapon(Transform playerTransform, AudioSource sound)
    {
        // Used when holding left mouse button
        // deals damage OVER TIME (some damage every interval)
        // stops when button released
        if (Input.GetKey(KeyCode.Mouse0) && (currentAmmo > 0))
        {
            WeaponManager.instance.weaponAnimator.SetBool("usingFlamethrower", true);
            WeaponManager.instance.playerAnimator.SetBool("usingWeapon", true);

            if (!soundPlaying)
            {
                sound.Play();
                soundPlaying = true;
            }
            
            //cast a circle from in front of the player while button is being held down
            RaycastHit2D hit = Physics2D.CircleCast(new Vector2(playerTransform.position.x + 1, playerTransform.position.y), 1f, playerTransform.TransformDirection(Vector2.right), range, 1 << 3);
            if (!onCooldown)
            {
                WeaponManager.instance.StartCoroutine(DealDamage(hit));
            }
            
        }
        else
        {
            WeaponManager.instance.weaponAnimator.SetBool("usingFlamethrower", false);
            WeaponManager.instance.playerAnimator.SetBool("usingWeapon", false);
            soundPlaying = false;
            sound.Stop();
        }
    }

    //deal damage then wait till damaging again
    private IEnumerator DealDamage(RaycastHit2D hit)
        {
            onCooldown = true;
            //ammo usage 
            currentAmmo -= ammoUsage;
            if (hit)
            {
                //deal damage
                try
                {
                    hit.collider.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
                }
                catch (Exception e)
                {
                    hit.collider.gameObject.GetComponent<BossController>().health -= damage;
                }
                Debug.Log("HIT"); //debug
            }
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
            this.damage = WeaponManager.instance.flamethrowerDamageU;
            this.ammoUsage = WeaponManager.instance.flamethrowerAmmoUsageU;
            this.cooldown = WeaponManager.instance.flamethrowerCooldownU;
            this.range = WeaponManager.instance.flamethrowerRangeU;
            WeaponManager.instance.flamethrowerUpgraded = true;
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

