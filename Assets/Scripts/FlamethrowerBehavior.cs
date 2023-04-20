using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Windows;

/*
Base weapon: Flamethrower
other weapon classes might look pretty much the same with
only the UseWeapon method different
*/
public class FlamethrowerBehavior : MonoBehaviour
{
    public class Flamethrower : Weapon
    {
        //weapon stats
        public string weaponName;
        public readonly int MAX_AMMO = 100;
        public int currentAmmo;
        public float damage;
        public float range;
        public int cooldown; //'cooldown' is the interval of time when flamethrower doesn't deal damage

        private bool onCooldown = false;

        public Flamethrower(){
            this.weaponName = "Flamethrower";
            this.currentAmmo = this.MAX_AMMO;
            this.damage = WeaponManager.instance.flamethrowerDamage; //get weapon damage from WeaponManager
            this.range = WeaponManager.instance.flamethrowerRange; //get range from WeaponManager
            this.cooldown = WeaponManager.instance.flamethrowerCooldown; //get cooldown from WeaponManager
        } 
        public void UseWeapon(Transform playerTransform)
        {
            // Used when holding left mouse button
            // deals damage OVER TIME (some damage every interval)
            // stops when button released
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Debug.Log("using flamethrower"); //debug
                WeaponManager.instance.ftVisualization.GetComponent<Renderer>().enabled = true; //debug
                //cast a circle from in front of the player while button is being held down
                RaycastHit2D hit = Physics2D.CircleCast(new Vector2(playerTransform.position.x + 1, playerTransform.position.y), 1f, playerTransform.TransformDirection(Vector2.right), range, 1 << 3);
                if (hit)
                {
                    if (!onCooldown)
                    {
                        WeaponManager.instance.StartCoroutine(DealDamage(hit));
                    }
                }
            }
            else
            {
                WeaponManager.instance.ftVisualization.GetComponent<Renderer>().enabled = false; //debug
            }
        }

        //deal damage then wait till damaging again
        private IEnumerator DealDamage(RaycastHit2D hit)
        {
            onCooldown = true;
            hit.collider.gameObject.GetComponent<EnemyController>().health -= damage;
            Debug.Log("HIT"); //debug
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
    }
}
