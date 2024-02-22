using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Mortar : Action
{
    [Header("SharedVariable")]
    [SerializeField] private SharedTransform behaviorObject;
    [SerializeField] private SharedGameObject targetObject;
    [SerializeField] private SharedGameObject UnityEventEnemy;

    [Header("AttackObject")]
    [SerializeField] private GameObject mortar;

    [Header("WaitTime")]
    [SerializeField] private float duration = 0.5f;

    private Transform legSlashPoint;
    private Rigidbody rb;
    private UnityEventEnemy_A unityEvent;
    private float timer;

    public LayerMask obstacleLayer; // 位置之間的最大水平距離

    public override void OnStart()
    {
        timer = Time.time;


        for (int i = 0; i < 3; i++)
        {
            // 設定並生成三個迫擊砲
            Vector3 positions;
            switch(i)
            {
                case 0:
                    positions = ChooseTargetPosition(0,1);
                    InstantiateAttackObject(positions);
                    break;
                case 1:
                    positions = ChooseTargetPosition(4,5);
                    InstantiateAttackObject(positions);
                    break;
                case 2:
                    positions = ChooseTargetPosition(6,7);
                    InstantiateAttackObject(positions);
                    break;
            }
        }
    }

    public override TaskStatus OnUpdate()
    {
        if(targetObject.Value == null) return TaskStatus.Failure;
        if (Time.time - timer > duration) return TaskStatus.Success;
        return TaskStatus.Running;
    }

    private Vector3 ChooseTargetPosition(float minRadius, float maxRadius)
    {
        float randomRadius = Random.Range(minRadius * 10, maxRadius * 10) / 10;
        Vector2 randomCircle = Random.insideUnitCircle.normalized * randomRadius;
        Vector3 randomDirection = new Vector3(randomCircle.x, 0f, randomCircle.y);
        Vector3 randomPosition = targetObject.Value.transform.position + randomDirection;
        bool foundValidPosition = false;

        // 被屋頂擋住
        while (!foundValidPosition)
        {
            RaycastHit hit;
            if (Physics.Raycast(new Vector3(randomPosition.x, 100f, randomPosition.z), Vector3.down, out hit, Mathf.Infinity, obstacleLayer))
            {
                randomPosition.y = hit.point.y;
                foundValidPosition = true;
            }
            else
            {
                randomCircle = Random.insideUnitCircle.normalized * 5;
                randomDirection = new Vector3(randomCircle.x, 0f, randomCircle.y);
                randomPosition = targetObject.Value.transform.position + randomDirection;
            }
        }
        return randomPosition;
    }

    private void InstantiateAttackObject(Vector3 targetPosition)
    {
        GameObject mortarObject = Object.Instantiate(mortar, targetPosition, Quaternion.identity);
    }
}