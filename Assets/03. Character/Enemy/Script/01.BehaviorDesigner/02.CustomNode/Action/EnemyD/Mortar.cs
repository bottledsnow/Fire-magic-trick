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
            Vector3 positions = ChooseTargetPosition();
            InstantiateAttackObject(positions);
        }


        // 顯示選擇的位置
        // foreach (Vector3 position in positions)
        // {
        //     InstantiateAttackObject(position);
        // }

        // unityEvent = UnityEventEnemy.Value.GetComponent<UnityEventEnemy_A>();
        // unityEvent.VFX_LegSlash_B();
    }

    public override TaskStatus OnUpdate()
    {
        if (Time.time - timer > duration) return TaskStatus.Success;
        return TaskStatus.Running;
    }

    private Vector3 ChooseTargetPosition()
    {
        Vector2 randomCircle = Random.insideUnitCircle.normalized * 5;
        Vector3 randomDirection = new Vector3(randomCircle.x, 0f, randomCircle.y);
        Vector3 randomPosition = targetObject.Value.transform.position + randomDirection;
        bool foundValidPosition = false;

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