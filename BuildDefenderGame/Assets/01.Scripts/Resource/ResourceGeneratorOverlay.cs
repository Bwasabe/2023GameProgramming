using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField]
    private ResourceGenerator _resourceGenerator;

    private Transform _barTransform;
    private void Start()
    {
        ResourceGeneratorData resourceGeneratorData = _resourceGenerator.GetResourceGeneratorData();

        _barTransform = transform.Find("Bar");
        transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;

        transform.Find("Text (TMP)").GetComponent<TextMeshPro>().SetText(_resourceGenerator.GetAmountGeneratedPerSecond().ToString("F1"));
    }

    private void Update()
    {
        _barTransform.localScale = new Vector3(1 - _resourceGenerator.GetTimerNormalized(), 1, 1);
    }
}
