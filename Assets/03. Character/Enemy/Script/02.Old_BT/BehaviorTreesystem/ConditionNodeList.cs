using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 與玩家距離條件節點
public class DistanceToPlayerConditionNode : BehaviorTreeNode
{
    private BehaviorTreeBlackboard behaviorTreeBlackboard;
    private float min;
    private float max;

    public DistanceToPlayerConditionNode(BehaviorTreeBlackboard behaviorTreeBlackboard, float min=-1, float max=-1)
    {
        this.behaviorTreeBlackboard = behaviorTreeBlackboard;
        this.min = min;
        this.max = max;
    }

    public override BehaviorTreeNodeState Execute()
    {
        if(min <= 0)
        {
            if (max >= behaviorTreeBlackboard.distanceToPlayer)
            {
                return BehaviorTreeNodeState.Success;
            }
            else
            {
                return BehaviorTreeNodeState.Failure;
            }
        }
        else if(max <= 0)
        {
            if(behaviorTreeBlackboard.distanceToPlayer >= min)
            {
                return BehaviorTreeNodeState.Success;
            }
            else
            {
                return BehaviorTreeNodeState.Failure;
            }
        }
        else
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
}
