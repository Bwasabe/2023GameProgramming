using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnDied;
    
    [SerializeField]
    private int _healthAmountMax = 100;
    private int _healthAmount;

    private void Awake()
    {
        _healthAmount = _healthAmountMax;
    }

    public void Damage(int damage)
    {
        _healthAmount -= damage;
        _healthAmount = Mathf.Clamp(_healthAmount, 0, _healthAmountMax);
        
        OnDamaged?.Invoke(this, EventArgs.Empty);
        if(IsDead())
        {
            OnDied?.Invoke(this,EventArgs.Empty);
        }
    }

    public bool IsDead()
    {
        return _healthAmount == 0;
    }

    public int GetHealthAmount()
    {
        return _healthAmount;
    }

    public float GetHealthAmountNormalized()
    {
        return (float)_healthAmount / _healthAmountMax;
    }

    public bool IsFullHealth()
    {
        return _healthAmount == _healthAmountMax;
    }
    
    public void SetHealthAmountMax(int healthAmountMax, bool updateHealthAmount)
    {
        _healthAmountMax = healthAmountMax;
        if(updateHealthAmount)
        {
            _healthAmount = _healthAmountMax;
        }
    }
}
