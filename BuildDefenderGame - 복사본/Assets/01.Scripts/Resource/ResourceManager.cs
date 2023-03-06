using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance{ get; private set;}

    private Dictionary<ResourceTypeSO, int> _resourceAmountDictionary;

    private void Awake()
    {
        Instance = this;
        _resourceAmountDictionary = new();

        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO));

        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            _resourceAmountDictionary[resourceType] = 0;
        }
        TestLogResourceAmountDictionary();
    }

    // private void Update()
    // {
    //     // if (Input.GetKeyDown(KeyCode.Q))
    //     // {
    //     //     ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO));
    //     //     AddResource(resourceTypeList.list[0], 1);
    //     // }

    //     // if (Input.GetKeyDown(KeyCode.W))
    //     // {
    //     //     ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO));
    //     //     AddResource(resourceTypeList.list[1], 1);
    //     // }

    //     // if (Input.GetKeyDown(KeyCode.E))
    //     // {
    //     //     ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO));
    //     //     AddResource(resourceTypeList.list[2], 1);
    //     // }

    //     // if(Input.GetKeyDown(KeyCode.R))
    //     // {
    //     //     ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO));
    //     //     AddResource(resourceTypeList.list[3], 1);
    //     // }
    // }

    private void TestLogResourceAmountDictionary()
    {
        foreach (ResourceTypeSO resourceType in _resourceAmountDictionary.Keys)
        {
            Debug.Log(resourceType.nameString + " : " + _resourceAmountDictionary[resourceType]);
        }
    }

    public void AddResource(ResourceTypeSO resourceType, int amount)
    {
        _resourceAmountDictionary[resourceType] += amount;
        TestLogResourceAmountDictionary();
    }

    public int GetResource(ResourceTypeSO resourceType)
    {
        return _resourceAmountDictionary[resourceType];
    }
}
