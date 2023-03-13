using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Holder))]
public class ResourceGenerator : MonoBehaviour
{
    private ResourceGeneratorData _resourceGeneratorData;
    private float _timer;
    private float _timerMax;
    
    private void Awake() {
        _resourceGeneratorData = GetComponent<BuildingTypeHolder>().BuildingType.resourceGeneratorData;
        _timerMax = _resourceGeneratorData.timerMax;
        
    }

    private void Start() {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, _resourceGeneratorData.resourceDetectionRadius);

        int nearbyResourceAmount = 0;
        foreach(Collider2D collider2D in collider2DArray)
        {
            ResourceNode resourceNode = collider2D.GetComponent<ResourceNode>();
            if(resourceNode != null)
            {
                if(resourceNode._resourceType == _resourceGeneratorData.resourceType)
                    nearbyResourceAmount++;
            }
        }

        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount,0, _resourceGeneratorData.maxResourceAmount);

        if(nearbyResourceAmount == 0)
        {
            enabled = false;
        }
        else{
            _timerMax = (_resourceGeneratorData.timerMax / 2) + _resourceGeneratorData.timerMax * 
            (1 - (float)nearbyResourceAmount / _resourceGeneratorData.maxResourceAmount);
        }
        Debug.Log("주변 리소스 노드의 갯수 : " + nearbyResourceAmount);
    }

    private void Update() {
        _timer -= Time.deltaTime;
        if(_timer <= 0)
        {
            _timer += _timerMax;
            ResourceManager.Instance.AddResource(_resourceGeneratorData.resourceType, 1);
            //Debug.Log(_buildingType.resourceGeneratorData.resourceType.nameString);
        }
    }
}
