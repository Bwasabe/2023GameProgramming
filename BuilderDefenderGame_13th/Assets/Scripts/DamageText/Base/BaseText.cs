using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public abstract class BaseText : MonoBehaviour
{
    public abstract BaseTextData TextData{ get; set; }
    
    protected TMP_Text _text;
    
    protected Color _defaultColor;
    
    protected virtual void Awake()
    {
        _text = GetComponentInChildren<TMP_Text>();

        _defaultColor = _text.color;
        ResetValue();
    }

    public abstract BaseText ShowText();

    public BaseText SetColor(Color color)
    {
        _text.color = color;

        return this;
    }

    public BaseText SetTextSize(float textSize)
    {
        _text.fontSize = textSize;

        return this;
    }

    public BaseText SetPosition(Vector3 pos)
    {
        transform.position = pos;

        return this;
    }

    public virtual BaseText SetText(float text)
    {
        _text.text = text.ToString();
        return this;
    }

    protected void ResetText()
    {
        ResetValue();
        PoolManager.Destroy(gameObject);
    }

    protected virtual void ResetValue()
    {
        _text.transform.localScale = Vector3.one * 0.1f;
        _text.color = _defaultColor;
        _text.transform.localPosition = Vector3.zero;
        _text.fontSize = TextData.DefaultTextSize;
    }
}

[Serializable]
public class BaseTextData
{
    [field: SerializeField] public float DefaultTextSize{ get; private set; } = 50f;
}
