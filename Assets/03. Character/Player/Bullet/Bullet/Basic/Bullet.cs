using UnityEngine;
using UnityEngine.VFX;

public class Bullet : MonoBehaviour, IHitNotifier
{
    [Header("Bullet")]
    [SerializeField] protected GameObject hitEnemyPrefab;
    [SerializeField] protected GameObject cardSlashPrefab;
    [SerializeField] protected float lifeTime;
    [SerializeField] protected float speed;

    //Script
    private CrosshairUI crosshairUI;
    private Rigidbody rb;
    private Collider coli;

    //delegate
    public event MyDelegates.OnHitHandler OnHit;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        coli = GetComponent<Collider>();
        crosshairUI = GameManager.singleton.UISystem.GetComponent<CrosshairUI>();

        Destroy(gameObject, lifeTime);
    }
    protected virtual void Update()
    {
        GiveSpeed();
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Contact 
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, collision.contacts[0].normal);
        Vector3 pos = contact.point;

        //Hit
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnergyCan"))
        {
            OnHit?.Invoke(collision);
            OnHitEnemy();
            GameObject enemyhit = Instantiate(hitEnemyPrefab, pos, rot);
            Destroy(enemyhit, 1f);
        }
        OnHitSomething();

        newHit(pos, rot);
        CroshairFeedback();
        DestroyBullet();
    }
    protected virtual void OnHitEnemy() { }
    protected virtual void OnHitSomething() { }
    private void newHit(Vector3 pos, Quaternion rot)
    {
        var hitVFX = Instantiate(cardSlashPrefab, pos, rot);
        var psHit = hitVFX.GetComponent<VisualEffect>();
        Destroy(hitVFX, 1.5f);
    }
    private void GiveSpeed()
    {
        rb.velocity = transform.forward * speed;
    }
    private void CroshairFeedback()
    {
        crosshairUI.CrosshairHit();
    }
    private void DestroyBullet()
    {
        coli.enabled = false;
        rb.drag = 100;
        Destroy(gameObject, 0.3f);
    }
}
