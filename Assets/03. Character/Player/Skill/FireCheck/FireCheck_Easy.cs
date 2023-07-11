using UnityEngine;

public class FireCheck_Easy : MonoBehaviour
{
    public LayerMask mask;
    public Transform testposition;
    public Transform FirePoint;
    public float rayDistance = 10f; // Éä¾€µÄéL¶È
    public bool isChooseFirePoint;
    private IFirePoint firePoint;

    private bool isHit = false;
    
    private void Update()
    {
        Check();
    }
    private void Check()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance, mask))
        {
            ChoosePoint(hit);
        }
        else
        {
            NotChoosePoint();
        }
    }
    public void DestroyFirePoint()
    {
        if ( firePoint != null )
        {
            firePoint.DestroyChoosePoint();
            firePoint = null;
        }
    }
    public void AbsorbFire()
    {
        if ( firePoint != null )
        {
            firePoint.AbsorbFire();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
    }
    private void ChoosePoint(RaycastHit hit)
    {
        if (!isHit)
        {
            isHit = true;
            if (hit.collider.gameObject.tag == "FirePoint")
            {
                FirePoint = hit.collider.gameObject.transform;
                isChooseFirePoint = true;
                firePoint = hit.collider.GetComponent<IFirePoint>();
                firePoint.PlayerChoosePoint();
            }
        }
    }
    private void NotChoosePoint()
    {
        if (isHit)
        {
            isHit = false;
            isChooseFirePoint = false;
            if (firePoint != null)
                firePoint.PlayerNotChoosePoint();
            FirePoint = null;
        }
    }
}
