using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] public float meleeCooldown = 0.5f;
    [SerializeField] public float meleeDistance = 2f;
    // Update is called once per frame
    Transform t;
    int layerMask;
    bool meleeOnCooldown = false;
    void Start()
    {
        t = GetComponent<Transform>();
        layerMask = 1 << 3; //Enemy layer
    }
    void Update()
    {
        Debug.DrawRay(t.position, t.TransformDirection(100,0,0), Color.white); //debug
        if (Input.GetKeyDown(KeyCode.Q) && !meleeOnCooldown)
        {
            StartCoroutine(MeleeAttack());
        }
    }

    IEnumerator MeleeAttack()
    {
        meleeOnCooldown = true;
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(t.position.x, t.position.y), t.TransformDirection(Vector3.right), meleeDistance, layerMask);
        print("hitting"); //debug
        if (hit)
        {
            print("HIT"); //debug
        }
        yield return new WaitForSeconds(meleeCooldown);
        meleeOnCooldown = false;
    }
}
