using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : BT_Node
{
    private PlayerVariable _playerVariable;
    private EntityVariable _entityVariable;
    public PlayerIdle(BehaviorTree tree, List<BT_Node> children = null) : base(tree, children)
    {
        _entityVariable = tree.DataController.GetData<EntityVariable>();
        _playerVariable = tree.DataController.GetData<PlayerVariable>();
    }

    protected override void OnEnter()
    {
        _playerVariable.Arm.localRotation = Quaternion.identity;
        _entityVariable.Rigidbody.velocity = Vector2.zero;

        _playerVariable.PlayerAnimationController.TrySetAnimationState(PlayerAnimationState.Idle);
    }
}