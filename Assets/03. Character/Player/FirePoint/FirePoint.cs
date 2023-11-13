using UnityEngine;
using System.Threading.Tasks;

public class FirePoint : MonoBehaviour
{
    private Collider PointCollider;

    [SerializeField] private GameObject CheckArea;
    [SerializeField] private ParticleSystem FirePointParticle;
    [SerializeField] private ParticleSystem FirePointCoreParticle;
    [Header("Setting")]
    [SerializeField] private int recoverTime_ms = 5000;

    private bool isRecover;
    private bool Trigger;
    private int ParticleNumber;
    private void Awake()
    {
        PointCollider = GetComponent<Collider>();
    }

    private void Update()
    {
    }
    private void FixedUpdate()
    {
        recoverSystem();
    }
    public async void ToUseFirePoint()
    {
        FirePointCoreParticle.Stop();
        isRecover = true;
        SetCollider(false);
        await Task.Delay(recoverTime_ms);
        SetCollider(true);
        isRecover = false;
        FirePointCoreParticle.Play();
    }
    public void SetCollider(bool active)
    {
        PointCollider.enabled = active;
        CheckArea.SetActive(active);
    }
    private void recoverSystem()
    {
        if(isRecover)
        {
            SetParticleNumber(ParticleNumber);
            ParticleNumber++;
        }
        else
        {
            Trigger = true;
            ParticleNumber = 0;
            SetParticleNumber(0);
        }
        
    }
    private void SetParticleNumber(int particleNumber)
    {
        var emission = FirePointParticle.emission;
        emission.rateOverTime = particleNumber;
    }
}
