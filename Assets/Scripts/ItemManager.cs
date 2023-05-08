using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour
{
    public PlayerData playerData;
    public ItemSO[] inventoryItems;
    public GameObject[] inventoryItemObjects;
    public InventoryTemplate[] inventoryButtons;
    public Button[] useItemButtons;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            inventoryItemObjects[i].SetActive(true);
        }
        LoadInventory();
        CheckUsable();
    }

    // Update is called once per frame
    void Update()
    {
        CheckUsable();
    }

    public void LoadInventory()
    {
        for(int i = 0; i < inventoryItems.Length; i++)
        {
            inventoryButtons[i].title.text = inventoryItems[i].name;
            inventoryButtons[i].count.text = inventoryItems[i].count.ToString();
            inventoryButtons[i].icon = inventoryItems[i].image;
        }
    }

    public void CheckUsable()
    {
        for(int i = 0; i < inventoryItems.Length; i++)
        {
            inventoryButtons[i].count.text = inventoryItems[i].count.ToString();
            if (inventoryItems[i].count > 0 && !inventoryItems[i].inUse)
            {
                useItemButtons[i].interactable = true;
            }
            else
            {
                useItemButtons[i].interactable = false;
            }
        }
    }

    // 0 = Bacon, 1 = Bandages, 2 = coffee, 3 = painkillers
    public void UseItem(int itemNo)
    {
        
        switch (itemNo)
        {
            // bacon
            case (0):
                StartCoroutine(UseBacon());
                break;
            //bandages
            case (1):
                playerData.setHealth(playerData.health += inventoryItems[1].healAmount);
                if (playerData.health > playerData.baseHealth)
                    playerData.setHealth(playerData.baseHealth);
                break;
            // coffee
            case (2):
                // increase speed here
                StartCoroutine(UseCoffee());
                break;
            // painkillers
            case (3):
                // increase health and defense here
                playerData.setHealth(playerData.health += inventoryItems[3].healAmount);
                if (playerData.health > playerData.baseHealth)
                    playerData.setHealth(playerData.baseHealth);
                StartCoroutine(UsePainkillers());
                //increase defense here
                break;
            default:
                break;
        }
        // decrease count
        inventoryItems[itemNo].count--;
        CheckUsable();

    }

    IEnumerator UseBacon()
    {
        float damageBoost = playerData.damageBoost;
        playerData.damageBoost += inventoryItems[0].damageBoostValue;
        inventoryItems[0].inUse = true;
        yield return new WaitForSeconds(inventoryItems[0].itemBoostDuration);
        inventoryItems[0].inUse = false;
        playerData.damageBoost = damageBoost;
    }

    IEnumerator UseCoffee()
    {
        float initSpeed = playerData.defaultMaxSpeed;
        playerData.defaultMaxSpeed += inventoryItems[2].speedBoostValue;
        inventoryItems[2].inUse = true;
        yield return new WaitForSeconds(inventoryItems[2].itemBoostDuration);
        inventoryItems[2].inUse = false;
        playerData.defaultMaxSpeed = initSpeed;
    }

    IEnumerator UsePainkillers()
    {
        playerData.defenseBoost = true;
        inventoryItems[3].inUse = true;
        yield return new WaitForSeconds(inventoryItems[3].itemBoostDuration);
        inventoryItems[3].inUse = false;
        playerData.defenseBoost = false;
    }
}
