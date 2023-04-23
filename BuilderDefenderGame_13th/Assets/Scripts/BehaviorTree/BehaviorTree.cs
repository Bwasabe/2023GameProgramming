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
        BindingFlags flag = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance;
        FieldInfo[] fieldInfos = myType.GetFields(flag);

        foreach (var field in fieldInfos)
        {
            BT_Variable data = field.GetValue(this) as BT_Variable;

            if(data != null)
            {
                DataController.AddData(data);
            }
        }
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
