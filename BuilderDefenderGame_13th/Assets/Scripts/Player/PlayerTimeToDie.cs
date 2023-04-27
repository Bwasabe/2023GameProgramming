using System;
using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public class PlayerTimeToDie : MonoBehaviour
{
    private HealthSystem _healthSystem;

    [SerializeField] private int _damagePerSec = 1;

    [SerializeField]
    private Vector3 _textOffset = new Vector3(0f,5f,0f);

    private float _timer;
    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
    }ㄸ

    private void Start()
    {
        _healthSystem.OnDied += PlayerDieParticle;
    }
    private void PlayerDieParticle(object sender, EventArgs e)
    {
        Instantiate(GameAssets.Instance.pfPlayerDieParticle, transform.position, Quaternion.identity);
    }
    private void Update()
    {
        if(DayNightCycle.Instance.DayCycle == DayCycle.Night) return;
        _timer += Time.deltaTime;
        if(_timer >= 1f)
        {
            DamageTextManager.Instance.GetDamageText(TextType.PlayerTickDamaged,transform.position + _textOffset, _damagePerSec).ShowText();
            _timer = 0f;
            _healthSystem.Damage(_damagePerSec);
        }
    }
    
    
}
