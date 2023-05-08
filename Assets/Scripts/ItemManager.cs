using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour
{
    public PlayerData playerData;
    public float itemBoostDuration;
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

    // 0 = Bacon, 1 = Bandages, 3 = coffee, 4 = painkillers
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
                // increase health here
                break;
            // coffee
            case (2):
                // increase speed here
                break;
            // painkillers
            case (3):
                // increase health and defense here
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
        playerData.damageBoost += 2;
        inventoryItems[0].inUse = true;
        yield return new WaitForSeconds(itemBoostDuration);
        inventoryItems[0].inUse = false;
        playerData.damageBoost = damageBoost;
    }
}
