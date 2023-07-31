using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 行為樹類
public class Enemy_BehaviorTreeTest : MonoBehaviour
{
    private BehaviorTreeNode root;
    private BehaviorTreeBlackboard blackboard;

    void Start()
    {
        // 行為樹節點
        BehaviorTreeNode patrolNode = new PatrolNode();
        BehaviorTreeNode chaseNode = new ChaseNode();
        BehaviorTreeNode attackNode = new AttackNode();
        blackboard = this.GetComponent<BehaviorTreeBlackboard>();

        // 建立行為樹
        root = new SelectorNode(
            new SequenceNode(
                new DistanceToPlayerConditionNode(blackboard),
                new SequenceNode(attackNode)
            ),
            new SequenceNode(patrolNode)
        );
    }

    // 執行行為樹
    void Update()
    {
        root.Execute();
    }

}
