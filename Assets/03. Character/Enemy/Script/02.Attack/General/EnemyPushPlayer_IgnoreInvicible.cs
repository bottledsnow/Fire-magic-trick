using UnityEngine;

public class EnemyPushPlayer_IgnoreInvicible : MonoBehaviour
{
    [Header("KickBack")]
    [SerializeField] private float force;
    [SerializeField] private Transform knockBackCoordinate;
    [SerializeField] private bool isVertical;

    Rigidbody rb;

    private void Start()
    {
        if (knockBackCoordinate == null)
        {
            knockBackCoordinate = this.transform.parent.parent;
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            PushPlayer(collider.gameObject);
        }
    }

    private void PushPlayer(GameObject player)
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

        healthSystem.ToPushPlayer(ForceDirection * force);
    }
}
