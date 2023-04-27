using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectUI : MonoBehaviour
{
    private Button _button;

    [SerializeField]
    private GameObject _playerGhost;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnPlayerUISelect);
        BuildingManager.Instance.OnActiveBuildingTypeChanged += OnPlayerSelectUI;
    }
    private void OnPlayerUISelect()
    {
        BuildingManager.Instance.SetActiveBuildingType(null, true);
    }
    private void OnPlayerSelectUI(object sender, BuildingManager.OnActiveBuildingTypeEventArgs e)
    {
        if(e.isPlayerUI)
        {
            _playerGhost.SetActive(true);
        }
        else
        {
            _playerGhost.SetActive(false);
        }
    }
}
