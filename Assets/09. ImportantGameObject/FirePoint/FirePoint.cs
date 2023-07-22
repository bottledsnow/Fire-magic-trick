using UnityEngine;
using System.Threading.Tasks;

public class FirePoint : MonoBehaviour
{
    [SerializeField] private GameObject firePoint;
    [Header("Rebirth")]
    [SerializeField] private bool isRebirth;
    [SerializeField] private int rebirth_ms;

    private bool canChoosePoint = true;
    private Collider thisCollider;

    private void Start()
    {
        thisCollider = GetComponent<Collider>();
    }
    public void PlayerChoosePoint()
    {
        if (canChoosePoint && firePoint != null)
        {
            firePoint.SetActive(true);
        }
    }
    public void PlayerNotChoosePoint()
    {
        if (firePoint != null)
        {
            firePoint.SetActive(false);
        }
    }
    public void DestroyFirePoint()
    {
        if(isRebirth)
        {
            DestroyFirePoint(rebirth_ms);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public async void DestroyFirePoint(int rebirth_ms)
    {
        canChoosePoint = false;
        firePoint.gameObject.SetActive(false);
        thisCollider.enabled = false;
        await Task.Delay(rebirth_ms);
        canChoosePoint = true;
        thisCollider.enabled = true;
    }
}
