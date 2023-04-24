using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityClickMoveCondition : BT_Condition
{
    private EntityVariable _variable;
    public EntityClickMoveCondition(BehaviorTree tree, List<BT_Node> children) : base(tree, children)
    {
        _variable = tree.DataController.GetData<EntityVariable>();
    }

    protected override void OnUpdate()
    {
        Vector3 distance = _variable.Target.position - _tree.transform.position;

        if(distance.magnitude <= _variable.MoveSpeed * Time.deltaTime)
        {
            _variable.Rigidbody.velocity = Vector2.zero;
            _variable.IsClickMoving = false;
            UpdateState = UpdateState.None;
            _nodeResult = NodeResult.FAILURE;
        }
        else
            _nodeResult = _children[0].Execute();
    }
}
