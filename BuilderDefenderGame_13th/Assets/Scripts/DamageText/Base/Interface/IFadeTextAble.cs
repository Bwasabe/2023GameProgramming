using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public interface IFadeTextAble
{
    public Tweener FadeTweener(TMP_Text text, float alpha, float duration)
    {
        return text.DOFade(alpha, duration);
    }
}

[Serializable]
public class FadeTextData
{
    [field: SerializeField] public float TextAlpha { get; private set; } = 0.2f;

    [field: SerializeField] public float AlphaDuration { get; private set; } = 0.5f;

}