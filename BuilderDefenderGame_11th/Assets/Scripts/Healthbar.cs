using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;

    Transform barTransform;

    private Transform _separatorContainer;

    private void Awake()
    {
        barTransform = transform.Find("bar");
    }

    private void Start()
    {
        _separatorContainer = transform.Find("separatorContainer");
        ConstructHealthBarSeparators();
        
        healthSystem.OnHealthAmountMaxChanged += HealthSystem_OnHealthAmountMaxChanged;
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;
        UpdateBar();
        UpdateBarVisible();
    }
    private void HealthSystem_OnHealthAmountMaxChanged(object sender, EventArgs e)
    {
        ConstructHealthBarSeparators();
    }

    private void ConstructHealthBarSeparators()
    {
        Transform separatorTemplate = _separatorContainer.Find("separatorTemplate");
        
        separatorTemplate.gameObject.SetActive(false);

        foreach (Transform separatorTransform in _separatorContainer)
        {
            if(separatorTransform == separatorTemplate)continue;
            Destroy(separatorTransform.gameObject);
        }

        int healthAmountPerSeparator = 10;
        float barSize = 4f;
        float barOneHealthAmountSize = barSize / healthSystem.GetHealthAmountMax();
        int healthSeparatorCount = Mathf.FloorToInt(healthSystem.GetHealthAmountMax() / healthAmountPerSeparator);

        for (int i = 1; i <= healthSeparatorCount; ++i)
        {
            Transform separatorTransfrom = Instantiate(separatorTemplate, _separatorContainer);
            separatorTransfrom.gameObject.SetActive(true);
            separatorTransfrom.localPosition = new Vector3(barOneHealthAmountSize * i * healthAmountPerSeparator, 0, 0);
        }
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        UpdateBar();
        UpdateBarVisible();
    }
    
    private void HealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        UpdateBar();
        UpdateBarVisible();
    }

    private void UpdateBar()
    {
        barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized(), 1, 1);
    }

    private void UpdateBarVisible()
    {
        if (healthSystem.IsFullHealth())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
