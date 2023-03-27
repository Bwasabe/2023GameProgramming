using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ResourceTypeList")]
public class ResourceTypeListSO : ScriptableObject
{
    public List<ResourceTypeSO> list;
}
