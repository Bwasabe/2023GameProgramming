using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 나였다면
// ResourceLoad에서 가져오고 foreach를 통해 sprite들을 가져온 후 button을 Instantiate를 해서
// button의 sprite를 image에 넣어준다


public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField]
    private Sprite _arrowSprite;
    private BuildingTypeListSO _buildingTypeList;

    [SerializeField]
    private List<BuildingTypeSO> _ignoreBuldingTypeList;

    private Dictionary<BuildingTypeSO, Transform> _btnTransfromDict;

    private Transform _arrowBtn;

    private void Awake()
    {
        _btnTransfromDict = new();
        _buildingTypeList = Resources.Load<BuildingTypeListSO>(nameof(BuildingTypeListSO));

        Transform btnTemplate = transform.Find("ButtonTemplate");
        btnTemplate.gameObject.SetActive(false);

        int index = 0;

        _arrowBtn = Instantiate(btnTemplate, transform);
        _arrowBtn.gameObject.SetActive(true);

        float offsetAmount = 160f;
        _arrowBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(index * offsetAmount, 0);

        _arrowBtn.Find("Image").GetComponent<Image>().sprite = _arrowSprite;
        _arrowBtn.Find("Image").GetComponent<RectTransform>().sizeDelta = new Vector2(65.4623f, 85.1729f);

        _arrowBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });
        
        MouseEnterExitEvent mouseEnterExitEvent = _arrowBtn.GetComponent<MouseEnterExitEvent>();

        mouseEnterExitEvent.OnMouseEnter += (object sender, EventArgs e) =>
        {
            TooltipUI.Instance.Show("Arrow Button");
        };
            
        mouseEnterExitEvent.OnMouseExit += (object sender, EventArgs e) =>
        {
            TooltipUI.Instance.Hide();
        };

        index++;

        foreach (BuildingTypeSO buildingType in _buildingTypeList.list)
        {
            if(_ignoreBuldingTypeList.Contains(buildingType))continue;
            
            Transform btnTransform = Instantiate(btnTemplate, transform);
            btnTransform.gameObject.SetActive(true);

            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(index * offsetAmount, 0);

            btnTransform.Find("Image").GetComponent<Image>().sprite = buildingType.sprite;

            btnTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });

            mouseEnterExitEvent = btnTransform.GetComponent<MouseEnterExitEvent>();
            mouseEnterExitEvent.OnMouseEnter += (object sender, EventArgs e) =>
            {
                TooltipUI.Instance.Show(buildingType.nameString + '\n' + buildingType.GetConstructionResourceCostString());
            };
            
            mouseEnterExitEvent.OnMouseExit += (object sender, EventArgs e) =>
            {
                TooltipUI.Instance.Hide();
            };

            _btnTransfromDict[buildingType] = btnTransform;
            index++;
        }
    }

    private void Start() {
        UpdateActiveBuildingTypeBtn();
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeEventArgs e)
    {
        UpdateActiveBuildingTypeBtn();
    }


    private void UpdateActiveBuildingTypeBtn()
    {
        _arrowBtn.Find("Selected").gameObject.SetActive(false);
        foreach (BuildingTypeSO buildingType in _btnTransfromDict.Keys)
        {
            Transform btnTransform = _btnTransfromDict[buildingType];
            btnTransform.Find("Selected").gameObject.SetActive(false);
        }

        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();

        if(activeBuildingType == null)
        {
            _arrowBtn.Find("Selected").gameObject.SetActive(true);
        }
        else
        {
            _btnTransfromDict[activeBuildingType].Find("Selected").gameObject.SetActive(true);
        }

    }

}
