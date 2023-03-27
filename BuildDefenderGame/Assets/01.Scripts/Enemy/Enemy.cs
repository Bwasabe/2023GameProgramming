using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HealthSystem))]
public class Enemy : MonoBehaviour
{
    private HealthSystem _healthSystem;
    
    public static Enemy Create(Vector3 position)
    {
        Transform pfEnemy = Resources.Load<Transform>("pfEnemy");
        Transform enemyTransform = Instantiate(pfEnemy, position, Quaternion.identity);
        Enemy enemy = enemyTransform.GetComponent<Enemy>();

        return enemy;
    }
    
    private Transform _targetTransform;
    private Rigidbody2D _enemyRigidbody2D;


    private float _lookForTargetTimer;
    private float _lookForTargetTimerMax = 0.2f;

    private void Awake()
    {
        _enemyRigidbody2D = GetComponent<Rigidbody2D>();

        _healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        if(BuildingManager.Instance.GetHQBuidling() != null)
            _targetTransform = BuildingManager.Instance.GetHQBuidling().transform;

        _healthSystem.OnDied += HealthSystem_OnDied;
        _lookForTargetTimer = Random.Range(0f, _lookForTargetTimerMax);
    }
    private void HealthSystem_OnDied(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        HandleMovement();
        HandleTargeting();
    }

    private void HandleMovement()
    {
        if(_targetTransform != null)
        {
            Vector3 moveDir = (_targetTransform.position - transform.position).normalized;
            float moveSpeed = 8f;
            
            _enemyRigidbody2D.velocity = moveDir * moveSpeed;
        }
        else
        {
            _enemyRigidbody2D.velocity = Vector2.zero;
            
        }
    }

    private void HandleTargeting()
    { 
        _lookForTargetTimer -= Time.deltaTime;
        if(_lookForTargetTimer < 0)
        {
            _lookForTargetTimer += _lookForTargetTimerMax;
            LookForTargets();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Building building = other.gameObject.GetComponent<Building>();

        if(building != null)
        {
            HealthSystem healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.Damage(10);
            Destroy(gameObject);
        }
    }

    private void LookForTargets()
    {
        float targetMaxRadius = 20f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D collider2D in collider2DArray)
        {
            Building building = collider2D.GetComponent<Building>();
            if(building != null)
            {
                if(_targetTransform == null)
                {
                    _targetTransform = building.transform;
                }
                else
                {
                    if(Vector3.Distance(transform.position, building.transform.position) <
                       Vector3.Distance(transform.position, _targetTransform.position))
                    {
                        _targetTransform = building.transform;
                    }
                }
            }
        }

        if(_targetTransform == null)
        {
            if(BuildingManager.Instance.GetHQBuidling() != null)
                _targetTransform = BuildingManager.Instance.GetHQBuidling().transform;
        }
    }
}
