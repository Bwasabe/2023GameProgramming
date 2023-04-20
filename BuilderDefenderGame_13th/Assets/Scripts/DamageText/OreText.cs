using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

public class OreText : BaseText, IUpTextAble, IFadeTextAble
{
    [SerializeField] private OreTextData _textData;

    public override BaseTextData TextData
    {
        get {
            return _textData;
        }
        set {
            _textData = value as OreTextData;
        } 
    }
    private IUpTextAble _upTextAble;
    private IFadeTextAble _fadeTextAble;

    protected override void Awake()
    {
        _upTextAble = this;
        _fadeTextAble = this;
        base.Awake();
    }

    public override BaseText ShowText()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(_upTextAble.UpTweener(_text.transform, _textData.UpTextData.TextHeight, _textData.UpTextData.HeightDuration));

        sequence.AppendInterval(_textData.AlphaDelay);

        sequence.Join(_fadeTextAble.FadeTweener(_text,_textData.TextAlpha, _textData.UpTextData.HeightDuration - _textData.AlphaDelay));

        sequence.AppendCallback(ResetText);

        return this;
    }
    
    public override BaseText SetText(float text)
    {
        _text.text = $"+{text.ToString()}";
        return this;
    } 
}

[Serializable]
public class OreTextData : BaseTextData
{
    [field:SerializeField] public UpTextData UpTextData{ get; private set; }

    [field:SerializeField] public float TextAlpha{get;private set;}

    [field:SerializeField] public float AlphaDelay{get;private set;}
}
