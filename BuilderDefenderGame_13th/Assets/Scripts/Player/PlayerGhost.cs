using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerGhost : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerConstruction;

    [SerializeField]
    private ResourceAmount[] _playerResources;

    private void Update()
    {
        transform.position = UtilClass.MousePos;
        if(Input.GetMouseButtonDown(0)&&!EventSystem.current.IsPointerOverGameObject())
        {
            if(ResourceManager.Instance.CanAfford(_playerResources))
            {
                ResourceManager.Instance.SpendResources(_playerResources);
                Instantiate(_playerConstruction, transform.position, Quaternion.identity);
            }
        }

    }
}