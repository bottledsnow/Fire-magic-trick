using UnityEngine;

public class SuperJump : MonoBehaviour
{
    [SerializeField] private ParticleSystem VFX_SuperJumpStart;
    [SerializeField] private float Force = 10f;

    //Script
    private PlayerDamage playerDamage;

    private void Start()
    {
        playerDamage = GetComponent<PlayerDamage>();
        Destroy(gameObject, 1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            playerDamage.ToDamageEnemy(other);

            Vector3 dir = (other.transform.position - transform.position).normalized;
            Rigidbody rb = other.GetComponent<Rigidbody>();

            rb.AddForce(dir * Force, ForceMode.Impulse);
        }
    }
}
