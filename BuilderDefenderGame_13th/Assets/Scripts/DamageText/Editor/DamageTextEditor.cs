using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DamageTextManager))]
public class DamageTextEditor : Editor
{
    private const string DAMAGETEXT_FOLDER_PATH = "Assets/Prefabs/DamageText";

    private DamageTextManager _damageTextManager;
    public override void OnInspectorGUI()
    {
        _damageTextManager = (DamageTextManager)target;
        GUILayout.BeginHorizontal();
        {
            if(GUILayout.Button("Refresh"))
            {
                _damageTextManager.ResetTextContainer();
                List<BaseText> damageTexts = GetAllDamageTextInProject();
                for (int i = 0; i < damageTexts.Count; ++i)
                {
                    int index = i;
                    _damageTextManager.AddTextContainer(new TextContainer(damageTexts[i], damageTexts[i].TextData, (TextType)index)); 
                }
                RefreshTextContainer();            
            }
            if(GUILayout.Button("Apply"))
                ApplyTextContainer();
        }
        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }
    
    
    private void RefreshTextContainer()
    {
        foreach (TextContainer container in _damageTextManager.TextContainer)
        {
            BaseText text = container.textPrefab.GetComponent<BaseText>();
            container.textData = text.TextData;
        }
    }

    private void ApplyTextContainer()
    {
        foreach (TextContainer container in _damageTextManager.TextContainer)
        {
            BaseText text = container.textPrefab.GetComponent<BaseText>();
            text.TextData = container.textData;
        }
        
        AssetDatabase.Refresh();
    }


    private List<BaseText> GetAllDamageTextInProject()
    {
        var textList = AssetDatabase.FindAssets("t:GameObject" , new []{DAMAGETEXT_FOLDER_PATH}).ToList()
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(AssetDatabase.LoadAssetAtPath<GameObject>)
            .Select(x => x.GetComponent<BaseText>())
            .ToList();

        return textList;
    }
}
