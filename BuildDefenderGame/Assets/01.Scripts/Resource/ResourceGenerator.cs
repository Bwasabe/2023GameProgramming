using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Holder))]
public class ResourceGenerator : MonoBehaviour
{
    [SerializeField]
    private ResourceManager _resourceManager;
    
    private BuildingTypeSO _buildingType;
    private float _timer;
    private float _timerMax;
    
    private void Awake() {
        _buildingType = GetComponent<BuildingTypeHolder>().BuildingType;
        _timerMax = _buildingType.resourceGeneratorData.timerMax;
        
    }

    private void Update() {
        _timer -= Time.deltaTime;
        if(_timer <= 0)
        {
            _timer += _timerMax;
            Debug.Log(_buildingType.resourceGeneratorData.resourceType.nameString);
        }
    }
}
