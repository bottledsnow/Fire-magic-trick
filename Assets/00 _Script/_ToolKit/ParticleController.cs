using UnityEngine;

public class ParticleController : MonoBehaviour
{
    void Awake()
    {
        var main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    private void OnParticleSystemStopped()
    {
        FinishAnim();
    }
    private void FinishAnim()
    {
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}