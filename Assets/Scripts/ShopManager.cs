using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    public int ooze;
    public TMP_Text oozeUI;
    public ItemSO[] purchasableItems;
    public GameObject[] shopItemPanelsGameObject;
    public ShopItemTemplate[] shopItemPanels;
    public ShopWeaponTemplate[] weaponPanels;
    public Button[] purchaseItemButtons;
    public Button[] weaponButtons;

    public Image flamethrowerImage;
    public Image fireAxeImage;
    public Image taserGunImage;
    public Image waterCannonImage;

    public int weaponCost;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < purchasableItems.Length; i++)
        {
            shopItemPanelsGameObject[i].SetActive(true);
        }

        // load weapon constants
        if(weaponPanels.Length == 4)
        {
            // images
            weaponPanels[0].icon = flamethrowerImage;
            weaponPanels[1].icon = fireAxeImage;
            weaponPanels[2].icon = taserGunImage;
            weaponPanels[3].icon = waterCannonImage;

            // titles
            weaponPanels[0].titleTxt.text = "Flamethrower";
            weaponPanels[1].titleTxt.text = "Fire Axe";
            weaponPanels[2].titleTxt.text = "Taser Gun";
            weaponPanels[3].titleTxt.text = "Water Cannon";
        }


        oozeUI.text = "Ooze: " + ooze.ToString();
        LoadPanels();
        LoadWeaponPanels();
        CheckAffordableItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddOoze()
    {
        ooze++;
        oozeUI.text = "Ooze: " + ooze.ToString();
        CheckAffordableItems();

    }

    public void LoadPanels()
    {
        for (int i = 0; i < purchasableItems.Length; i++)
        {
            shopItemPanels[i].titleTxt.text = purchasableItems[i].title;
            shopItemPanels[i].descriptionTxt.text = purchasableItems[i].description;
            shopItemPanels[i].costTxt.text = "Ooze: " + purchasableItems[i].baseCost.ToString();
        }
    }

    public void LoadWeaponPanels()
    {
        if (weaponPanels.Length == 4)
        {
            WeaponManager weaponMgr = WeaponManager.instance;

            // get info from weapon manager and load info
            if (weaponMgr.flamethrower)
            {
                if (weaponMgr.flamethrowerUpgraded)
                {
                    weaponButtons[0].interactable = false;
                    weaponPanels[0].costTxt.text = "Cost: --";
                    weaponPanels[0].buttonTxt.text = "Upgraded";
                    weaponPanels[0].cooldownTxt.text = "Cooldown: " + weaponMgr.flamethrowerCooldownU.ToString();
                    weaponPanels[0].damageTxt.text = "Damage: " + weaponMgr.flamethrowerDamageU.ToString();
                    weaponPanels[0].rangeTxt.text = "Range: " + weaponMgr.flamethrowerRangeU.ToString();
                }
                    
                else
                {
                    weaponButtons[0].interactable = true;
                    weaponPanels[0].costTxt.text = "Cost: " + weaponCost;
                    weaponPanels[0].buttonTxt.text = "Upgrade";
                    weaponPanels[0].cooldownTxt.text = "Cooldown: " + weaponMgr.flamethrowerCooldown.ToString();
                    weaponPanels[0].damageTxt.text = "Damage: " + weaponMgr.flamethrowerDamage.ToString();
                    weaponPanels[0].rangeTxt.text = "Range: " + weaponMgr.flamethrowerRange.ToString();
                }
            }
            else
            {
                weaponButtons[0].interactable = true;
                weaponPanels[0].costTxt.text = "Cost: " + weaponCost;
                weaponPanels[0].buttonTxt.text = "Buy";
                weaponPanels[0].cooldownTxt.text = "Cooldown: " + weaponMgr.flamethrowerCooldown.ToString();
                weaponPanels[0].damageTxt.text = "Damage: " + weaponMgr.flamethrowerDamage.ToString();
                weaponPanels[0].rangeTxt.text = "Range: " + weaponMgr.flamethrowerRange.ToString();
            }


            if (weaponMgr.fireAxe)
            {
                if (weaponMgr.fireAxeUpgraded)
                {
                    weaponButtons[1].interactable = false;
                    weaponPanels[1].costTxt.text = "Cost: --";
                    weaponPanels[1].buttonTxt.text = "Upgraded";
                    weaponPanels[1].cooldownTxt.text = "Speed: " + weaponMgr.fireAxeAttackSpeedU.ToString();
                    weaponPanels[1].damageTxt.text = "Damage: " + weaponMgr.fireAxeDamageU.ToString();
                    weaponPanels[1].rangeTxt.text = "Radius: " + weaponMgr.fireAxeAttackRadiusU.ToString();
                }

                else
                {
                    weaponButtons[1].interactable = true;
                    weaponPanels[1].costTxt.text = "Cost: " + weaponCost;
                    weaponPanels[1].buttonTxt.text = "Upgrade";
                    weaponPanels[1].cooldownTxt.text = "Speed: " + weaponMgr.fireAxeAttackSpeed.ToString();
                    weaponPanels[1].damageTxt.text = "Damage: " + weaponMgr.fireAxeDamage.ToString();
                    weaponPanels[1].rangeTxt.text = "Radius: " + weaponMgr.fireAxeAttackRadius.ToString();
                }
            }
            else
            {
                weaponButtons[1].interactable = true;
                weaponPanels[1].costTxt.text = "Cost: " + weaponCost;
                weaponPanels[1].buttonTxt.text = "Buy";
                weaponPanels[1].cooldownTxt.text = "Speed: " + weaponMgr.fireAxeAttackSpeed.ToString();
                weaponPanels[1].damageTxt.text = "Damage: " + weaponMgr.fireAxeDamage.ToString();
                weaponPanels[1].rangeTxt.text = "Radius: " + weaponMgr.fireAxeAttackRadius.ToString();
            }

            // get info from weapon manager and load info
            if (weaponMgr.taserGun)
            {
                if (weaponMgr.taserGunUpgraded)
                {
                    weaponButtons[2].interactable = false;
                    weaponPanels[2].costTxt.text = "Cost: --";
                    weaponPanels[2].buttonTxt.text = "Upgraded";
                    weaponPanels[2].cooldownTxt.text = "Cooldown: " + weaponMgr.taserGunCooldownU.ToString();
                    weaponPanels[2].damageTxt.text = "Damage: " + weaponMgr.taserGunDamageU.ToString();
                    weaponPanels[2].rangeTxt.text = "Range: " + weaponMgr.taserGunRangeU.ToString();
                }

                else
                {
                    weaponButtons[2].interactable = true;
                    weaponPanels[2].costTxt.text = "Cost: " + weaponCost;
                    weaponPanels[2].buttonTxt.text = "Upgrade";
                    weaponPanels[2].cooldownTxt.text = "Cooldown: " + weaponMgr.taserGunCooldown.ToString();
                    weaponPanels[2].damageTxt.text = "Damage: " + weaponMgr.taserGunDamage.ToString();
                    weaponPanels[2].rangeTxt.text = "Range: " + weaponMgr.taserGunRange.ToString();
                }
            }
            else
            {
                weaponButtons[2].interactable = true;
                weaponPanels[2].costTxt.text = "Cost: " + weaponCost;
                weaponPanels[2].buttonTxt.text = "Buy";
                weaponPanels[2].cooldownTxt.text = "Cooldown: " + weaponMgr.taserGunCooldown.ToString();
                weaponPanels[2].damageTxt.text = "Damage: " + weaponMgr.taserGunDamage.ToString();
                weaponPanels[2].rangeTxt.text = "Range: " + weaponMgr.taserGunRange.ToString();
            }

            // get info from weapon manager and load info
            if (weaponMgr.waterCannon)
            {
                if (weaponMgr.waterCannonUpgraded)
                {
                    weaponButtons[3].interactable = false;
                    weaponPanels[3].costTxt.text = "Cost: --";
                    weaponPanels[3].buttonTxt.text = "Upgraded";
                    weaponPanels[3].cooldownTxt.text = "Cooldown: " + weaponMgr.waterCannonCooldownU.ToString();
                    weaponPanels[3].damageTxt.text = "Damage: " + weaponMgr.waterCannonDamageU.ToString();
                    weaponPanels[3].rangeTxt.text = "Range: " + weaponMgr.waterCannonRangeU.ToString();
                }

                else
                {
                    weaponButtons[3].interactable = true;
                    weaponPanels[3].costTxt.text = "Cost: " + weaponCost;
                    weaponPanels[3].buttonTxt.text = "Upgrade";
                    weaponPanels[3].cooldownTxt.text = "Cooldown: " + weaponMgr.waterCannonCooldown.ToString();
                    weaponPanels[3].damageTxt.text = "Damage: " + weaponMgr.waterCannonDamage.ToString();
                    weaponPanels[3].rangeTxt.text = "Range: " + weaponMgr.waterCannonRange.ToString();
                }
            }
            else
            {
                weaponButtons[3].interactable = true;
                weaponPanels[3].costTxt.text = "Cost: " + weaponCost;
                weaponPanels[3].buttonTxt.text = "Buy";
                weaponPanels[3].cooldownTxt.text = "Cooldown: " + weaponMgr.waterCannonCooldown.ToString();
                weaponPanels[3].damageTxt.text = "Damage: " + weaponMgr.waterCannonDamage.ToString();
                weaponPanels[3].rangeTxt.text = "Range: " + weaponMgr.waterCannonRange.ToString();
            }
        }


    }

    public void UpgradeWeapon(string id)
    {
        WeaponManager weaponMgr = WeaponManager.instance;
        ooze -= weaponCost;
        oozeUI.text = "Ooze: " + ooze.ToString();
        if (id == "Flamethrower")
        {
            if (weaponMgr.flamethrower)
            {
                if(!weaponMgr.flamethrowerUpgraded)
                {
                    weaponMgr.UpgradeWeapon("Flamethrower");
                }
            }
            else
            {
                weaponMgr.flamethrower = true;
            }
        }
        else if(id == "Fire Axe")
        {
            if (weaponMgr.fireAxe)
            {
                if (!weaponMgr.fireAxeUpgraded)
                {
                    weaponMgr.UpgradeWeapon("Fire Axe");
                }
            }
            else
            {
                weaponMgr.fireAxe = true;
            }
        }
        else if(id == "Taser Gun")
        {
            if (weaponMgr.taserGun)
            {
                if (!weaponMgr.taserGunUpgraded)
                {
                    weaponMgr.UpgradeWeapon("Taser Gun");
                }
            }
            else
            {
                weaponMgr.taserGun = true;
            }
        }
        else if(id == "Water Cannon")
        {
            if (weaponMgr.waterCannon)
            {
                if (!weaponMgr.waterCannonUpgraded)
                {
                    weaponMgr.UpgradeWeapon("Water Cannon");
                }
            }
            else
            {
                weaponMgr.waterCannon = true;
            }
        }
        LoadWeaponPanels();
        CheckAffordableItems();
    }

    public void CheckAffordableItems()
    {
        for(int i = 0; i < purchasableItems.Length; i++)
        {
            if (ooze >= purchasableItems[i].baseCost)
                purchaseItemButtons[i].interactable = true;
            else
                purchaseItemButtons[i].interactable = false;
        }

        for (int i = 0; i < weaponButtons.Length; i++)
        {
            if (ooze >= weaponCost)
                weaponButtons[i].interactable = true;
            else
                weaponButtons[i].interactable = false;
        }
    }

    public void PurchaseItem(int btn)
    {
        print("purchased");
        for (int i = 0; i < purchasableItems.Length; i++)
        {
            if(ooze >= purchasableItems[btn].baseCost)
            {
                ooze = ooze - purchasableItems[btn].baseCost;
                purchasableItems[btn].count++;
                oozeUI.text = "Ooze: " + ooze.ToString();
                CheckAffordableItems();

                // add item to inventory (use purchaseableItems.title as a key)

            }
        }
    }

}
