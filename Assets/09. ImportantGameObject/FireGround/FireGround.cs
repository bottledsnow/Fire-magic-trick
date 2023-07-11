using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class FireGround : MonoBehaviour
{
    [SerializeField] ParticleSystem FireGroundParticle;
    [SerializeField] private float ALLFirePower;
    [SerializeField] private float RecoverCD;
    [SerializeField] private float RecoverEnergy;
    private EmissionModule emissionModule;
    private EnergySystem energySystem;
    private FireAbsorb fireGroundAbsorb;
    private bool isRecover;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            energySystem = other.GetComponent<EnergySystem>();
            isRecover = true;
            EnergyRecover();
            Debug.Log("Player Enter");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(isRecover)
            {
                isRecover = false;
            }
        }
    }

    private void Start()
    {
        fireGroundAbsorb = GetComponent<FireAbsorb>();
        emissionModule = FireGroundParticle.emission;
    }
    private void Update()
    {
        
    }
    private async void EnergyRecover()
    {
        while (isRecover && ALLFirePower > 0)
        {
            await Task.Delay((int)(RecoverCD*1000));
            if(!energySystem.FireEnergyFull)
            {
                fireGroundAbsorb.enabled = true;
                energySystem.ReplenishFireEnergy(RecoverEnergy);
                ALLFirePower -= RecoverEnergy;
                UpdateEmissionRate();
            }
        }
        fireGroundAbsorb.enabled = false;
    }
    private void UpdateEmissionRate()
    {
        float emissionRate = ALLFirePower; 
        emissionModule.rateOverTime = new MinMaxCurve(emissionRate);
    }
}
