using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HealText : FadeScaleText, IRandomTextAble
{
    [SerializeField]
    private HealTextData _textData;
    
    public override BaseTextData TextData
    {
        get {
            return _textData;
        }
        set {
            _textData = value as HealTextData;
        }
    }

    private IRandomTextAble _randomTextAble;

    protected override void Awake()
    {
        _randomTextAble = this;
        base.Awake();
    }

    public override BaseText ShowText()
    {
        _randomTextAble.SetRandomPosition(_text.transform, _textData.RandomPositionTextData.RadiusMin, _textData.RandomPositionTextData.RadiusMax);
        
        FadeScaleSequence(_textData);
        
        return this;
    }
    public override BaseText SetText(float text)
    {
        _text.text = $"+{text.ToString()}";
        return this;
    }
}

[Serializable]
public class HealTextData : FadeScaleTextData
{
    [field:SerializeField] public RandomPositionTextData RandomPositionTextData{ get; private set; }
}