using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFXController : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject floatVFX;
    [SerializeField] private GameObject windFeetCardVFX;
    [SerializeField] private GameObject superDashVFX;
    [SerializeField] private GameObject canComboVFX;
    [SerializeField] private GameObject dashVFX;

    [SerializeField] private ParticleSystem burningVFX;
    [SerializeField] private ParticleSystem fireCountVFX;
    [SerializeField] private ParticleSystem windCountVFX;
    [SerializeField] private ParticleSystem fireMaxVFX;
    [SerializeField] private ParticleSystem windMaxVFX;
    [SerializeField] private ParticleSystem windMaxStar;
    [SerializeField] private ParticleSystem fireMaxStar;
    [SerializeField] private ParticleSystem fireExplode;
    [SerializeField] private ParticleSystem superDashHit;
    [SerializeField] private ParticleSystem windSuperJump;
    [SerializeField] private ParticleSystem fireSuperJump;

    [Header("Prefabs")]
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private GameObject respawnVFXPRefab;

    [Header("Super Jump")]
    [SerializeField] private Transform feetPosition;
    [SerializeField] private GameObject superJumpLandWindVFXPrefab;
    [SerializeField] private GameObject superJumpLandFireVFXPrefab;

    private Core core;
    private ParticleManager particleManager;
    private void Awake()
    {
        core = GetComponentInChildren<Core>();
        particleManager = core.GetCoreComponent<ParticleManager>();

        Init();
    }

    public void Init()
    {
        superDashVFX.SetActive(false);
        floatVFX.SetActive(false);
        windFeetCardVFX.SetActive(false);
        canComboVFX.SetActive(false);
        dashVFX.SetActive(false);

        burningVFX.gameObject.SetActive(true);
        windMaxStar.gameObject.SetActive(true);
        fireMaxStar.gameObject.SetActive(true);
        fireCountVFX.gameObject.SetActive(true);
        windCountVFX.gameObject.SetActive(true);
        fireMaxVFX.gameObject.SetActive(true);
        windMaxVFX.gameObject.SetActive(true);
        fireExplode.gameObject.SetActive(true);
        superDashHit.gameObject.SetActive(true);
        windSuperJump.gameObject.SetActive(true);
        fireSuperJump.gameObject.SetActive(true);

        burningVFX.Stop();
        superDashHit.Stop();
        fireExplode.Stop();
        fireCountVFX.Stop();
        windCountVFX.Stop();
        fireMaxVFX.Stop();
        windMaxVFX.Stop();
        windMaxStar.Stop();
        fireMaxStar.Stop();
        windSuperJump.Stop();
        fireSuperJump.Stop();
    }

    public void SetModelVFX(bool value)
    {
        fireCountVFX.gameObject.SetActive(value);
        windCountVFX.gameObject.SetActive(value);
        windMaxStar.gameObject.SetActive(value);
        fireMaxStar.gameObject.SetActive(value);
    }

    public void PlayWindSuperJump()
    {
        windSuperJump.Stop();
        windSuperJump.Play();
    }

    public void PlayFireSuperJump()
    {
        fireSuperJump.Stop();
        fireSuperJump.Play();
    }

    public void PlayFireExplode()
    {
        fireExplode.Stop();
        fireExplode.Play();
    }


    public void ActivateWindLandVFX()
    {
        particleManager.StartParticles(superJumpLandWindVFXPrefab, feetPosition.position);
    }

    public void ActivateFireLandVFX()
    {
        particleManager.StartParticles(superJumpLandFireVFXPrefab, feetPosition.position);
    }

    public void ActivateDeathVFX()
    {
        particleManager.StartParticles(deathVFXPrefab, transform.position);
    }

    public void ActivateRespawnVFX()
    {
        particleManager.StartParticles(respawnVFXPRefab, transform.position, transform);
    }

    public void SetCanComboVFX(bool value)
    {
        canComboVFX.SetActive(value);
    }

    public void SetFloatVFX(bool value)
    {
        floatVFX.SetActive(value);
    }

    public void SetWindFeetCardVFX(bool value)
    {
        windFeetCardVFX.SetActive(value);
    }

    public void SetWindCountVFX(int value)
    {
        var emission = windCountVFX.emission;
        emission.rateOverTimeMultiplier = value * 5;
        if (windCountVFX.isStopped)
            windCountVFX.Play();
    }

    public void SetFireCountVFX(int value)
    {
        var emission = fireCountVFX.emission;
        emission.rateOverTimeMultiplier = value * 5;
        if (fireCountVFX.isStopped)
            fireCountVFX.Play();
    }

    public void PlayWindMax()
    {
        windMaxVFX.Play();
    }

    public void PlayFireMax()
    {
        fireMaxVFX.Play();
    }

    public void SetWindMaxStar(bool value)
    {
        if (value)
        {
            windMaxStar.Play();
        }
        else
        {
            windMaxStar.Stop();
        }
    }

    public void SetFireMaxStar(bool value)
    {
        if (value)
        {
            fireMaxStar.Play();
        }
        else
        {
            fireMaxStar.Stop();
        }
    }

    public void SetSuperDashVFX(bool value)
    {
        superDashVFX.SetActive(value);
    }

    public void SuperDashHit()
    {
        superDashHit.Stop();
        superDashHit.Play();
    }

    public void SetDashVFX(bool value)
    {
        dashVFX.SetActive(value);
    }

    public void SetBurningVFX(float value)
    {
        var comp = burningVFX.main;
        comp.startSize = value * 2f;

        if(value > 0)
        {
            burningVFX.Play();
        }
        else
        {
            burningVFX.Stop();
        }
    }
}
