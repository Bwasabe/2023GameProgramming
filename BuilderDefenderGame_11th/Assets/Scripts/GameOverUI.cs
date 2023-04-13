using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    
    public static GameOverUI Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        transform.Find("retryBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        });
        transform.Find("mainMenuBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
        });
        Hide();
    }

    public void Show()
    {
        transform.Find("waveSurvivedText").GetComponent<TextMeshProUGUI>()
            .SetText("You Survived " + EnemyWaveManager.Instance.GetWaveNumber() + "Waves!");
        gameObject.SetActive(true);
    }
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
