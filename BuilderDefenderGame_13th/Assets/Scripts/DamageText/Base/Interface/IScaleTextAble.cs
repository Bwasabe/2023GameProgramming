
using System;
using DG.Tweening;
using UnityEngine;

public interface IScaleTextAble
{
    public Tweener ScaleTweener(Transform text, Vector3 scale, float duration)
    {
        return text.DOScale(scale, duration).SetEase(Ease.InBack);
    }
}

[Serializable]
public class ScaleTextData
{
    [field:SerializeField] public Vector3 TextScale{get;private set;} = Vector3.zero;

    [field:SerializeField] public float ScaleDuration{get;private set;} = 0.4f;
}
