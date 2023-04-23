using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMove : BT_Node
{
    private EntityVariable _variable;
    public EntityMove(BehaviorTree tree, List<BT_Node> children = null) : base(tree, children) {
        _variable = tree.DataController.GetData<EntityVariable>();
    }

    protected override void OnUpdate()
    {
        Vector2 dir = _variable.Target.position - _tree.transform.position;
        dir.Normalize();
    }
}

public partial class EntityVariable
{
    public Transform Target{get;set;}
}
