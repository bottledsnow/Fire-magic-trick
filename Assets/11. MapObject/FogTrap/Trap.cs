using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private float force;
    //Script
    private ImpactReceiver impactReceiver;

    private void Start()
    {
        impactReceiver = GameManager.singleton.Player.GetComponent<ImpactReceiver>();
    }
    public void Play()
    {
        Vector3 force = -transform.forward * this.force;
        impactReceiver.AddImpact(force);
    }
}
