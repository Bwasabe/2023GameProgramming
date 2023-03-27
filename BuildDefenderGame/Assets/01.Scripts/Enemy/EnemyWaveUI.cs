using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyWaveUI : MonoBehaviour
{
    [SerializeField]
    private EnemyWaveManager _enemyWaveManager;
    
    private TextMeshProUGUI _waveNumberText;
    private TextMeshProUGUI _waveMessageText;

    private void Awake()
    {
        _waveNumberText = transform.Find("WaveNumberText").GetComponent<TextMeshProUGUI>();
        _waveMessageText = transform.Find("WaveMessageText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _enemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveChanged;
        SetWaveNumberText("Wave " + _enemyWaveManager.GetWaveNumber());
    }
    private void EnemyWaveManager_OnWaveChanged(object sender, EventArgs e)
    {
        SetWaveNumberText("Wave " + _enemyWaveManager.GetWaveNumber());
    }

    private void Update()
    {
        float nextWaveSpawnTimer = _enemyWaveManager.GetNextWaveSpawnTimer();
        if(nextWaveSpawnTimer <= 0f)
        {
            SetMesssageText("");
        }
        else
        {
            SetMesssageText("Next Wave in " + nextWaveSpawnTimer.ToString("F1") + "s");
        }
    }

    private void SetWaveNumberText(string text)
    {
        _waveNumberText.SetText(text);
    }

    private void SetMesssageText(string message)
    {
        _waveMessageText.SetText(message);
    }

}
