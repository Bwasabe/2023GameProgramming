using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : BT_Node
{
    private EntityVariable _entityVariable;
    private float _timer;
    private PlayerVariable _playerVariable;

    private Entity _entity;
    public PlayerAttack(Entity tree, List<BT_Node> children = null) : base(tree, children)
    {
        _entity = tree;
        _playerVariable = tree.DataController.GetData<PlayerVariable>();
        _entityVariable = tree.DataController.GetData<EntityVariable>();
        
        _playerVariable.AnimationEventHandler.AddEvent("Attack", Attack);
    }

    protected override void OnEnter()
    {
        base.OnEnter();
        Vector3 dir = _entityVariable.Target.position - _tree.transform.position;
        dir.Normalize();
        
        _entity.Flip(dir);

        float rotation = UtilClass.GetAngleFromVector(dir);
        
        if(_tree.transform.localScale.x < 0)
        {
            rotation = 180f - rotation;
        }
        _playerVariable.Arm.localRotation = Quaternion.Euler(0f,0f,rotation);

        _playerVariable.PlayerAnimationController.SetAnimationState(PlayerAnimationState.Attack);
    }

    private void Attack()
    {
        PlayerProjectile.Create(_playerVariable.PlayerAttackTransform.position, _entityVariable.Target);
    }
}

public partial class PlayerVariable
{
    public AnimationEventHandler AnimationEventHandler{ get; set; }
    
    [field:SerializeField] public Transform Arm{ get; private set; }
    [field:SerializeField]public Transform PlayerAttackTransform{ get; private set; }
}