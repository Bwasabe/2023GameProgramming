using System.Collections.Generic;
using UnityEngine;

public class BT_ListRandomNode : BT_Node
{
    private int _random;

    private List<BT_Node> _currentChildren;
    
    public BT_ListRandomNode(BehaviorTree tree,List<BT_Node> children) : base(tree, children)
    {
        _random = Random.Range(0, children.Count);

        ResetLst();
    }

    protected override void OnUpdate()
    {
        BT_Node currentChild = _currentChildren[_random]; 
        _nodeResult = currentChild.Execute();
        UpdateState = currentChild.UpdateState;

        if(CheckUpdateState(UpdateState.None))
        {
            UpdateState = UpdateState.Exit;
        }
    }

    protected override void OnExit()
    {
        _currentChildren.RemoveAt(_random);

        if(_currentChildren.Count <= 0)
        {
            ResetLst();
        }
        
        _random = Random.Range(0, _children.Count);

        UpdateState = UpdateState.None;
    }

    private void ResetLst()
    {
        _currentChildren = new(_children);
    }
    
}
