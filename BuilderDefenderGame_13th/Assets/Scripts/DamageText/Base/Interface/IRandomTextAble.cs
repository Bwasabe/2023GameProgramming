using System;
using UnityEngine;
using Random = UnityEngine.Random;

public interface IRandomTextAble
{
    public void SetRandomPosition(Transform text, float radiusMin, float radiusMax)
    {
        Vector3 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        dir.Normalize();

        float radius = Random.Range(radiusMin, radiusMax);

        dir *= radius;
        
        text.localPosition = dir;
    }
}

[Serializable]
public class RandomPositionTextData
{
    [field: SerializeField] public float RadiusMin{ get; private set; } = -1f;
    [field:SerializeField] public float RadiusMax{ get; private set; } = 1f;
}
