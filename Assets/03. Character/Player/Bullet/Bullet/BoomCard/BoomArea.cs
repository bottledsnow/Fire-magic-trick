using UnityEngine;
using System.Threading.Tasks;

public class BoomArea : MonoBehaviour
{
    [Header("Boom Area")]
    [SerializeField] private float delay = 1.5f;
    [SerializeField] private int damage = 0;
    [SerializeField] private float forceToEnemy = 30;
    [SerializeField] private float forceToPlayer = 90;

    //interface
    private IHealth health;

    //Scritp
    private ImpactReceiver impactReceiver;
    private KickBackEnemy kickBackEnemy;
    private Collider coli;

    private void Start()
    {
        impactReceiver = GameManager.singleton.Player.GetComponent<ImpactReceiver>();
        kickBackEnemy = GetComponent<KickBackEnemy>();
        coli = GetComponent<Collider>();

        delayBoom();
        Destroy(this.gameObject, delay+0.2f);
    }
    private async void delayBoom()
    {
        await Task.Delay((int)(delay * 1000));
        coli.enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            health = other.GetComponent<IHealth>();
            health.TakeDamage(damage, PlayerDamage.DamageType.NormalShoot);
            kickBackEnemy.kickBackEnemy(other, forceToEnemy);
        }
        if(other.CompareTag("Player"))
        {
            Debug.Log("Boom Player");
            Vector3 direction = (other.transform.position - this.transform.position).normalized;
            Vector3 directionXZ = new Vector3(direction.x, 0, direction.z);
            Vector3 directionY = new Vector3(0, direction.y, 0);
            impactReceiver.ToImpact(directionXZ*0.3f*forceToPlayer+directionY*0.6f*forceToPlayer);
        }
    }
}
