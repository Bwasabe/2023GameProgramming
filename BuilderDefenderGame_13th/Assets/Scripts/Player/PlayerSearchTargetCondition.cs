using System.Collections.Generic;
using UnityEngine;

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
        {
            _nodeResult = _children[0].Execute();
            UpdateState = UpdateState.Exit;
        }
        else
            _nodeResult = NodeResult.FAILURE;
    }

    protected override void OnExit()
    {
        _timer = 0f;
        base.OnExit();
    }

}

public partial class PlayerVariable
{
    [field: SerializeField] public float SearchDuration { get; private set; } = 0.2f;

}

