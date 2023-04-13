using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    private Enemy targetEnemy;
    private Vector3 lastMoveDir;
    private float timeToDie = 2f;

    public static ArrowProjectile Create(Vector3 position, Enemy enemy)
    {
        Transform pfArrowProjectile = Resources.Load<Transform>("pfArrowProjectile");
        Transform arrowTransform = Instantiate(pfArrowProjectile, position, Quaternion.identity);
        ArrowProjectile arrowProjectile = arrowTransform.GetComponent<ArrowProjectile>();
        arrowProjectile.SetTarget(enemy);

        return arrowProjectile;
    }

    private void Update()
    {
        timeToDie -= Time.deltaTime;
        if(timeToDie <= 0f)
        {
            Destroy(gameObject);
        }

        Vector3 moveDir = Vector3.zero;
        if (targetEnemy != null)
        {
            moveDir = (targetEnemy.transform.position - transform.position).normalized;
            lastMoveDir = moveDir;
        }
        else
        {
            moveDir = lastMoveDir;
            Destroy(gameObject);
        }
        float moveSpeed = 20f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0f, 0f, UtilClass.GetAngleFromVector(moveDir));
    }

    private void SetTarget(Enemy targetEnemy)
    {
        this.targetEnemy = targetEnemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if(enemy != null)
        {
            int damageAmount = 10;
            enemy.GetComponent<HealthSystem>().Damage(damageAmount);
            Destroy(gameObject);
        }
    }
}
