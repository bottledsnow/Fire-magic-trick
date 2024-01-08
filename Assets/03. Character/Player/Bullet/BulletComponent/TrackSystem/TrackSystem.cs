using UnityEngine;

public class TrackSystem : MonoBehaviour
{
    [Header("Track System")]
    [SerializeField] private LayerMask TrackLayerMask;
    [SerializeField] private AnimationCurve TrackCurve;
    [SerializeField] private float onTrackSpeed;
    [SerializeField] private float CheckDistance;
    
    [Header("Rotate")]
    [SerializeField] private float smoothRotateTime;
    [SerializeField] private float MaxRotateSpeed;
    [SerializeField] private float RotateSpeed;

    //Script
    private RaycastHit HitInfo;
    private GameObject Target;
    private Bullet bullet;
    private BulletRotation bulletRotation;

    //variable
    private bool raycastHit;
    private bool isRotate;
    private float deltaTime;
    private float rotateTimer;

    private void Start()
    {
        bullet = GetComponent<Bullet>();
        bulletRotation = GetComponent<BulletRotation>();
    }
    private void Update()
    {
        shootRay();
        trackRotate();
        trackRotateTimer();
    }
    private void shootRay()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        Ray ray = new Ray(origin, direction);

        raycastHit = Physics.Raycast(ray, out HitInfo, CheckDistance, TrackLayerMask);
    }
    private void trackRotateTimer()
    {
        if (isRotate)
        {
            rotateTimer += Time.deltaTime;
            deltaTime = rotateTimer / smoothRotateTime;
        }

        if(smoothRotateTime <= rotateTimer)
        {
            RotateSpeed = MaxRotateSpeed;
        }else
        {
            RotateSpeed = TrackCurve.Evaluate(deltaTime) * MaxRotateSpeed;
        }
    }
    private void trackRotate()
    {
        if (raycastHit)
        {
            SetIsRotate(true);
            this.transform.parent = null;
            bullet.SetSpeed(onTrackSpeed);
            if(bulletRotation != null) bulletRotation.OnTrack();
            Target = HitInfo.collider.gameObject;
        }

        if (isRotate)
        {
            Vector3 TargetDirection = Target.transform.position - this.transform.position;
            float singleStep = RotateSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(this.transform.forward, TargetDirection, singleStep, 0f);
            this.transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
    private void SetIsRotate(bool value)
    {
        isRotate = value;
    }
}
