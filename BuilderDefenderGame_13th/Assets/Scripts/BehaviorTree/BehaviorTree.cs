using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BehaviorTree : MonoBehaviour
{
    public readonly DataController<BT_Variable> DataController = new();

    protected BT_Node _root;

    public bool IsStop{ get; set; } = false;

    protected virtual void Awake() {
        InitAllData();
    }

    protected virtual void Start()
    {
        _root = SetupTree();
    }

    private void InitAllData()
    {
        Type myType = this.GetType();
        BindingFlags flag = BindingFlags.NonPublic | BindingFlags.Public 
                            | BindingFlags.Static | BindingFlags.Instance | BindingFlags.FlattenHierarchy;
        FieldInfo[] fieldInfos = myType.GetFields(flag);

        foreach (var field in fieldInfos)
        {
            if(field.GetValue(this) is BT_Variable variable)
            {
                DataController.AddData(variable);
            }
        }
    }
    
    protected abstract BT_Node SetupTree();

    public void Update()
    {
        if(!IsStop)
        {
            _root?.Execute();
        }
    }

}
