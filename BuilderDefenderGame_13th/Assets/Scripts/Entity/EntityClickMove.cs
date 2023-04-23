using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityClickMove : BT_Node
{
    private EntityVariable _variable;

    public EntityClickMove(BehaviorTree tree, List<BT_Node> children = null) : base(tree, children)
    {
        _variable = tree.DataController.GetData<EntityVariable>();
    }


    protected override void OnUpdate()
    {
        Vector2 dir = _variable.MovePos - _tree.transform.position;

        // TODO: EntityClickCondition에서 걸러줘야함
        if(dir.magnitude <= _variable.MoveSpeed * Time.deltaTime)
        {
            _variable.IsClickMoving = false;
            UpdateState = UpdateState.None;
        }

        dir.Normalize();

        _variable.Rigidbody.velocity = dir * _variable.MoveSpeed;

        _nodeResult = NodeResult.SUCCESS;
    }
}

public partial class EntityVariable
{
    public Vector3 MovePos { get; set; }

    [field: SerializeField] public float MoveSpeed { get; private set; }
}
