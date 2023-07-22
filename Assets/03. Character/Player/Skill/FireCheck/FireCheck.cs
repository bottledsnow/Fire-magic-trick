using System.ComponentModel;
using UnityEngine;

public class FireCheck : MonoBehaviour
{
    [Header("CheckRay")]
    [SerializeField] private bool isHit = false;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float rayDistance = 10f; // Éä¾€µÄéL¶È

    [HideInInspector] public Transform FirePoint;
    [HideInInspector] public bool isChoosePoint = false;
    

    private FirePoint _firePoint;
    private bool needInitialization;
    private void Update()
    {
        Check();
        Initialization();
    }
    
    private void Check()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
        RaycastHit hit;
        RayHitCheck(ray,out hit);
        ChoosePoint(hit);
    }
    private void RayHitCheck(Ray ray,out RaycastHit hit)
    {
        isHit = Physics.Raycast(ray, out hit, rayDistance, mask);
    }
    private void ChoosePoint(RaycastHit hit)
    {
        if (isHit && !isChoosePoint && hit.collider.CompareTag("FirePoint"))
        {
            FirePoint = hit.collider.gameObject.transform;
            _firePoint = hit.collider.GetComponent<FirePoint>();
            _firePoint.PlayerChoosePoint();
            isChoosePoint = true;
            needInitialization = true;
        }
    }
    private void Initialization()
    {
        if(needInitialization && !isHit)
        {
            needInitialization = false;
            if (_firePoint != null)
            {
                _firePoint.PlayerNotChoosePoint();
            }
            isChoosePoint = false;
            FirePoint = null;
        }
    }
    public void AbsorbFire()
    {
        if (_firePoint != null)
        {
            FireAbsorb fireAbsorb = _firePoint.GetComponent<FireAbsorb>();
            fireAbsorb.enabled = true;
        }
    }
    public void DestroyFirePoint()
    {
        if (_firePoint != null)
        {
            _firePoint.DestroyFirePoint();
            _firePoint = null;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
    }
}
