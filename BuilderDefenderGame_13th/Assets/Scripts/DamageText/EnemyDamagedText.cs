using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyDamagedText : FadeScaleText, IRandomTextAble
{
    [SerializeField]
    private EnemyDamagedTextData _textData;
    
    public override BaseTextData TextData{
        get {
            return _textData;
        }
        set {
            _textData = value as EnemyDamagedTextData;
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
        _randomTextAble.SetRandomPosition(_text.transform, _textData.RandomPosTextData.RadiusMin, _textData.RandomPosTextData.RadiusMax);
        
        Sequence sequence = DOTween.Sequence();

        sequence.Append(
            _text.transform.DOLocalMoveY( _textData.TextHeight,_textData.HeightDuration).SetEase(_textData.AnimationCurve)
        );

        sequence.Append(FadeScaleSequence(_textData));

        return this;
    }


}

[Serializable]
public class EnemyDamagedTextData : FadeScaleTextData
{
    [field: SerializeField] public RandomPositionTextData RandomPosTextData{ get; private set; }

    [field: SerializeField] public AnimationCurve AnimationCurve{ get; private set; }
    

    [field: SerializeField] public float TextHeight{ get; private set; } = 1f;
    [field: SerializeField] public float HeightDuration{ get; private set; } = 0.5f;

}

