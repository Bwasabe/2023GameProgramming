using System.Collections.Generic;

public class BT_Sequence : BT_Node
{
    public BT_Sequence(BehaviorTree tree, List<BT_Node> children) : base(tree, children) {}

    protected override void OnUpdate()
    {
        foreach (BT_Node child in _children)
        {
            NodeResult nodeResult = child.Execute();

            switch (nodeResult)
            {
            case NodeResult.SUCCESS:
                _nodeResult = NodeResult.SUCCESS;
                continue;
            case NodeResult.FAILURE:
                _nodeResult = NodeResult.FAILURE;
                UpdateState = UpdateState.Exit;
                return;

            }
        }
    }
}
