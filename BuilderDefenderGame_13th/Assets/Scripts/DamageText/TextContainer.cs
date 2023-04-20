using System;
using UnityEngine;

[Serializable]
public class TextContainer
{
    public TextType textType;
    
    public BaseText textPrefab;
    
    [field: SerializeReference]
    public BaseTextData textData;
    public TextContainer(BaseText textPrefab, BaseTextData textData, TextType textType)
    {
        this.textPrefab = textPrefab;
        this.textData = textData;
        this.textType = textType;
    }
}
