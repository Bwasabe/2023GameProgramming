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
        Debug.Log("움직이는 중");
        Vector2 dir = _variable.ClickMovePos - _tree.transform.position;
        dir.Normalize();

        _variable.Rigidbody.velocity = dir * _variable.MoveSpeed;

        _nodeResult = NodeResult.SUCCESS;
    }
}

public partial class EntityVariable
{
    public Vector3 ClickMovePos { get; set; }

    [field: SerializeField] public float MoveSpeed { get; private set; }
}
