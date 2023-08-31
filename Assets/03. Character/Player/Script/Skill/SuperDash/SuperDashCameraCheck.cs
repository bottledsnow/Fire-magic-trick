using UnityEngine;

public class SuperDashCameraCheck : MonoBehaviour
{
    [Header("CheckRay")]
    public GameObject Target;

    [SerializeField] private LayerMask mask;
    [SerializeField] private float rayDistance = 10f;

    private CrosshairUI _crosshairUI;
    private bool isHit = false;

    private void Start()
    {
        _crosshairUI = GameManager.singleton.UISystem.GetComponent<CrosshairUI>();
    }

    private void Update()
    {
        Check();
        Check_UI();
    }
    private void Check()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
        RaycastHit hit;
        RayHitCheck(ray,out hit);
        GetTarget(hit);
    }
    private void RayHitCheck(Ray ray,out RaycastHit hit)
    {
        isHit = Physics.Raycast(ray, out hit, rayDistance, mask);
    }
    private void GetTarget(RaycastHit hit)
    {
        Target = isHit ? hit.collider.gameObject : null;
    }

    private void Check_UI()
    {
        if(isHit)
        {
            _crosshairUI.SuperDashCheck();
        }else
        {
            _crosshairUI.CrosshairInitialization();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * rayDistance);
    }
}
