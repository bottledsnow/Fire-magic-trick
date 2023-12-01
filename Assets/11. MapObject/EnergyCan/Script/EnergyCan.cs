using MoreMountains.Feedbacks;
using UnityEngine;

public class EnergyCan : MonoBehaviour ,IHealth
{
    [Header("FireEnergyObject")]
	[SerializeField] private GameObject fireEnergy;

    [Header("EnemyHealth")]
    [SerializeField] private int health;

    [Header("Feedbacks")]
    [SerializeField] private MMF_Player Feedbacks_Broken;
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

    private void Broke()
    {
        Feedbacks_Broken.PlayFeedbacks();
        Object.Instantiate(fireEnergy, transform.position , Quaternion.identity);
        Destroy(gameObject,0.25f);
    }
}
