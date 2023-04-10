using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Create(Vector3 position)
    {
        Transform pfEnemy = Resources.Load<Transform>("pfEnemy");
        Transform enemyTransform = Instantiate(pfEnemy, position, Quaternion.identity);
        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        return enemy;
    }



    private Transform targetTransform;
    private Rigidbody2D enemyRigidbody2D;
    private HealthSystem healthSystem;

    private float lookForTargetTimer;
    private float lookForTargetTimerMax = .2f;

    private void Awake()
    {
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (BuildingManager.Instance.GetHqBuilding() != null)
        {
            targetTransform = BuildingManager.Instance.GetHqBuilding().transform;
        }
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDied += HealthSystem_OnDied;


        lookForTargetTimer = Random.Range(0f, lookForTargetTimerMax);
    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        HandleMovement();
        HandleTargetting();
    }

    private void HandleMovement()
    {
        if (targetTransform != null)
        {
            Vector3 moveDir = (targetTransform.position - transform.position).normalized;
            float moveSpeed = 8f;

            enemyRigidbody2D.velocity = moveDir * moveSpeed;
        }
        else
        {
            enemyRigidbody2D.velocity = Vector2.zero;
        }
    }

    private void HandleTargetting()
    {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer < 0)
        {
            lookForTargetTimer += lookForTargetTimerMax;
            LookForTargets();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Building building = collision.gameObject.GetComponent<Building>();
        if (building != null)
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

        foreach(Collider2D collider2D in collider2DArray)
        {
            Building building = collider2D.GetComponent<Building>();
            if (building != null)
            {
                if (targetTransform == null)
                {
                    targetTransform = building.transform;
                }
                else
                {
                    if (Vector3.Distance(transform.position, building.transform.position) < 
                        Vector3.Distance(transform.position, targetTransform.position))
                    {
                        targetTransform = building.transform;
                    }
                }
            }
        }

        if (targetTransform == null)
        {
            if (BuildingManager.Instance.GetHqBuilding() != null)
            {
                targetTransform = BuildingManager.Instance.GetHqBuilding().transform;
            }
            
        }
    }
}
