using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSearchTarget : BT_Node
{
    private PlayerVariable _playerVariable;
    public PlayerSearchTarget(BehaviorTree tree, List<BT_Node> children = null) : base(tree, children) { 
        _playerVariable = tree.DataController.GetData<PlayerVariable>();
    }

    protected override void OnEnter()
    {
        Collider2D collider = Physics2D.OverlapCircle(_tree.transform.position, _playerVariable.SearchDistance);

        if(collider != null)
        {
            if(collider.transform.TryGetComponent<Enemy>(out Enemy value))
            {
                _playerVariable.Target = value.transform;
            }
        }
        UpdateState = UpdateState.None;
    }
}

public class PlayerSearchTargetCondition : BT_Condition
{
    private PlayerVariable _playerVariable;
    public PlayerSearchTargetCondition(BehaviorTree tree, List<BT_Node> children ) : base(tree, children) { 
        _playerVariable = tree.DataController.GetData<PlayerVariable>();
    }

    private float _timer;
    protected override void OnUpdate()
    {
        _timer += Time.deltaTime;

        if(_timer >= _playerVariable.SearchDuration)
            _nodeResult = _children[0].Execute();
        else
            _nodeResult = NodeResult.FAILURE;
    }


}

public partial class PlayerVariable
{
    public Transform Target{get;set;}
    [field: SerializeField] public float SearchDistance { get; private set; } = 10f;

    [field: SerializeField] public float SearchDuration { get; private set; } = 0.2f;

}
