using UnityEngine;

public class KickBackEnemy : MonoBehaviour
{
    //interfate
    private IHitNotifier[] hitNotifier;

    [Header("Force")]
    [SerializeField] private Transform BackCoordinate;
    [SerializeField] private float force;

    private void Start()
    {
        hitNotifier = GetComponents<IHitNotifier>();

        for (int i = 0; i < hitNotifier.Length; i++)
        {
            if (hitNotifier[i] != null)
            {
                hitNotifier[i].OnHit += kickBackEnemy;
            }
        }
    }
    public void kickBackEnemy(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyHealthSystem>() != null)
        {
            if (collision.gameObject.GetComponent<EnemyHealthSystem>().kickBackGuard != true) // Could be knock back
            {
                Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 Direction = (collision.transform.position - BackCoordinate.position).normalized;
                Vector3 ForceDirection = new Vector3(Direction.x, 0, Direction.z);
                rb.AddForce(ForceDirection * force * collision.gameObject.GetComponent<EnemyHealthSystem>().kickBackRatio, ForceMode.Impulse);
            }
        }
    }
    public void kickBackEnemy(Collision collision,float force)
    {
        if (collision.gameObject.GetComponent<EnemyHealthSystem>() != null)
        {
            if (collision.gameObject.GetComponent<EnemyHealthSystem>().kickBackGuard != true) // Could be knock back
            {
                Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 Direction = (collision.transform.position - BackCoordinate.position).normalized;
                Vector3 ForceDirection = new Vector3(Direction.x, 0, Direction.z);
                rb.AddForce(ForceDirection * force * collision.gameObject.GetComponent<EnemyHealthSystem>().kickBackRatio, ForceMode.Impulse);
            }
        }
    }
    public void kickBackEnemy(Collider other, float force)
    {
        if (other.gameObject.GetComponent<EnemyHealthSystem>() != null)
        {
            if (other.gameObject.GetComponent<EnemyHealthSystem>().kickBackGuard != true) // Could be knock back
            {
                Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
                Vector3 Direction = (other.transform.position - BackCoordinate.position).normalized;
                Vector3 ForceDirection = new Vector3(Direction.x, 0, Direction.z);
                rb.AddForce(ForceDirection * force * other.gameObject.GetComponent<EnemyHealthSystem>().kickBackRatio, ForceMode.Impulse);
            }
        }
    }
}
