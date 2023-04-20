

using System;
using UnityEngine;
using Random = UnityEngine.Random;

public interface IVerticalRandomTextAble
{
    public void SetVerticalRandomPos(Transform text, float verticalMin, float VerticalMax)
    {
        Vector3 dir = Vector3.right * Random.Range(verticalMin, VerticalMax);
        text.localPosition = dir;
    }
}

[Serializable]
public class VerticalRandomTextData
{
    [field: SerializeField] public float VerticalMin{ get; private set; } = -1f;
    
    [field: SerializeField] public float VerticalMax{ get; private set; } = 1f;
}
