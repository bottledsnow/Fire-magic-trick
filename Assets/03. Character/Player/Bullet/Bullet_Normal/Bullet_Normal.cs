using UnityEngine;
using UnityEngine.VFX;

public class Bullet_Normal : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private float lifeTime;
    [SerializeField] private float speed;
    [Header("Preferb")]
    [SerializeField] private GameObject hitPrefab;

    private Collider bulletCollider;
    private Rigidbody rb;
    private CrosshairUI _crosshairUI;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        bulletCollider = GetComponent<Collider>();
        _crosshairUI = GameManager.singleton.UISystem.GetComponent<CrosshairUI>();
        Initialization();
    }
    private void Update()
    {
    }
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        newHit(pos, rot);
        CroshairFeedback();
        DestroyBullet();
    }
    private void CroshairFeedback()
    {
        _crosshairUI.CrosshairHit();
    }
    private void newHit(Vector3 pos, Quaternion rot)
    {
        var hitVFX = Instantiate(hitPrefab, pos, rot);
        var psHit = hitVFX.GetComponent<VisualEffect>();
        Destroy(hitVFX, 1.5f);

    }
    private void Initialization()
    {
        DestroyBullet(lifeTime);
        GiveSpeed();
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
        Destroy(gameObject,0.3f);
    }
}
