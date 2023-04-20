using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;


// TODO: TickDamageText는 아직 수정 안함
public class TickText : BaseText
{
    [SerializeField]
    private RandomAngleTextData _textData;
    
    public override BaseTextData TextData{
        get {
            return _textData;
        }
        set {
            _textData = value as RandomAngleTextData;
        }
    }


    public override BaseText ShowText()
    {
        bool isLeft = Random.Range(0, 2) == 0;

        float angle = Random.Range(_textData.AngleMin, _textData.AngleMax);

        if(isLeft)
        {
            angle *= -1f;
        }

        angle += 90f;

        angle *= Mathf.Deg2Rad;

        Vector3 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)
        );
        dir.Normalize();
        
        dir *= Random.Range(_textData.RadiusMin, _textData.RadiusMax);

        dir += _text.transform.position;

        float jumpPower = Random.Range(_textData.JumpPowerMin, _textData.JumpPowerMax);

        Sequence sequence = DOTween.Sequence();

        sequence.Append(_text.transform.DOJump(dir, jumpPower, _textData.JumpNum, _textData.AnimationDuration));
        sequence.Join(_text.DOFade(_textData.TextAlpha, _textData.AnimationDuration));

        sequence.AppendCallback(ResetText);
        
        return this;
    }


// #if UNITY_EDITOR
    
//     private void OnDrawGizmos()
//     {
        
//         Vector3 position = transform.position;
    
//         Gizmos.color = Color.cyan;
//         DrawWireArc(position, _textData.AngleMin,_textData.AngleMax ,_textData.RadiusMax,90f);
        
//         Gizmos.color = Color.red;
//         DrawWireArc(position, _textData.AngleMin,_textData.AngleMax ,_textData.RadiusMin,90f);
        
    
//         Gizmos.color = Color.cyan;
//         DrawWireArc(position, -_textData.AngleMin,-_textData.AngleMax ,_textData.RadiusMax,90f);
        
//         Gizmos.color = Color.red;
//         DrawWireArc(position, -_textData.AngleMin,-_textData.AngleMax,_textData.RadiusMin,90f);
    
        
//     }
    
//     private void DrawWireArc(Vector3 position, float startAngle, float endAngle , float radius, float addAngle = 0f ,float maxSteps = 20)
//     {
//         Vector3 initialPos = position;
//         Vector3 posA = initialPos;
//         float stepAngles = (endAngle - startAngle) / maxSteps;
        
//         for (int i = 0; i <= maxSteps; i++)
//         {
//             float rad = Mathf.Deg2Rad * (startAngle + addAngle);
//             Vector3 posB = initialPos;
//             posB += new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * radius;
    
//             Gizmos.DrawLine(posA, posB);
    
//             startAngle += stepAngles;
//             posA = posB;
//         }
//         Gizmos.DrawLine(posA, initialPos);
//     }
    
// #endif
}

[Serializable]
public class RandomAngleTextData : BaseTextData
{
    [field: SerializeField]
    public float AnimationDuration{ get; private set;} = 0.5f;
    [field: SerializeField]
    public float TextAlpha{ get; private set;} = 0f;

    [field: SerializeField]
    public float AngleMin{ get; private set;} = 10f;
    [field: SerializeField]
    public float AngleMax{ get; private set;} = 30f;

    [field: SerializeField]
    public float RadiusMin{ get; private set;} = 1.5f;
    [field: SerializeField]
    public float RadiusMax{ get; private set;} = 2f;

    [field: SerializeField]
    public float JumpPowerMin{ get; private set; } = 0.4f;
    [field: SerializeField]
    public float JumpPowerMax{ get; private set;} = 0.7f;

    [field: SerializeField]
    public int JumpNum{ get; private set; } = 1;
}
