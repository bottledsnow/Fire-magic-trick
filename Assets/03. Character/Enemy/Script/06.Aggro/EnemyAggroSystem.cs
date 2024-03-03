using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class EnemyAggroSystem : MonoBehaviour
{
    [Header("IdleType")]
    [SerializeField] public IdleActionType idleActionType;
    public enum IdleActionType
    {
        Patrol, //巡邏
        Construct //建築
    }
    [Header("Aggro")]
    [SerializeField] float maxAggro;
    [SerializeField] float aggroValue;

    [Header("FieldOfView")]
    [SerializeField] public bool showFovEditor;
    [SerializeField] public float fovRadius;
    [SerializeField] [Range(0, 360)] public float fovAngle;
    [SerializeField] public float viewHeight;
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstructionMask;

    [Header("Teammate")]
    [SerializeField] bool isCallNearbyEnemy;
    [SerializeField] float nearbyRange;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] [Range(0, 0.2f)] float callingDelay = 0.05f;

    BehaviorTree behaviorTree;
    Vector3 viewPosition;
    bool stopReducing;

    private void Awake()
    {
        behaviorTree = GetComponent<BehaviorTree>();
    }
    void Start()
    {
        viewPosition = new Vector3(transform.position.x , transform.position.y + viewHeight , transform.position.z);
    }

    void Update()
    {
        FieldOfView();
    }

    void FieldOfView()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(viewPosition, fovRadius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - viewPosition).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < fovAngle / 2 && !Physics.Raycast(viewPosition, directionToTarget, fovRadius, obstructionMask))
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
        // 根據距離延遲觸發
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
        // 不在停止減少狀態
        if(!stopReducing)
        {
            // 減少仇恨值
            aggroValue--;
        }
        // 仇恨值歸零時
        if (aggroValue <= 0)
        {
            // 消除仇恨目標
            CleanAggroTarget();
        }
    }

    public void FillAggro()
    {
        // 若有目標填滿仇恨值
        if(behaviorTree.GetVariable("targetObject") != null)
        {
            aggroValue = maxAggro;
        }
    }
    
    public void CleanAggroTarget()
    {
        behaviorTree.SetVariableValue("targetObject", null);
        aggroValue = 0;
    }
    public void GiveAggroTarget(GameObject player)
    {
        behaviorTree.SetVariableValue("targetObject", player);

        if (behaviorTree.GetVariable("targetObject") != null)
        {
            aggroValue = maxAggro;
        }
    }

    public void StopReducingController(bool isTrue)
    {
        stopReducing = isTrue;
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
