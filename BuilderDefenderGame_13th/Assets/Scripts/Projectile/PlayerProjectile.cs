using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public static PlayerProjectile Create(Vector3 position, Transform target)
    {
        Transform arrowProjectileTransform = Instantiate(GameAssets.Instance.pfPlayerProjectile, position, Quaternion.identity);
        PlayerProjectile arrowProjectile = arrowProjectileTransform.GetComponent<PlayerProjectile>();
        arrowProjectile.SetTarget(target);
        
        if(target == null) Destroy(arrowProjectileTransform.gameObject);

        return arrowProjectile;
    }

    private Transform targetEnemy;
    private Vector3 lastMoveDir;
    [SerializeField]
    private float _moveSpeed = 30f;
    [SerializeField]
    private float timeToDie = 2f;

    private void Update()
    {
        Vector3 moveDir;

        if (targetEnemy != null)
        {
            moveDir = (targetEnemy.transform.position - transform.position).normalized;
            lastMoveDir = moveDir;
        }
        else
        {
            moveDir = lastMoveDir;
        }

        
        transform.position += _moveSpeed * Time.deltaTime * moveDir;
        transform.eulerAngles = new Vector3(0, 0, UtilClass.GetAngleFromVector(moveDir));

        timeToDie -= Time.deltaTime;
        if (timeToDie < 0f)
        {
            Destroy(gameObject);
        }
    }

    private void SetTarget(Transform targetEnemy)
    {
        this.targetEnemy = targetEnemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            int damageAmount = 10;
            DamageTextManager.Instance.GetDamageText(TextType.EnemyDamaged, enemy.transform.position, damageAmount).ShowText();
            enemy.GetComponent<HealthSystem>().Damage(damageAmount);
            Destroy(gameObject);
        }
    }
}
