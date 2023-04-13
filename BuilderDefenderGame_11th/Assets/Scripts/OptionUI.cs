using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    private void Awake()
    {
        transform.Find("soundIncreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
        });

        transform.Find("soundDecreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
        });
        transform.Find("musicIncreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
        });
        transform.Find("musicDecreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
        });
        transform.Find("mainMenuBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
        });
    }
}
