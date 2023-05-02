using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    public PlayerData playerData;
    public float meleeCooldown = 0.5f;
    public float meleeDistance = 1f;
    public float meleeDamageBase = 1f;
    float meleeDamage;
    Transform t;
    int layerMask;
    bool meleeOnCooldown = false;

    void Start()
    {
        t = GetComponent<Transform>();
        layerMask = 1 << 3; //Enemy layer
        meleeDamage = meleeDamageBase;
    }
    
    void Update()
    {
        //update damage - check for upgrade
        meleeDamage = meleeDamageBase + playerData.damageBoost;

        Debug.DrawRay(t.position, t.TransformDirection(Vector3.right * meleeDistance), Color.white); //debug - roughly visualizes melee attack distance
        if (Input.GetKeyDown(KeyCode.Q) && !meleeOnCooldown)
        {
            StartCoroutine(MeleeAttack());
        }
    }

    IEnumerator MeleeAttack()
    {
        meleeOnCooldown = true;
        //RaycastHit2D hit = Physics2D.Raycast(new Vector2(t.position.x, t.position.y), t.TransformDirection(Vector3.right), meleeDistance, layerMask);
        RaycastHit2D hit = Physics2D.CircleCast(new Vector2(t.position.x, t.position.y), 1f, t.TransformDirection(Vector3.right), meleeDistance/2, layerMask);
        print("hitting"); //debug
        if (hit)
        {
            print("HIT"); //debug
            print(hit.collider.gameObject); //debug - print whatever object was hit
            hit.collider.gameObject.GetComponent<EnemyController>().health -= meleeDamage; //decrease enemy health; thanks to the enemy layer, the object that was hit is always an enemy and has health attribute
        }
        yield return new WaitForSeconds(meleeCooldown);
        meleeOnCooldown = false;
    }
}
