using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 與玩家距離條件節點
public class DistanceToPlayerConditionNode : BehaviorTreeNode
{
    private BehaviorTreeBlackboard behaviorTreeBlackboard;
    private float min;
    private float max;

    public DistanceToPlayerConditionNode(BehaviorTreeBlackboard behaviorTreeBlackboard, float min, float max)
    {
        this.behaviorTreeBlackboard = behaviorTreeBlackboard;
        this.min = min;
        this.max = max;
    }

    public override BehaviorTreeNodeState Execute()
    {
        if (max >= behaviorTreeBlackboard.distanceToPlayer && behaviorTreeBlackboard.distanceToPlayer >= min)
        {
            return BehaviorTreeNodeState.Success;
        }
        else
        {
            return BehaviorTreeNodeState.Failure;
        }
    }
}
