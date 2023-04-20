using System;
using System.Collections.Generic;

public class BT_Selector : BT_Node
{
    public BT_Selector(BehaviorTree tree, List<BT_Node> children) : base(tree, children) {}

    protected override void OnUpdate()
    {
        foreach (BT_Node child in _children)
        {
            NodeResult nodeResult = child.Execute();

            switch (nodeResult)
            {
            case NodeResult.SUCCESS:
                _nodeResult = NodeResult.SUCCESS;
                UpdateState = UpdateState.Exit;
                return;
            case NodeResult.FAILURE:
                _nodeResult = NodeResult.FAILURE;

                continue;
            }
        }
    }
    
}
