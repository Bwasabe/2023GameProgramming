using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ResourceType", fileName ="ResourceType")]
public class ResourceTypeSO : ScriptableObject
{
    public string nameString;
    public string nameShort;
    public Sprite sprite;
    public string colorHex;
}
