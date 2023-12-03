using UnityEngine;

public class EnemyHaveEnergyCan : MonoBehaviour
{
    [Header("Can")]
    [SerializeField] private EnergyCan energyCanSystem;

    private EnemyHealthSystem healthSystem;
    private void Start()
    {
        healthSystem = GetComponent<EnemyHealthSystem>();
        healthSystem.OnEnemyDeath += EnergyCanBroke;
    }

    private void EnergyCanBroke()
    {
        energyCanSystem.Broke();
    }


}
