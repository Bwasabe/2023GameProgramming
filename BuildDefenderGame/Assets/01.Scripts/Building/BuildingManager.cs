using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }
    public event EventHandler<OnActiveBuildingTypeEventArgs> OnActiveBuildingTypeChanged;

    public class OnActiveBuildingTypeEventArgs : EventArgs{
        public BuildingTypeSO BuildingType;
    }

private BuildingTypeListSO _buildingTypeList;
    private BuildingTypeSO _activeBuidingType;

    // private int _currentIndex;

    private Camera _mainCamera;

    private void Awake() {
        Instance = this;
        _buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
    }
    
    private void Start() {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject()  )
        {
            if(_activeBuidingType != null&& 
               CanSpawnBuilding(_activeBuidingType,UtilClass.GetMouseWorldPosition()) )
            {
                if(ResourceManager.Instance.CanAfford(_activeBuidingType.constructionResourceCostArray))
                {
                    ResourceManager.Instance.SpendResources(_activeBuidingType.constructionResourceCostArray);
                    Instantiate(_activeBuidingType.prefab, UtilClass.GetMouseWorldPosition(), Quaternion.identity);
                }
            }

            Debug.Log("스폰할 수 있는가? : " +CanSpawnBuilding(_activeBuidingType,UtilClass.GetMouseWorldPosition()));
        }

    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        _activeBuidingType = buildingType;
        OnActiveBuildingTypeChanged?.Invoke(this,new OnActiveBuildingTypeEventArgs{BuildingType = _activeBuidingType});
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return _activeBuidingType;
    }

    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position)
    {
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();

        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size , 0f);

        bool isAreaClear = collider2DArray.Length == 0;
        
        if(!isAreaClear)return false;

        collider2DArray = Physics2D.OverlapCircleAll(position, _activeBuidingType.minConstructionRadius);

        foreach(Collider2D collider2D in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if(buildingTypeHolder != null)
            {
                if(buildingTypeHolder.BuildingType == _activeBuidingType)
                {
                    return false;
                }
            }
        }

        float maxConstructionRadius = 25f;
        collider2DArray = Physics2D.OverlapCircleAll(position, maxConstructionRadius);

        foreach(Collider2D collider2D in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if(buildingTypeHolder != null)
            {
                return true;
            }
        }

        return false;
    }
    
}
