using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMove : BT_Node
{
    private EntityVariable _variable;

    private Entity _entity;
    protected EntityMove(Entity tree, List<BT_Node> children = null) : base(tree, children) {
        _variable = tree.DataController.GetData<EntityVariable>();
        _entity = tree;
    }

    protected override void OnUpdate()
    {
        Vector2 dir = _variable.Target.position - _tree.transform.position;
        dir.Normalize();
        
        _entity.Flip(dir);
        
        _variable.Rigidbody.velocity = dir * _variable.MoveSpeed;
    }
}

public partial class EntityVariable
{
    public Transform Target{get;set;}
}
