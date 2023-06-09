using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "shopMenu", menuName = "Scriptable Objects/New Shop Item", order = 1)]
public class ItemSO : ScriptableObject
{
    public string title;
    public string description;
    public int baseCost;
    public int count;
    public Image image;
    public bool inUse;
    public int healAmount; // for bandages and painkillers
    public float speedBoostValue; //for coffee
    public int damageBoostValue;
    public float itemBoostDuration; // for bacon, coffee, and painkillers
}
