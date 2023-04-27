using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField]
    private PlayerVariable _playerVariable;

    protected override void Awake()
    {
        base.Awake();
        _playerVariable.PlayerAnimationController = GetComponentInChildren<PlayerAnimationController>();
        _playerVariable.AnimationEventHandler = GetComponentInChildren<AnimationEventHandler>();
    }

    [ContextMenu("asdf")]
    private void SetRotation()
    {
        _playerVariable.Arm.rotation = Quaternion.Euler(0f, 0f, 30f);
    }

    protected override BT_Node SetupTree()
    {
        return new BT_Selector(this, new() {
            new EntityClickMoveCondition(this, new() {
                new PlayerClickMove(this)
            }),
            new PlayerSearchTargetCondition(this, new() {
               new PlayerSearchTarget(this) 
            }),
            new PlayerAttackOrIdleCondition(this, new() {
                new PlayerAttack(this),
                new PlayerIdle(this)
            })
        });
    }
}
