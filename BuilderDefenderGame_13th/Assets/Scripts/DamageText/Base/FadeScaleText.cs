using System;
using DG.Tweening;
using UnityEngine;

public abstract class FadeScaleText : BaseText, IFadeTextAble, IScaleTextAble
{
    protected IFadeTextAble _fadeAble;

    protected IScaleTextAble _scaleAble;

    protected override void Awake()
    {
        _scaleAble = this;
        _fadeAble = this;
        base.Awake();
    }

    protected Sequence FadeScaleSequence(FadeScaleTextData textData)
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(textData.TextDisplayDuration);

        sequence.Append(_fadeAble.FadeTweener(_text, textData.TextAlpha, textData.FadeScaleDuration));

        sequence.Join(_scaleAble.ScaleTweener(_text.transform, Vector3.zero, textData.FadeScaleDuration));

        sequence.AppendCallback(ResetText);

        return sequence;
    }
}

[Serializable]
public class FadeScaleTextData : BaseTextData
{
    [field: SerializeField] public float TextDisplayDuration{ get; private set; } = 0.5f;

    [field:SerializeField] public float TextAlpha {get;private set;} = 0.2f;
    [field:SerializeField] public float FadeScaleDuration {get;private set;} = 0.5f;
}
