using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class EnemyTeamSystem : MonoBehaviour
{
    public enum EnemyType { A, B, C, D, Connie }

    [Header("EnemyType")]
    [SerializeField] public EnemyType enemyType;

    BehaviorTree behaviorTree;

    void Start()
    {
        behaviorTree = GetComponent<BehaviorTree>();
    }

    void Update()
    {
        
    }

    public void CoordinatedAttack()
    {
        Debug.Log("協同攻擊");
        behaviorTree.SendEvent("CoordinatedAttack");
    }
}
