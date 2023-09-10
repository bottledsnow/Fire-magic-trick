using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 基本行為樹節點類
public abstract class BehaviorTreeNode
{
    // 執行行為與返回結果
    public abstract BehaviorTreeNodeState Execute();
}
public enum BehaviorTreeNodeState
{
    Success,  // 行為成功
    Running,  //行為執行中
    Failure   // 行為失敗
}

// 順序節點，遇到否時返回否
public class SequenceNode : BehaviorTreeNode
{
    private List<BehaviorTreeNode> childNodes = new List<BehaviorTreeNode>();

    public SequenceNode(params BehaviorTreeNode[] nodes)
    {
        childNodes.AddRange(nodes);
    }

    public override BehaviorTreeNodeState Execute()
    {
        foreach (var node in childNodes)
        {
            if (node.Execute() == BehaviorTreeNodeState.Failure)
            {
                return BehaviorTreeNodeState.Failure;
            }
        }
        return BehaviorTreeNodeState.Success;
    }
}
// 選擇節點，遇到是時返回是
public class SelectorNode : BehaviorTreeNode
{
    private List<BehaviorTreeNode> childNodes = new List<BehaviorTreeNode>();

    public SelectorNode(params BehaviorTreeNode[] nodes)
    {
        childNodes.AddRange(nodes);
    }

    public override BehaviorTreeNodeState Execute()
    {
        foreach (var node in childNodes)
        {
            if (node.Execute() == BehaviorTreeNodeState.Success)
            {
                return BehaviorTreeNodeState.Success;
            }
        }
        return BehaviorTreeNodeState.Failure;
    }
}