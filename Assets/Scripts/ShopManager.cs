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
    public Button[] purchaseItemButtons;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < purchasableItems.Length; i++)
        {
            shopItemPanelsGameObject[i].SetActive(true);
        }

        oozeUI.text = "Ooze: " + ooze.ToString();
        LoadPanels();
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

    public void CheckAffordableItems()
    {
        for(int i = 0; i < purchasableItems.Length; i++)
        {
            if (ooze >= purchasableItems[i].baseCost)
                purchaseItemButtons[i].interactable = true;
            else
                purchaseItemButtons[i].interactable = false;
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
