using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSearchTarget : BT_Node
{
    private PlayerVariable _playerVariable;
    private EntityVariable _entityVariable;
    public PlayerSearchTarget(BehaviorTree tree, List<BT_Node> children = null) : base(tree, children) { 
        _playerVariable = tree.DataController.GetData<PlayerVariable>();
        _entityVariable = tree.DataController.GetData<EntityVariable>();
    }

    protected override void OnEnter()
    {
        Collider2D collider = Physics2D.OverlapCircle(_tree.transform.position, _playerVariable.SearchDistance);

        if(collider != null)
        {
            if(collider.transform.TryGetComponent<Enemy>(out Enemy value))
            {
                _entityVariable.Target = value.transform;
            }
        }
        UpdateState = UpdateState.None;
    }
}

public partial class PlayerVariable
{
    [field: SerializeField] public float SearchDistance { get; private set; } = 10f;

}
