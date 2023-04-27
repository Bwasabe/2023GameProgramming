using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum DayCycle
{
    Day,
    Night,
}

public class DayNightCycle : MonoSingleton<DayNightCycle>
{
    [SerializeField] private Gradient _gradient;

    [SerializeField] private float _secondsPerDay = 10f;

    [SerializeField] private float _nightStart = 0.2f;

    [SerializeField] private float _dayStart = 0.8f;

    public DayCycle DayCycle{ get; private set; }

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

        float normalizeDay = dayTime % 1f;

        DayCycle = normalizeDay switch {
            float time when time < _nightStart => DayCycle.Day,
            float time when time >= _nightStart && time <= _dayStart => DayCycle.Night,
            float time when time >= _dayStart => DayCycle.Day,
            _ => DayCycle
        };

        light2D.color = _gradient.Evaluate(dayTime % 1f);
    }


}
