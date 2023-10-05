using UnityEngine;

public class ChargeRepel : MonoBehaviour
{
    [SerializeField] private float repelForfce = 10f;
    private Rigidbody rb;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            rb = collision.gameObject.GetComponent<Rigidbody>();
            ToRepel(rb);
        }
    }
    private void ToRepel(Rigidbody rb)
    {
        Vector3 BulletPosition = transform.position;
        Vector3 EnemyPosition = rb.transform.position;
        Vector3 Direction = (EnemyPosition - BulletPosition).normalized;
        
        rb.velocity = Direction * repelForfce;
    }
}
