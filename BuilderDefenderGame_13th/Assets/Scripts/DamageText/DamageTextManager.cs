using System;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : MonoSingleton<DamageTextManager>
{
    // [SerializeField] private TextType _testTextType;

    [SerializeField] private List<TextContainer> _textContainer;
    

    public List<TextContainer> TextContainer => _textContainer;

    private readonly Dictionary<TextType, BaseText> _textDict = new();


    protected override void Start()
    {
        base.Start();
        InitText();
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         Debug.Log("텍스트 출력");
    //         var text = GetDamageText(_testTextType).SetText(40f);
    //         text.ShowText();
    //     }
    // }

    public void AddTextContainer(TextContainer container)
    {
        _textContainer.Add(container);
    }

    public void ResetTextContainer()
    {
        _textContainer.Clear();
    }

    private void InitText()
    {
        foreach (TextContainer container in _textContainer)
        {
            if (!_textDict.TryAdd(container.textType, container.textPrefab))
            {
                throw new SystemException("TextType is overlap in dictionary");
            }
        }
    }


    public BaseText GetDamageText(TextType type)
    {
        if (_textDict.TryGetValue(type, out BaseText textPrefab))
        {
            BaseText text = PoolManager.Instantiate(textPrefab.gameObject).GetComponent<BaseText>();
            return text;
        }
        else
            throw new SystemException("Type is Null in Dict");
    }

    public BaseText GetDamageText(TextType type, Color color, Vector3 position, float text)
    {
        BaseText baseText = GetDamageText(type);
        baseText.SetColor(color);
        baseText.SetPosition(position);
        baseText.SetText(text);

        return baseText;
    }

    public BaseText GetDamageText(TextType type, Color color, float text)
    {
        BaseText baseText = GetDamageText(type);
        baseText.SetColor(color);
        baseText.SetText(text);

        return baseText;
    }

    public BaseText GetDamageText(TextType type, Vector3 position, float text)
    {
        BaseText baseText = GetDamageText(type);
        baseText.SetPosition(position);
        baseText.SetText(text);

        return baseText;
    }

    public BaseText GetDamageText(TextType type, Color color, Vector3 position)
    {
        BaseText baseText = GetDamageText(type);
        baseText.SetColor(color);
        baseText.SetPosition(position);

        return baseText;
    }

    public BaseText GetDamageText(TextType type, Color color)
    {
        BaseText baseText = GetDamageText(type);
        baseText.SetColor(color);

        return baseText;
    }

    public BaseText GetDamageText(TextType type, Vector3 position)
    {
        BaseText baseText = GetDamageText(type);
        baseText.SetPosition(position);

        return baseText;
    }

    public BaseText GetDamageText(TextType type, float text)
    {
        BaseText baseText = GetDamageText(type);
        baseText.SetText(text);

        return baseText;
    }


#if UNITY_EDITOR

    // private void OnDrawGizmos()
    // {
    //     
    //     Vector3 position = _spawnPos.position;
    //
    //     Gizmos.color = Color.cyan;
    //     DrawWireArc(position, _angleMin,_angleMax ,_radiusMax,90f);
    //     
    //     Gizmos.color = Color.red;
    //     DrawWireArc(position, _angleMin,_angleMax ,_radiusMin,90f);
    //     
    //
    //     Gizmos.color = Color.cyan;
    //     DrawWireArc(position, -_angleMin,-_angleMax ,_radiusMax,90f);
    //     
    //     Gizmos.color = Color.red;
    //     DrawWireArc(position, -_angleMin,-_angleMax,_radiusMin,90f);
    //
    //     
    // }
    //
    // private void DrawWireArc(Vector3 position, float startAngle, float endAngle , float radius, float addAngle = 0f ,float maxSteps = 20)
    // {
    //     Vector3 initialPos = position;
    //     Vector3 posA = initialPos;
    //     float stepAngles = (endAngle - startAngle) / maxSteps;
    //     
    //     for (int i = 0; i <= maxSteps; i++)
    //     {
    //         float rad = Mathf.Deg2Rad * (startAngle + addAngle);
    //         Vector3 posB = initialPos;
    //         posB += new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * radius;
    //
    //         Gizmos.DrawLine(posA, posB);
    //
    //         startAngle += stepAngles;
    //         posA = posB;
    //     }
    //     Gizmos.DrawLine(posA, initialPos);
    // }

#endif
}