using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class EnemyAggroSystem : MonoBehaviour
{
    [Header("Aggro")]
    [SerializeField] float maxAggro;
    [SerializeField] float aggroValue;

    [Header("DetectArea")]
    [SerializeField] private float radius;
    [SerializeField] private float angle;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstructionMask;

    BehaviorTree behaviorTree;

    void Start()
    {
        behaviorTree = GetComponent<BehaviorTree>();
    }

    void Update()
    {
        FieldOfView();
    }

    void FieldOfView()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2 && !Physics.Raycast(transform.position, directionToTarget, radius, obstructionMask))
            {
                SetAggroTarget(rangeChecks[0].gameObject);
            }
            else
            {
                ReduceAggro();
            }
        }
        else
        {
            ReduceAggro();
        }
    }

    public void SetAggroTarget(GameObject target)
    {
        behaviorTree.SetVariableValue("targetObject", target);
        aggroValue = maxAggro;
    }

    public void ReduceAggro()
    {
        aggroValue--;
        
        if(aggroValue<=0)
        {
            CleanAggroTarget();
        }
    }

    public void CleanAggroTarget()
    {
        behaviorTree.SetVariableValue("targetObject", null);
        aggroValue = 0;
    }
}
