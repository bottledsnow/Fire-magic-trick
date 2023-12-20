using UnityEngine;

public class EnemyDamage_BBullet : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private int damage;

    [Header("KickBack")]
    [SerializeField] private float force;
    [SerializeField] private Transform knockBackCoordinate;

    Rigidbody rb;

    private void Start()
    {
        if(knockBackCoordinate == null)
        {
            knockBackCoordinate = this.transform.parent.parent;
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            DamagePlayer(collider.gameObject);
        }
    }

    private void DamagePlayer(GameObject player)
    {
        HealthSystem healthSystem = player.GetComponent<HealthSystem>();

        Vector3 Direction = (knockBackCoordinate.position- player.transform.position).normalized;
        Vector3 ForceDirection = Direction;


        healthSystem.ToDamagePlayer(0, ForceDirection * force);
    }
    public void DestroyObject()
    {
        Destroy(transform.parent.gameObject);
    }
}
