using MoreMountains.Feedbacks;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using UnityEngine.VFX;

public class ProjectileMove : MonoBehaviour
{
    public MMFeedbacks HitFeedback;
    public float speed;
    public float fireRate;
    public GameObject muzzlePrefab;
    public GameObject hitPrefab;

    void Start()
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

    void Update()
    {
        GiveSpeed();
    }

    void OnCollisionEnter (Collision co)
    {
        speed = 0;

        ContactPoint contact = co.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        if(hitPrefab != null)
        {
            //oldHit(pos, rot);
            newHit(pos,rot);
        }
        HitFeedback?.PlayFeedbacks();
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
    private void newHit(Vector3 pos, quaternion rot)
    {
        var hitVFX = Instantiate(hitPrefab, pos, rot);
        var psHit = hitVFX.GetComponent<VisualEffect>();
        if (psHit != null)
        {
            Destroy(hitVFX, psHit.playRate);
            // playRate need to change to duration;
        }
        
    }
    private void oldHit(Vector3 pos,quaternion rot)
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
}