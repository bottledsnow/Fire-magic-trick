using UnityEngine;

public class ChargeRepel : MonoBehaviour
{
    public float repelForfce;
    private Rigidbody rb;
    private PlayerDamage _playerDamage;

    private void Start()
    {
        _playerDamage = GetComponent<PlayerDamage>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            rb = collision.gameObject.GetComponent<Rigidbody>();
            _playerDamage.ToDamageEnemy(collision);
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
