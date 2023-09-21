using UnityEngine;

public class TriggerFly : MonoBehaviour
{
    [SerializeField] private EnemyAreaHealth _health;
    [SerializeField] private DashHitFlay DashHitFlay;
    private void OnTriggerEnter(Collider other)
    {
            _health.Playfeedback();
            DashHitFlay.isFlay = true;
    }
}
