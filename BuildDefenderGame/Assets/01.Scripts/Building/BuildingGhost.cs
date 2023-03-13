using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private GameObject _spriteGameObject;

    void Awake()
    {
        _spriteGameObject = transform.Find("sprite").gameObject;
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
    }
    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeEventArgs e)
    {
        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        if(e.BuildingType == null)
        {
            Hide();
        }
        else
        {
            Show(e.BuildingType.sprite);
        }
    }
    void Update()
    {
        transform.position = UtilClass.GetMouseWorldPosition();
    }

    private void Show(Sprite ghostSprite)
    {
        _spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
            _spriteGameObject.SetActive(true);
    }

    private void Hide()
    {
        _spriteGameObject.SetActive(false);
    }
}
