using UnityEngine;

public class AshExplode : MonoBehaviour
{
    private int ExplodeDamage;
    private ShootingSystem shootingSystem;
    private Shooting_Charge_old ChargeSystem;
    private bool ToDamage = false;

    private void Start()
    {
        shootingSystem = GameManager.singleton._input.GetComponent<ShootingSystem>();
        ChargeSystem = shootingSystem._ShootingCharge;
        ExplodeDamage = ChargeSystem.explodeDamage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if(!ToDamage)
            {
                ToDamage = true;
                Debug.Log("Trigger Obj:" + other.attachedRigidbody.gameObject.name);
                hitEnemy(other);
            }
        }
    }

    private void hitEnemy(Collider other)
    {
        IHealth health = other.attachedRigidbody.GetComponent<IHealth>();
        health.TakeDamage(ExplodeDamage);
    }
}
