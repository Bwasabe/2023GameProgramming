
using System.Collections.Generic;
using UnityEngine;

public enum NodeResult
{
    SUCCESS,
    FAILURE,
}

public enum UpdateState
{
    None,
    Enter,
    Update,
    Exit
}

public abstract class BT_Node
{
    protected NodeResult _nodeResult = NodeResult.FAILURE;

    protected BehaviorTree _tree;

    public UpdateState UpdateState{ get; set; } = UpdateState.None;

    protected List<BT_Node> _children;

    protected BT_Node(BehaviorTree tree, List<BT_Node> children = null)
    {
        _tree = tree;
        _children = children;
    }

    public virtual NodeResult Execute()
    {
        if(CheckUpdateState(UpdateState.None))
        {
            EnterNode();
        }
        
        if (CheckUpdateState(UpdateState.Enter))
        {
            OnEnter();
        }

        if (CheckUpdateState(UpdateState.Update))
        {
            OnUpdate();
        }

        if (CheckUpdateState(UpdateState.Exit))
        {
            OnExit();
        }

        return _nodeResult;
    }
    
    protected virtual void OnEnter()
    {
        UpdateState = UpdateState.Update;
    }
    
    protected virtual void OnUpdate()
    {
        
    }
    
    protected virtual void OnExit()
    {
        UpdateState = UpdateState.None;
    }
    
    private void EnterNode()
    {
        UpdateState = UpdateState.Enter;

        if(_children == null) return;

        foreach (BT_Node child in _children)
        {
            if(child.CheckUpdateState(UpdateState.None))
            {
                child.UpdateState = UpdateState.Enter;
                child.EnterNode();
            }
        }
    }

    public virtual void ResetNode()
    {
        if(_children == null) return;
        
        foreach (BT_Node child in _children)
        {
            int updateStateInt = (int)child.UpdateState;

            if(updateStateInt != (int)UpdateState.None)
            {
                child.OnExit();
                child.UpdateState = UpdateState.None;
            }
            child.ResetNode();
        }
    }

    protected bool CheckUpdateState(UpdateState updateState)
    {
        int currentState = (int)UpdateState;
        int checkState = (int)updateState;

        return currentState == checkState;
    }
}
