using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BehaviorTree : MonoBehaviour, IUpdateAble
{
    public abstract BT_Variable Variable{ get; set; }

    protected BT_Node _root;

    public bool IsStop{ get; set; } = false;
    

    protected virtual void Start()
    {
        _root = SetupTree();
    }

    protected virtual void OnEnable()
    {
        //UpdateManager.Instance.RegisterObject(this);
    }

    protected void OnDisable()
    {
        //UpdateManager.Instance?.UnRegisterObject(this);
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
    

    protected abstract BT_Node SetupTree();
    public void OnUpdate()
    {
        if(!IsStop)
        {
            _root?.Execute();
        }
    }

}
