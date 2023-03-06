using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private BuildingTypeListSO _buildingTypeList;
    private BuildingTypeSO _buildingType;

    // private int _currentIndex;

    private Camera _mainCamera;

    private void Awake() {
        _buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        _buildingType = _buildingTypeList.list[0];
    }
    
    private void Start() {
        _mainCamera = Camera.main;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(_buildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            _buildingType = _buildingTypeList.list[0];
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            _buildingType = _buildingTypeList.list[1];

        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            _buildingType = _buildingTypeList.list[2];
        }
        // for (int i = 0; i < _buildingTypeList.list.Count; ++i)
        // {
        //     if (Input.GetKeyDown(_buildingTypeList.list[i].keycode))
        //     {
        //         _currentIndex = _buildingTypeList.list[i].index;
        //     }
        // }


    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        return mouseWorldPosition;
    }
}
