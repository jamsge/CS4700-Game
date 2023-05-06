using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour
{
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
            if (inventoryItems[i].count > 0)
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
            case (1):
                // increase attack here
                break;
            //bandages
            case (2):
                // increase health here
                break;
            // coffee
            case (3):
                // increase speed here
                break;
            // painkillers
            case (4):
                // increase health and defense here
                break;
            default:
                break;
        }
        // decrease count
        inventoryItems[itemNo].count--;
        CheckUsable();

    }
}
