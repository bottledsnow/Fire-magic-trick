using UnityEngine;
using System.Threading.Tasks;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.DebugUI.Table;
using Unity.VisualScripting;

public class FireCard : MonoBehaviour
{
    public float LifeTime;
    public float speed;
    public float fireRate;
    public GameObject muzzlePrefab;
    public GameObject hitPrefab;
    public GameObject FireAsh;
    private Vector3 Hitposition;
    [SerializeField] private FireCard mCard;
    void Start()
    {
        mCard = this.GetComponent<FireCard>();
        ShootMuzzle();
        explode();
    }

    void Update()
    {
        GiveSpeed();
    }

    void OnCollisionEnter(Collision co)
    {
        ContactPoint contact = co.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        Hitposition = pos;
        hitTarget(pos, rot);
        explodeByHit();
    }
    private void ShootMuzzle()
    {
        if (muzzlePrefab != null)
        {
            var muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
            muzzleVFX.transform.forward = gameObject.transform.forward;
            var psMuzzle = muzzleVFX.GetComponent<ParticleSystem>();
            if (psMuzzle != null)
            {
                Destroy(muzzleVFX, psMuzzle.main.duration);
            }
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }
    }
    private void hitTarget(Vector3 pos,Quaternion rot)
    {
        speed = 0;

        if (hitPrefab != null)
        {
            var hitVFX = Instantiate(hitPrefab, pos, rot);
            var psHit = hitVFX.GetComponent<ParticleSystem>();
            if (psHit != null)
            {
                Destroy(hitVFX, psHit.main.duration);
            }
            else
            {
                var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitVFX, psChild.main.duration);
            }
        }
        Destroy(gameObject);
    }
    private void GiveSpeed()
    {
        if (speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("No Speed");
        }
    }
    private async void explode()
    {
        await Task.Delay((int)(1000 * LifeTime));
        if(mCard != null )
        {
            hitTarget(this.transform.position, Quaternion.identity);
            Instantiate(FireAsh, this.transform.position, Quaternion.identity);
        }else
        {
            Debug.Log("No Card");
        }
    }
    private void explodeByHit()
    {
        if (mCard != null)
        {
            hitTarget(this.transform.position, Quaternion.identity);
            Instantiate(FireAsh, this.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("No Card");
        }
    }
}