using UnityEngine;

public class FireDashCollider : MonoBehaviour
{
    [SerializeField] private FireDash _fireDash;
    private float CrashForce;
    private bool IsDash;
    private void Start()
    {
        _fireDash = GameManager.singleton.EnergySystem.GetComponent<FireDash>();
        Initialization();
    }
    private void Initialization()
    {
        CrashForce = _fireDash.CrashForce;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (IsDash)
        {
            if (other.CompareTag("Enemy"))
            {
                Debug.Log("Hit");
                Vector3 playerposition = transform.parent.transform.position;
                Vector3 EnemyPosition = other.transform.position;
                Vector3 direction = (EnemyPosition - playerposition).normalized;
                Vector3 Enemyup = other.transform.up;
                other.GetComponent<Rigidbody>().velocity = direction * CrashForce + Enemyup * CrashForce / 2;
            }
        }
    }
    public void SetIsDash(bool value)
    {
        IsDash = value;
    }
}
