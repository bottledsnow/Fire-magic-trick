using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class HookShoot_Shoot : Action
{
    [Header("SharedVariable")]
    [SerializeField] private SharedGameObject targetObject;
    [SerializeField] private SharedTransform behaviorObject;

    [Header("hookPrefab")]
    [SerializeField] private GameObject hookPrefab;
    [SerializeField] private float hookSpeed = 60;

    [Header("Player")]
    [SerializeField] private float playerHeight = 0.3f;

    //Behavior object
    private Transform hookPoint;
    //private GameObject originalHook;


    public override void OnStart()
    {
        hookPoint = behaviorObject.Value.Find("HookPoint");
        //originalHook = behaviorObject.Value.Find("OriginalHook").gameObject;

        //originalHook.SetActive(false);
    }

    public override TaskStatus OnUpdate()
    {
        if (hookPrefab != null && hookPoint!= null)
        {
            GameObject hook = Object.Instantiate(hookPrefab, hookPoint.position, hookPoint.rotation);
            Rigidbody hookRigidbody = hook.GetComponent<Rigidbody>();
            hook.GetComponent<EnemyDamage>().forcePoint = transform;

            if (hookRigidbody != null)
            {
                Vector3 direction = (new Vector3(0, playerHeight, 0) + targetObject.Value.transform.position - hookPoint.position).normalized;
                hookRigidbody.AddForce(direction * hookSpeed, ForceMode.Impulse);
            }
        }
        return TaskStatus.Success;
    }
}