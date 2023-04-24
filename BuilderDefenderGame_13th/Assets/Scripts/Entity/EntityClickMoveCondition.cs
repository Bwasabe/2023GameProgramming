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
        if(!_variable.IsClickMoving)
        {
            _nodeResult = NodeResult.FAILURE;
            Debug.Log("Return");
            return;
        }
        Vector3 distance = _variable.ClickMovePos - _tree.transform.position;

        if(distance.magnitude <= _variable.MoveSpeed * Time.deltaTime)
        {
            Debug.Log("Failure" + _variable.ClickMovePos);

            _variable.Rigidbody.velocity = Vector2.zero;
            _variable.IsClickMoving = false;
            
            UpdateState = UpdateState.None;
            _nodeResult = NodeResult.FAILURE;
        }
        else
        {
            Debug.Log("Child Execute");
            _nodeResult = _children[0].Execute();

        }
    }
}
