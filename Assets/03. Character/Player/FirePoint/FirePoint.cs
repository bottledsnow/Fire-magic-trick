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
    private int ParticleNumber;
    private void Awake()
    {
        PointCollider = GetComponent<Collider>();
    }
    private void FixedUpdate()
    {
        recoverSystem();
    }
    public async void ToUseFirePoint()
    {
        SetCollider(false);
        FirePointCoreParticle.Stop();
        isRecover = true;
        await Task.Delay(recoverTime_ms);
        isRecover = false;
        FirePointCoreParticle.Play();
        SetCollider(true);
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
