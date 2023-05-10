using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    public PlayerData playerData;
    public Image fillImage;

    private float initialFillWidth;

    private void Start()
    {
        initialFillWidth = fillImage.rectTransform.sizeDelta.x;
        UpdateHealthBar(playerData.health); // Update the health bar when the scene starts
        playerData.onPlayerHealthSet += UpdateHealthBar; // Subscribe to the event to update the health bar
    }

    private void OnDestroy()
    {
        playerData.onPlayerHealthSet -= UpdateHealthBar; // Unsubscribe from the event when the object is destroyed
    }

    private void UpdateHealthBar(int health)
    {
        float fillAmount = (float)health / playerData.baseHealth;
        fillImage.rectTransform.sizeDelta = new Vector2(initialFillWidth * fillAmount, fillImage.rectTransform.sizeDelta.y);
    }
}
