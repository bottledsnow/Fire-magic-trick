using BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion;
using UnityEngine;

public class TrackSystem : MonoBehaviour
{
    [SerializeField] private LayerMask TrackLayerMask;
    [SerializeField] private float CheckDistance;
    [SerializeField] private float RotateSpeed;

    private RaycastHit HitInfo;
    private GameObject Target;
    private bool raycastHit;
    private bool Trigger;
    private void Update()
    {
        ShootRay();
        TrackRotate();
    }
    private void ShootRay()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        Ray ray = new Ray(origin, direction);

        raycastHit = Physics.Raycast(ray, out HitInfo, CheckDistance, TrackLayerMask);
    }
    private void TrackRotate()
    {
        if (raycastHit)
        {
            Trigger = true;
            Target = HitInfo.collider.gameObject;
        }

        if (Trigger)
        {
            Vector3 TargetDirection = Target.transform.position - this.transform.position;
            float singleStep = RotateSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(this.transform.forward, TargetDirection, singleStep, 0f);
            this.transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
    
}
