using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AG_Enemy : MonoBehaviour
{
    public Rigidbody RB;

    private Transform _target;
    private void Awake()
    {
        RB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 dir = _target.position - transform.position;
        dir.y = 0f;
        dir.Normalize();
        
        RB.velocity = dir * 2f;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

}
