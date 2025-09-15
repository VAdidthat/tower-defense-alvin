using System;
using Actors;
using Alvin.TowerDefense.Combat;
using Stats;
using UnityEngine;

public class Health:MonoBehaviour
{
    public float CurrentHealth{get; private set; }
    public float MaxHealth{get; private set; }
    public event Action<float, float> OnHealthChanged;
    private void Start()
    {
        MaxHealth = gameObject.GetComponent<Actor>().StatCollection.GetCurrentValue(StatId.Health);
        CurrentHealth = MaxHealth;
    }
    public float GetHealth()
    {
        return CurrentHealth;
    }
    public void SetHealth(float amount)
    {
        CurrentHealth = amount;
        CheckCurrentHealth();
    }
    public void Increase(float amount)
    {
        CurrentHealth += amount;
        CheckCurrentHealth();
    }
    public void Decrease(float amount)
    {
        CurrentHealth -= amount;
        CheckCurrentHealth();
    }

    private void CheckCurrentHealth()
    {        
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            gameObject.GetComponent<Actor>().StateMachine.ChangeState<DeathState>();
        }
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }
}
