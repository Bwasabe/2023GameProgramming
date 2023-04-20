using System.Collections.Generic;

public abstract class BT_Condition : BT_Node
{
    protected BT_Condition(BehaviorTree tree, List<BT_Node> children) : base(tree, children) {}
    
}
