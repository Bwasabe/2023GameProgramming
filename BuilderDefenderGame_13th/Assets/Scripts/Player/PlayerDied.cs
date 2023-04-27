using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(HealthSystem))]
public class PlayerDied : MonoBehaviour
{
    private HealthSystem _healthSystem;
    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
        
    }
    private void Start()
    {
        _healthSystem.OnDied += OnPlayerDied;
    }
    private void OnPlayerDied(object sender, EventArgs e)
    {
        gameObject.SetActive(false);
    }
}