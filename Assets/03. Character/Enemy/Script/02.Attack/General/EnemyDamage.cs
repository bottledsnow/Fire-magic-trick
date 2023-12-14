using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private int damage;

    [Header("KickBack")]
    [SerializeField] private float force;
    [SerializeField] private Transform knockBackCoordinate;
    [SerializeField] private bool isVertical;

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

        Vector3 Direction = (player.transform.position - knockBackCoordinate.position).normalized;
        Vector3 ForceDirection = Direction;

        if (!isVertical)
        {
            ForceDirection = new Vector3(Direction.x, 0, Direction.z);
        }
        else
        {
            ForceDirection = new Vector3(0, Direction.y, 0);
        }

        healthSystem.ToDamagePlayer(0, ForceDirection * force);
    }
    public void DestroyObject()
    {
        Destroy(transform.parent.gameObject);
    }
}
