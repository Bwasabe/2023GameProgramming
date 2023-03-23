using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private HealthSystem _healthSystem;

    private Transform _barTransform;
    
    
    private void Awake()
    {
        _barTransform = transform.Find("Bar");
    }

    private void Start()
    {
        _healthSystem.OnDamaged += HealthSystem_OnDamaged;

        UpdateBar();
        UpdateBarVisible();


    }
    private void HealthSystem_OnDamaged(object sender, EventArgs e)
    {
        UpdateBar();;
        UpdateBarVisible();
    }

    private void UpdateBar()
    {
        _barTransform.localScale = new Vector3(_healthSystem.GetHealthAmountNormalized(), 1, 1);
    }

    private void UpdateBarVisible()
    {
        if(_healthSystem.IsFullHealth())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);

        }
    }

    
}
