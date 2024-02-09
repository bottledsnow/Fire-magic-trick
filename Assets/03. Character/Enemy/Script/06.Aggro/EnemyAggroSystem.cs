using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class EnemyAggroSystem : MonoBehaviour
{
    [Header("Aggro")]
    [SerializeField] float maxAggro;
    [SerializeField] float aggroValue;

    [Header("FieldOfView")]
    [SerializeField] public bool showFovEditor;
    [SerializeField] public float fovRadius;
    [SerializeField] [Range(0, 360)] public float fovAngle;
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstructionMask;

    [Header("Teammate")]
    [SerializeField] bool isCallNearbyEnemy;
    [SerializeField] float nearbyRange;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] [Range(0, 0.2f)] float callingDelay = 0.05f;

    BehaviorTree behaviorTree;

    private void Awake()
    {
        behaviorTree = GetComponent<BehaviorTree>();
    }
    void Start()
    {
    }

    void Update()
    {
        FieldOfView();
    }

    void FieldOfView()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, fovRadius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < fovAngle / 2 && !Physics.Raycast(transform.position, directionToTarget, fovRadius, obstructionMask))
            {
                if (aggroValue <= 0) CallNearbyEnemy(rangeChecks[0].gameObject);
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

    public void CallNearbyEnemy(GameObject target)
    {
        if (isCallNearbyEnemy)
        {
            foreach (GameObject enemy in NearbyEnemy())
            {
                StartCoroutine(SetAggroWithDistanceDelay(target));
            }
        }
    }

    IEnumerator SetAggroWithDistanceDelay(GameObject target)
    {
        foreach (GameObject enemy in NearbyEnemy())
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            yield return new WaitForSeconds(distanceToEnemy * callingDelay);

            enemy.GetComponent<EnemyAggroSystem>().SetAggroTarget(target);
        }
    }

    #region AggroSetting
    public void SetAggroTarget(GameObject target)
    {
        behaviorTree.SetVariableValue("targetObject", target);
        aggroValue = maxAggro;
    }

    public void ReduceAggro()
    {
        aggroValue--;

        if (aggroValue <= 0)
        {
            CleanAggroTarget();
        }
    }

    public void CleanAggroTarget()
    {
        behaviorTree.SetVariableValue("targetObject", null);
        aggroValue = 0;
    }
    #endregion

    GameObject[] NearbyEnemy()
    {
        Collider[] i = Physics.OverlapSphere(transform.position, nearbyRange, enemyMask);
        List<GameObject> nearbyEnemy = new List<GameObject>();

        foreach (Collider collider in i)
        {
            EnemyAggroSystem aggroSystem = collider.GetComponent<EnemyAggroSystem>();

            if (aggroSystem != null && collider != GetComponent<Collider>())
            {
                nearbyEnemy.Add(collider.gameObject);
            }
        }

        return nearbyEnemy.ToArray();
    }
}
