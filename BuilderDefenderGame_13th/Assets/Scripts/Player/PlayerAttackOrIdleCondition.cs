using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackOrIdleCondition : BT_Condition
{
    private EntityVariable _entityVariable;
    public PlayerAttackOrIdleCondition(BehaviorTree tree, List<BT_Node> children) : base(tree, children)
    {
        _entityVariable = tree.DataController.GetData<EntityVariable>();
    }
    
    // child 0 : Attack;
    // child 1 : Move
    protected override void OnUpdate()
    {
        if(_entityVariable.Target != null)
        {
            _nodeResult = _children[0].Execute();
        }
        else
        {
            _nodeResult = _children[1].Execute();
        }
    }
}
