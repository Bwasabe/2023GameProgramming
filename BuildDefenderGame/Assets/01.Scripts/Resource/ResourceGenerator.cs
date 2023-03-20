using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Holder))]
public class ResourceGenerator : MonoBehaviour
{

    public static int GetNearbyResourceAmount(ResourceGeneratorData resourceGeneratorData, Vector3 position)
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(position, resourceGeneratorData.resourceDetectionRadius);

        int nearbyResourceAmount = 0;
        foreach (Collider2D collider2D in collider2DArray)
        {
            ResourceNode resourceNode = collider2D.GetComponent<ResourceNode>();
            if(resourceNode != null)
            {
                if(resourceNode._resourceType == resourceGeneratorData.resourceType)
                    nearbyResourceAmount++;
            }
        }

        return nearbyResourceAmount;
    }
    private ResourceGeneratorData _resourceGeneratorData;
    private float _timer;
    private float _timerMax;

    private void Awake()
    {
        _resourceGeneratorData = GetComponent<BuildingTypeHolder>().BuildingType.resourceGeneratorData;
        _timerMax = _resourceGeneratorData.timerMax;

    }

    private void Start()
    {
        int nearbyResourceAmount = GetNearbyResourceAmount(_resourceGeneratorData, transform.position);

        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, _resourceGeneratorData.maxResourceAmount);

        if(nearbyResourceAmount == 0)
        {
            enabled = false;
        }
        else
        {
            _timerMax = (_resourceGeneratorData.timerMax / 2) + _resourceGeneratorData.timerMax *
                (1 - (float)nearbyResourceAmount / _resourceGeneratorData.maxResourceAmount);
        }

        Debug.Log("주변 리소스 노드의 갯수 : " + nearbyResourceAmount);
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if(_timer <= 0)
        {
            _timer += _timerMax;
            ResourceManager.Instance.AddResource(_resourceGeneratorData.resourceType, 1);
            //Debug.Log(_buildingType.resourceGeneratorData.resourceType.nameString);
        }
    }

    public ResourceGeneratorData GetResourceGeneratorData()
    {
        return _resourceGeneratorData;
    }

    // 생산하는 것을 0~1로 정규화 한 것
    public float GetTimerNormalized()
    {
        return _timer / _timerMax;
    }

    // 1초당 생산하는 갯수
    public float GetAmountGeneratedPerSecond()
    {
        return 1f / _timerMax;
    }
}
