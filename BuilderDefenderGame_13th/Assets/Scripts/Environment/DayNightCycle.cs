using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private Gradient _gradient;

    [SerializeField] private AnimationCurve _intensityCurve;

    [SerializeField] private float _secondsPerDay = 10f;

    private Light2D light2D;
    
    private float dayTime;
    private float dayTimeSpeed;

    private void Awake()
    {
        light2D = GetComponent<Light2D>();
        dayTimeSpeed = 1 / _secondsPerDay;
    }

    private void Update()
    {
        dayTime += Time.deltaTime * dayTimeSpeed;
        light2D.color = _gradient.Evaluate(dayTime % 1f);
        light2D.intensity = _intensityCurve.Evaluate(dayTime % 1f);
    }


}
