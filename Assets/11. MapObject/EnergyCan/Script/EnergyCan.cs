using UnityEngine;

public class EnergyCan : MonoBehaviour ,IHealth
{
    [Header("FireEnergyObject")]
	[SerializeField] private GameObject fireEnergy;

    [Header("EnemyHealth")]
    [SerializeField] private int health;
    public int iHealth
    {
        get { return health; }
        set { health = value; }
    }

    public void TakeDamage(int damage , PlayerDamage.DamageType damageType)
    {
        health -= damage;

        if(health <= 0)
        {
            Broke();
        }
    }

    void Broke()
    {
        Object.Instantiate(fireEnergy, transform.position , Quaternion.identity);
        Destroy(gameObject);
    }
}
