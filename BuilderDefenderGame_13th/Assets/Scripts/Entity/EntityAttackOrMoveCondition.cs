using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttackOrMoveCondition : BT_Condition
{
    private EntityVariable _variable;

    public EntityAttackOrMoveCondition(BehaviorTree tree, List<BT_Node> children) : base(tree, children)
    {
        _variable = tree.DataController.GetData<EntityVariable>();
    }

    // Children 0 : Attack
    // Children 1 : Idle
    protected override void OnUpdate()
    {
        Vector3 distance = _variable.Target.position - _tree.transform.position;

        if(_variable.AttackDistance <= distance.magnitude)
            _nodeResult = _children[0].Execute();
        else
            _children[1].Execute();
    }
}

public partial class EntityVariable
{
    [field:SerializeField] public float AttackDistance{ get; private set; }
}
