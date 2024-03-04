using UnityEngine;

public class Satin_Laser_Collider : MonoBehaviour
{
    [SerializeField] private float force;
    private HealthSystem healthSystem;

    private void Start()
    {
        healthSystem = GameManager.singleton.Player.GetComponent<HealthSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ImpactReceiver impact = other.GetComponent<ImpactReceiver>();
            Vector3 force = this.transform.up * this.force;
            impact.AddImpact(force);
            healthSystem.ToDamagePlayer(0);
        }
    }
}
