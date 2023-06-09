using System;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public int ooze;
    public int baseHealth;
    public int health;
    public float defaultMaxSpeed = 5f; //walk speed
    public float damageBoost = 0;
    public bool defenseBoost;
    public event Action<int> onPlayerHealthSet;
    public event Action onPlayerHit;
    public event Action onPlayerDeath;

    // Resets health value to base health when game is started
    private void OnEnable(){
        this.ooze = 0;
        this.defenseBoost = false;
        this.health = baseHealth;
        this.damageBoost = 0;
    }

    // Change health value directly, used for initializing HUD or increasing health. Triggers set health event
    public void setHealth(int health)
{
    this.health = health;
    onPlayerHealthSet?.Invoke(this.health);
}


    // Player takes a hit, remove health and trigger hit event
    public void takeHit(){
        this.health--;
        onPlayerHit?.Invoke();
        if (this.health <= 0){
            onPlayerDeath?.Invoke();
        }
    }

    public void takeHit(int damage){
        if (defenseBoost)
            damage /= 2; //reduce damage taken when defense boost is active
        this.health -= damage;
        onPlayerHit?.Invoke();
        if (this.health <= 0){
            onPlayerDeath?.Invoke();
        }
    }
}
