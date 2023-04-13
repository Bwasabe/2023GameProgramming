using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Building : MonoBehaviour
{
    private Transform buildingRepairBtn;
    private Transform buildingDemolishBtn;
    
    private HealthSystem healthSystem;
    private BuildingTypeSO buildingType;
    // Start is called before the first frame update


    private void Awake()
    {
        buildingDemolishBtn = transform.Find("pfBuildingDemolishBtn");
        buildingRepairBtn = transform.Find("pfBuildingRepairBtn");
        HideBuildingDemolishBtn();
        HideBuildingRepairBtn();
    }

    private void OnMouseEnter()
    {
        ShowBuildingDemolishBtn();
    }
    private void OnMouseExit()
    {
        HideBuildingDemolishBtn();
    }

    private void ShowBuildingDemolishBtn()
    {
        buildingDemolishBtn?.gameObject.SetActive(true);
    }
    private void HideBuildingDemolishBtn()
    {
        buildingDemolishBtn?.gameObject.SetActive(false);
    }
    
    private void ShowBuildingRepairBtn()
    {
        buildingDemolishBtn?.gameObject.SetActive(true);
    }
    private void HideBuildingRepairBtn()
    {
        buildingDemolishBtn?.gameObject.SetActive(false);
    }

    void Start()
    {

        healthSystem = GetComponent<HealthSystem>();
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;

        healthSystem.SetHealthAmountMax(buildingType.healthAmountMax, true);

        healthSystem.OnDied += HealthSystem_OnDied;
        healthSystem.OnDamaged += HealthSystem_OnDamaged;

    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
        SoundManager.Instance.PlaySound(Sound.BuildingDestroyed);
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        //if (healthSystem.IsFullHealth())
            ShowBuildingRepairBtn();
            SoundManager.Instance.PlaySound(Sound.BuildingDamanged);
    }
}
