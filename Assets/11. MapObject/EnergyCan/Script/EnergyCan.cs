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

    [Header("CheckArea")]
    [SerializeField] private GameObject brokenBoomArea;

    private bool isBroken = false;
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

    public void Broke()
    {
        if(!isBroken)
        {
            isBroken = true;
            Feedbacks_Broken.PlayFeedbacks();
            Object.Instantiate(fireEnergy, transform.position, Quaternion.identity);
            brokenBoomArea.SetActive(true);
            Destroy(gameObject, 0.25f);
        }
    }
}
