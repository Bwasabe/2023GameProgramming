using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnDied;

    public event EventHandler OnHealed;

    public event EventHandler OnHealthAmountMaxChanged;

    [SerializeField] private int healthAmountMax;
    private int healthAmount;

    private void Awake()
    {
        healthAmount = healthAmountMax;
    }

    public void Heal(int healAmount)
    {
        healthAmount += healAmount;
        healthAmount = Mathf.Clamp(healAmount, 0, healthAmountMax);
        
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public int GetHealthAmountMax()
    {
        return healthAmountMax;
    }

    public void HealFull()
    {
        healthAmount = healthAmountMax;
        
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public void Damage(int damage)
    {
        healthAmount -= damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);

        OnDamaged?.Invoke(this, EventArgs.Empty);
        if (IsDead())
        {
            OnDied?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsDead()
    {
        return healthAmount == 0;
    }

    public int GetHealthAmount()
    {
        return healthAmount;
    }

    public float GetHealthAmountNormalized()
    {
        return (float)healthAmount / healthAmountMax;
    }

    public bool IsFullHealth()
    {
        return healthAmount == healthAmountMax;
    }

    public void SetHealthAmountMax(int healthAmountMax, bool updateHealthAmount)
    {
        this.healthAmountMax = healthAmountMax;
        
        if (updateHealthAmount)
        {
            healthAmount = healthAmountMax;
        }
        
        OnHealthAmountMaxChanged?.Invoke(this, EventArgs.Empty);
    }
}
