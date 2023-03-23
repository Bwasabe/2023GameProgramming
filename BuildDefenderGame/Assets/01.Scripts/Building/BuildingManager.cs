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

    [SerializeField]
    private Building _hqBuilding;

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
            if(_activeBuidingType != null)
            {
                if(CanSpawnBuilding(_activeBuidingType,UtilClass.GetMouseWorldPosition(), out string errorMessage))
                {
                    if(ResourceManager.Instance.CanAfford(_activeBuidingType.constructionResourceCostArray))
                    {
                        ResourceManager.Instance.SpendResources(_activeBuidingType.constructionResourceCostArray);
                        Instantiate(_activeBuidingType.prefab, UtilClass.GetMouseWorldPosition(), Quaternion.identity);
                    }
                    else
                    {
                        TooltipUI.Instance.Show("자원 부족 : " + _activeBuidingType.GetConstructionResourceCostString());
                    }
                }
                else
                {
                    TooltipUI.Instance.Show(errorMessage, new TooltipUI.TooltipTimer(){timer = 2f});
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            Vector3 enemySpawnPosition = UtilClass.GetMouseWorldPosition() + UtilClass.GetRandomDir() * 5f;
            Enemy.Create(enemySpawnPosition);
            
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

    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position, out string errorMessage)
    {
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();

        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size , 0f);

        bool isAreaClear = collider2DArray.Length == 0;

        if(!isAreaClear)
        {
            errorMessage = "건물을 놓을 수 없는 곳입니다.";
            return false;    
        }

        collider2DArray = Physics2D.OverlapCircleAll(position, _activeBuidingType.minConstructionRadius);

        foreach(Collider2D collider2D in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if(buildingTypeHolder != null)
            {
                if(buildingTypeHolder.BuildingType == _activeBuidingType)
                {
                    errorMessage = "같은 유형의 건물이 주변에 있습니다.";
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
                errorMessage = "";
                return true;
            }
        }

        errorMessage = "다른 건물이 주변에 있어야 합니다.";
        return false;
    }

    public Building GetHQBuidling()
    {
        return _hqBuilding;
    }
    
}
