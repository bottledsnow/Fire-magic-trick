using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 巡邏節點
public class PatrolNode : BehaviorTreeNode
{
    public override BehaviorTreeNodeState Execute()
    {
        // TODO: 實現巡邏行為的代碼
        Debug.Log("巡邏");
        return BehaviorTreeNodeState.Success;
    }
}

// 追逐節點
public class ChaseNode : BehaviorTreeNode
{
    public override BehaviorTreeNodeState Execute()
    {
        // TODO: 實現追逐行為的代碼
        Debug.Log("追逐");
        return BehaviorTreeNodeState.Success;
    }
}

// 攻擊節點
public class AttackNode : BehaviorTreeNode
{
    public override BehaviorTreeNodeState Execute()
    {
        // TODO: 實現攻擊行為的代碼
        Debug.Log("攻擊");
        return BehaviorTreeNodeState.Success;
    }
}
