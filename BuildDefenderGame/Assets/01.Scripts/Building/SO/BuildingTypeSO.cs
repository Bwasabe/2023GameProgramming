using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( menuName = "ScriptableObject/BuildingType", fileName = "BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    // public KeyCode keycode = KeyCode.A;
    // public int index;
    public string nameString;
    public Transform prefab;
    public ResourceGeneratorData resourceGeneratorData;
    public Sprite sprite;
    public float minConstructionRadius;

    public ResourceAmount[] constructionResourceCostArray;

    public string GetConstructionResourceCostString()
    {
        string str = "";
        foreach (ResourceAmount resourceAmount in constructionResourceCostArray)
        {
            str += "<color=#"+resourceAmount.resourceTypeSo.colorHex + ">" +
            resourceAmount.resourceTypeSo.nameShort +  + resourceAmount.amount + "</color>";
        }

        return str;
    }
}
