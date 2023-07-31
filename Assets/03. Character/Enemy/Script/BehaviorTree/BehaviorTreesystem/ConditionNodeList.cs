using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 與玩家距離條件節點
public class DistanceToPlayerConditionNode : BehaviorTreeNode
{
    private BehaviorTreeBlackboard behaviorTreeBlackboard;

    public DistanceToPlayerConditionNode(BehaviorTreeBlackboard behaviorTreeBlackboard)
    {
        this.behaviorTreeBlackboard = behaviorTreeBlackboard;
    }

    public override BehaviorTreeNodeState Execute()
    {
        if(behaviorTreeBlackboard.distanceToPlayer <= 5)
        {
            return BehaviorTreeNodeState.Success;
        }
        else
        {
            return BehaviorTreeNodeState.Failure;
        }
    }
}
