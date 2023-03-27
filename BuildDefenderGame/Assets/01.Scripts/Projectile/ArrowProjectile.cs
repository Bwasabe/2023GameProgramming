using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    private Enemy _targetEnemy;

    public static ArrowProjectile Create(Vector3 position, Enemy enemy)
    {
        Transform pfArrowProjectile = Resources.Load<Transform>("pfArrowProjectile");;
        Transform arrowTransform = Instantiate(pfArrowProjectile, position, Quaternion.identity);

        ArrowProjectile arrowProjectile = arrowTransform.GetComponent<ArrowProjectile>();
        arrowProjectile.SetTarget(enemy);

        return arrowProjectile;
    }

    private void Update()
    {
        Vector3 moveDir = Vector3.zero;
        if(_targetEnemy != null)
            moveDir = (_targetEnemy.transform.position - transform.position).normalized;
        else
            Destroy(gameObject);

        transform.eulerAngles = new Vector3(0f, 0f, UtilClass.GetAngleFromVector(moveDir));

        float moveSpeed = 20f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void SetTarget(Enemy targetEnemy)
    {
        _targetEnemy = targetEnemy;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if(enemy != null)
        {
            int damageAmount = 10;
            enemy.GetComponent<HealthSystem>().Damage(damageAmount);
            
            Destroy(gameObject);
        }
    }
}
