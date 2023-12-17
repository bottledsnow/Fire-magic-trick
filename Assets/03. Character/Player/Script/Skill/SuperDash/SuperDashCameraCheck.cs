using Unity.VisualScripting;
using UnityEngine;

public class SuperDashCameraCheck : MonoBehaviour
{
    [Header("CheckRay")]
    public GameObject Target;

    [SerializeField] private LayerMask rayMask;
    [SerializeField] private LayerMask usefullMask;
    [SerializeField] private float rayDistance = 10f;

    private SuperDash _superDash;
    private CrosshairUI _crosshairUI;
    private bool isHit = false;

    private void Start()
    {
        _crosshairUI = GameManager.singleton.UISystem.GetComponent<CrosshairUI>();
        _superDash = GetComponent<SuperDash>();
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

        if(hit.collider != null)
        {
            GetTarget(hit);
        }
    }
    private void RayHitCheck(Ray ray,out RaycastHit hit)
    {
        if(Physics.Raycast(ray, out hit, rayDistance, rayMask))
        {
            if(hit.collider.gameObject.CompareTag("SuperDashTarget"))
            {
                isHit = true;
            }else
            {
                isHit = false;
            }
        }
    }
    private void GetTarget(RaycastHit hit)
    {
        if(!_superDash.isSuperDash)
        {
            if(isHit)
            {
                if(hit.collider.gameObject !=null)
                {
                    Target = hit.collider.gameObject;
                }
            }
            else
            {
                Target = null;
            }
        }
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
