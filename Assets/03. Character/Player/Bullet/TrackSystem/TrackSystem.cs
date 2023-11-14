using UnityEngine;

public class TrackSystem : MonoBehaviour
{
    [SerializeField] private LayerMask TrackLayerMask;
    [SerializeField] private float CheckDistance;
    [SerializeField] private float RotateSpeed;

    private RaycastHit hit;
    private bool raycastHit;
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

        raycastHit = Physics.Raycast(ray, out hit, CheckDistance, TrackLayerMask);
    }
    private void TrackRotate()
    {
        if (raycastHit)
        {
            Debug.Log("Hit Target");    
            Vector3 TargetDirection = hit.transform.position - this.transform.position;
            float singleStep = RotateSpeed * Time.deltaTime;
            Vector3 newRotate = Vector3.RotateTowards(this.transform.forward, TargetDirection, singleStep, 0f);
            this.transform.rotation = Quaternion.Euler(newRotate);
        }
    }
}
