using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public interface IUpTextAble
{
    public Tweener UpTweener(Transform text, float height, float duration)
    {
        return text.DOLocalMoveY(height, duration);
    }
    
    
}

[System.Serializable]
public class UpTextData
{
    [field: SerializeField] public float TextHeight{ get; private set; } = 1f;
    [field: SerializeField] public float HeightDuration{ get; private set; } = 0.5f;
    
}