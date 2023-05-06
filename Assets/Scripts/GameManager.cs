using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public PlayerData playerData;
    public static GameManager instance;
    public int healthUpgradeCount = 0;
    public bool speedUpgraded = false;
    public bool damageUpgraded = false;

    void Awake()
    {   
        // Load HUD scene on top of game scene
        LoadSceneAdditively("InGameHUD");
        //This makes sure there is always one instance of GameManager
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
 
    void Start() {
    }

    private static void LoadSceneAdditively(string sceneName) {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    //call to upgrade player health
    public void UpgradeHealth(int newMaxHealth)
    {
        if (healthUpgradeCount < 3)
        {
            playerData.baseHealth = newMaxHealth;
            healthUpgradeCount++;
        }
    }

    //call to upgrade player speed
    public void UpgradeSpeed(float newSpeed)
    {
        playerData.defaultMaxSpeed = newSpeed;
    }

    //call to upgrade player overall damage
    public void UpgradeDamage(float damageBoostAmount)
    {
        playerData.damageBoost = damageBoostAmount;
    }
}
