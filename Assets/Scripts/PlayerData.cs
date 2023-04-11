using System;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    [SerializeField] private int baseHealth = 3;
    public int health;
    public event Action<int> onPlayerHealthSet;
    public event Action onPlayerHit;

    private void OnEnable(){
        this.health = baseHealth;
    }

    public void setHealth (int health){
        this.health = health;
        onPlayerHealthSet?.Invoke(this.health);
    }

    public void takeHit(){
        this.health--;
        onPlayerHit?.Invoke();
    }
}
