using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Entity : BehaviorTree
{
    [SerializeField]
    private EntityVariable _entityVariable;

    protected override void Awake() {
        base.Awake();
        _entityVariable.Rigidbody = GetComponent<Rigidbody2D>();
    }

    public void FlipLeft()
    {
        Vector3 scale = transform.localScale;
        float scaleX = Mathf.Abs(scale.x);
        
        scale.x = scaleX * -1f;
        
        transform.localScale = scale;
    }

    public void FlipRight()
    {
        Vector3 scale = transform.localScale;
        float scaleX = Mathf.Abs(scale.x);
        
        scale.x = scaleX;
        
        transform.localScale = scale;
    }
}