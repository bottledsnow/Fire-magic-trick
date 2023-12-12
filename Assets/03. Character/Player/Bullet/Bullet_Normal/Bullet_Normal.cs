using UnityEngine;
using UnityEngine.VFX;
using System.Threading.Tasks;

public class Bullet_Normal : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private float lifeTime;
    [SerializeField] private float speed;
    [Header("Force")]
    [SerializeField] private Transform BackCoordinate;
    [SerializeField] private float force;
    [Header("Feedback")]
    [SerializeField] private ParticleSystem addspeedFeedback;
    [Header("VfxPrefab")]
    [SerializeField] private GameObject cardSlashPrefab;
    [SerializeField] private GameObject hitEnemyPrefab;

    private Collider bulletCollider;
    private Rigidbody rb;
    private CrosshairUI _crosshairUI;
    private PlayerDamage _playerDamage;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        bulletCollider = GetComponent<Collider>();
        _crosshairUI = GameManager.singleton.UISystem.GetComponent<CrosshairUI>();
        _playerDamage = GetComponent<PlayerDamage>();
        Initialization();
    }
    private void Update()
    {
        GiveSpeed();
    }
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        //Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, collision.contacts[0].normal);
        Vector3 pos = contact.point;

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnergyCan"))
        {
            _playerDamage.ToDamageEnemy(collision);
            GameObject enemyhit = Instantiate(hitEnemyPrefab, pos, rot);
            KickBackEnemy(collision);
            Destroy(enemyhit, 1f);
        }

        newHit(pos, rot);
        CroshairFeedback();
        DestroyBullet();
    }
    private void KickBackEnemy(Collision collision)
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
    private void CroshairFeedback()
    {
        _crosshairUI.CrosshairHit();
    }
    private void newHit(Vector3 pos, Quaternion rot)
    {
        var hitVFX = Instantiate(cardSlashPrefab, pos, rot);
        var psHit = hitVFX.GetComponent<VisualEffect>();
        Destroy(hitVFX, 1.5f);
    }
    private void Initialization()
    {
        DestroyBullet(lifeTime);
    }
    private void GiveSpeed()
    {
        rb.velocity = transform.forward * speed;
    }
    private void DestroyBullet(float lifetime)
    {
        Destroy(gameObject, lifetime);
    }
    private void DestroyBullet()
    {
        bulletCollider.enabled = false;
        rb.drag = 100;
        Destroy(gameObject, 0.3f);
    }
}
