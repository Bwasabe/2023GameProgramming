using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSystem _healthSystem;
    private BuildingTypeSO _buildingTypeSo;
    private void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();

        _buildingTypeSo = GetComponent<BuildingTypeHolder>().BuildingType;
        _healthSystem.SetHealthAmountMax(_buildingTypeSo.healthAmountMax, true);
        _healthSystem.OnDied += HealthSystem_OnDied;
    }
    private void HealthSystem_OnDied(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }
}
