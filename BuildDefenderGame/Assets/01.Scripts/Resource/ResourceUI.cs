using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour
{
    private ResourceTypeListSO _resourceTypeList;

    private Dictionary<ResourceTypeSO, Transform> _resourceTransformDictionary;
    private void Awake() {
        _resourceTransformDictionary = new();

        _resourceTypeList = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO));
        Transform resourceTemplate = transform.Find("ResourceTemplate");
        resourceTemplate.gameObject.SetActive(false);

        int index = 0;
        foreach(ResourceTypeSO resourceType in _resourceTypeList.list)
        {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);
            resourceTransform.gameObject.SetActive(true);

            float offsetAmount = -200f;
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new(index * offsetAmount, 0f);

            resourceTransform.Find("Image").GetComponent<Image>().sprite = resourceType.sprite;

            _resourceTransformDictionary[resourceType] = resourceTransform;

            index++;
        }
    }

    private void Start() {
        ResourceManager.Instance.onResourceAmountChanged += OnResourceAmountChanged;
    }

    private void OnResourceAmountChanged(object sender, EventArgs e)
    {
        UpdateResourceAmount();
    }

    // private void Update() {
    //     UpdateResourceAmount();
    // }

    private void UpdateResourceAmount()
    {
        foreach (ResourceTypeSO resourceType in _resourceTypeList.list)
        {
            Transform resourceTransform = _resourceTransformDictionary[resourceType];

            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
            resourceTransform.Find("Text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());

        }
    }
}
