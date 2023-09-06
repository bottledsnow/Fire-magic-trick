using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Card_Move : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float fireRate;
    [SerializeField] private GameObject muzzlePrefab;
    [SerializeField] private GameObject hitPrefab;

    private CrosshairUI _crosshairUI;
    void Start()
    {
        _crosshairUI = GameManager.singleton.UISystem.GetComponent<CrosshairUI>();
        DestroyBullet(lifeTime);

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

    void OnCollisionEnter(Collision co)
    {
        speed = 0;

        ContactPoint contact = co.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        if (hitPrefab != null)
        {
            newHit(pos, rot);
        }

        if(co.transform.tag == "Enemy")
        {
            _crosshairUI.CrosshairHit();
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
    private void newHit(Vector3 pos, Quaternion rot)
    {
        var hitVFX = Instantiate(hitPrefab, pos, rot);
        var psHit = hitVFX.GetComponent<VisualEffect>();
        if (psHit != null)
        {
            Destroy(hitVFX, psHit.playRate);
            // playRate need to change to duration;
        }

    }
    private void DestroyBullet(float lifetime)
    {
        Destroy(gameObject, lifetime);
    }
}