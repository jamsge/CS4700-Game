using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponManager : MonoBehaviour
{
    public PlayerData playerData;
    public static WeaponManager instance;
    public Weapon currentWeapon; 
    public List<Weapon> weapons; //keep weapon loadout as a list
    public GameObject player;
    public event Action onWeaponSwitch;
    public event Action onWeaponUse;
    Transform playerT; //player's transform

    [Header("Weapon Avaiability")]
    public bool flamethrower;
    public bool fireAxe;
    public bool waterCannon;
    public bool taserGun;

    [Header("Weapon Upgraded")]
    public bool flamethrowerUpgraded = false;
    public bool fireAxeUpgraded = false;
    public bool waterCannonUpgraded = false;
    public bool taserGunUpgraded = false;

    //Weapon stats - can be modified in unity
    //FLAMETHROWER
    [Header("Flamethrower Stats")]
    public float flamethrowerDamage;
    public float flamethrowerRange;
    public int flamethrowerCooldown;
    public int flamethrowerAmmoUsage;

    //more weapons tba 

    //FIRE AXE
    [Header("Fire Axe Stats")]
    public float fireAxeAttackSpeed;
    public float fireAxeDamage;
    public float fireAxeAttackRadius;

    [Header("Taser Gun Stats")]
    public float taserGunDamage;
    public float taserGunRange;
    public float taserGunStunDuration;
    public float taserGunCooldown;
    public int taserGunAmmoUsage;

    [Header("Water Cannon Stats")]
    public float waterCannonDamage;
    public float waterCannonRange;
    public float waterCannonKnockbackStrength;
    public int waterCannonCooldown;
    public int waterCannonAmmoUsage;

    [Header("Audio")]
    [SerializeField] public AudioSource flamethrowerSoundEffect;
    [SerializeField] public AudioSource fireAxeSoundEffect;
    [SerializeField] public AudioSource taserGunSoundEffect;
    [SerializeField] public AudioSource waterCannonSoundEffect;

    //UPGRADE STATS
    [Header("Flamethrower Upgraded Stats")]
    public float flamethrowerDamageU;
    public float flamethrowerRangeU;
    public int flamethrowerCooldownU;
    public int flamethrowerAmmoUsageU;

    [Header("Fire Axe Upgraded Stats")]
    public float fireAxeAttackSpeedU;
    public float fireAxeDamageU;
    public float fireAxeAttackRadiusU;

    [Header("Taser Gun Upgraded Stats")]
    public float taserGunDamageU;
    public float taserGunRangeU;
    public float taserGunStunDurationU;
    public float taserGunCooldownU;
    public int taserGunAmmoUsageU;

    [Header("Water Cannon Upgraded Stats")]
    public float waterCannonDamageU;
    public float waterCannonRangeU;
    public float waterCannonKnockbackStrengthU;
    public int waterCannonCooldownU;
    public int waterCannonAmmoUsageU;


    

    void Awake()
    {   //This makes sure there is always one instance of WeaponManager
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        weapons = new List<Weapon>(5);
        player = GameObject.Find("Player");
        playerT = player.GetComponent<Transform>();
        CheckWeaponAvailability();
        currentWeapon = weapons[0];
    }

    void Update()
    {
        //check available weapons
        CheckWeaponAvailability();

        player = GameObject.Find("Player"); //player's position might be needed when using a weapon

        float input = Input.GetAxis("Mouse ScrollWheel");  //switch weapons with scroll
        if (input != 0)
        {
            SwitchWeapon(input);
        }

        UseWeapon(); //keeps calling UseWeapon, input will be checked in each weapon's class as it might differ among different weapons

        //input for weapon reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    //Switch weapons with scroll
    void SwitchWeapon(float input)
    {
        int currentWeaponIndex = weapons.IndexOf(currentWeapon);
        if (input > 0)
        {
            currentWeaponIndex -= 1;
            if (currentWeaponIndex >= 0 && (weapons[currentWeaponIndex] != null))
            {
                currentWeapon = weapons[currentWeaponIndex];
            }
        }
        else if (input < 0)
        {
            currentWeaponIndex += 1;
            if (currentWeaponIndex < weapons.Count && (weapons[currentWeaponIndex] != null))
            {
                currentWeapon = weapons[currentWeaponIndex];
            }
        }
        onWeaponSwitch?.Invoke();
    }

    //This method calls the current weapon's UseWeapon method
    void UseWeapon(){
        if (currentWeapon != null)
        {
            if (currentWeapon.GetWeaponName() == "Flamethrower")
            {
                currentWeapon.UseWeapon(playerT, flamethrowerSoundEffect);
            }
            else if (currentWeapon.GetWeaponName() == "Fire Axe")
            {
                currentWeapon.UseWeapon(playerT, fireAxeSoundEffect);
            }
            else if (currentWeapon.GetWeaponName() == "Taser Gun")
            {
                currentWeapon.UseWeapon(playerT, taserGunSoundEffect);
            }
            else if (currentWeapon.GetWeaponName() == "Water Cannon")
            {
                currentWeapon.UseWeapon(playerT, waterCannonSoundEffect);
            }
            onWeaponUse?.Invoke();
        }
    }

    //Checks whether weapons are already available, if yes, adds them to weapon list
    void CheckWeaponAvailability()
    {
        if (flamethrower && (Flamethrower.instanceCount == 0))
        {
            weapons.Add(new Flamethrower());
        }
        if (fireAxe && (FireAxe.instanceCount == 0))
        {
            weapons.Add(new FireAxe());
        }
        if (taserGun && (TaserGun.instanceCount == 0))
        {
            weapons.Add(new TaserGun());
        }
        if (waterCannon && (WaterCannon.instanceCount == 0))
        {
            weapons.Add(new WaterCannon());
        }
        //more tba
    }

    //Gizmos for visualization
    void OnDrawGizmos()
    {
        if (currentWeapon != null)
        {
            //flamethrower hitbox (approximate)
            if (currentWeapon.GetWeaponName() == "Flamethrower")
            {
                Gizmos.DrawWireCube(playerT.position + playerT.TransformDirection(flamethrowerRange/2, 0, 0), new Vector3(flamethrowerRange, 1, 1));
            }
            //fireaxe hitbox
            else if (currentWeapon.GetWeaponName() == "Fire Axe")
            {
                Gizmos.DrawWireSphere(playerT.position, fireAxeAttackRadius);
            }
            //taser gun hitbox(approximate)
            else if (currentWeapon.GetWeaponName() == "Taser Gun")
            {
                Gizmos.DrawLine(playerT.position, playerT.position + playerT.TransformDirection(taserGunRange,0,0));
            }
            //water cannon hitbox (approximate)
            else if (currentWeapon.GetWeaponName() == "Water Cannon")
            {
                Gizmos.DrawWireCube(playerT.position + playerT.TransformDirection(waterCannonRange/2, 0, 0), new Vector3(waterCannonRange, 1, 1));
            }
        }
    }

    //Upgrade weapon - find weapon by name - Has to be called from shop script where upgrades are obtained
    public void UpgradeWeapon(string weaponName)
    {
        foreach (Weapon w in weapons)
        {
            if (w.GetWeaponName() == weaponName)
            {
                //Call weapon's upgrade method
                w.Upgrade();
                break;
            }
        }
    }

    public void UpgradeDamage()
    {
        foreach (Weapon w in weapons)
        {
            w.SetDamage(w.GetDamage() + playerData.damageBoost);
        }
    }

    //Reload weapon
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1f);
        currentWeapon.SetCurrentAmmo(currentWeapon.GetMaxAmmo());
    }
}
