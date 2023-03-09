using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance{ get; private set; }
    private BuildingTypeListSO _buildingTypeList;
    private BuildingTypeSO _activeBuidingType;

    // private int _currentIndex;

    private Camera _mainCamera;

    private void Awake() {
        Instance = this;
        _buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        _activeBuidingType = _buildingTypeList.list[0];
    }
    
    private void Start() {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if(_activeBuidingType != null)
            {
                Instantiate(_activeBuidingType.prefab, GetMouseWorldPosition(), Quaternion.identity);
            }
        }

    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        _activeBuidingType = buildingType;
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return _activeBuidingType;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        return mouseWorldPosition;
    }
}
