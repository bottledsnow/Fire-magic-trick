using UnityEngine;

public class EnemyDamage_BBullet : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private int damage;

    [Header("KickBack")]
    [SerializeField] private float force;
    [SerializeField] private Transform knockBackCoordinate;

    bool trigger;
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
            if(!trigger)
            {
                DamagePlayer(collider.gameObject);
            }
        }
    }

    private void DamagePlayer(GameObject player)
    {
        trigger = true;
        HealthSystem healthSystem = player.GetComponent<HealthSystem>();

        Vector3 Direction = (player.transform.position- knockBackCoordinate.position).normalized;
        Vector3 ForceDirection = Direction;

        healthSystem.ToDamagePlayer(damage, ForceDirection * force);
    }
    public void DestroyObject()
    {
        Destroy(transform.parent.gameObject);
    }
}
