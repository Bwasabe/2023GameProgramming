using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClickMove : EntityClickMove
{
    private PlayerVariable _playerVariable;

    public PlayerClickMove(Entity tree, List<BT_Node> children = null) : base(tree, children)
    {
        _playerVariable = tree.DataController.GetData<PlayerVariable>();
    }

    protected override void OnEnter()
    {
        _playerVariable.Arm.localRotation = Quaternion.identity;

        _playerVariable.PlayerAnimationController.TrySetAnimationState(PlayerAnimationState.Move);
        base.OnEnter();
    }
}

public partial class PlayerVariable
{
    public PlayerAnimationController PlayerAnimationController{ get; set; }
}
