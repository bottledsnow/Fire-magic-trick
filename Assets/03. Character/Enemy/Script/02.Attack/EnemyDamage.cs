using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private int damage;

    [Header("KickBack")]
    [SerializeField] private float force;
    [SerializeField] private Transform BackCoordinate;

    Rigidbody rb;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            DamagePlayer(collider.gameObject);
            KickBackPlayer(collider.gameObject);
        }
    }

    private void DamagePlayer(GameObject player)
    {
        EnergySystem energySystem = player.GetComponent<EnergySystem>();
        energySystem.Energy -= damage;
    }

    private void KickBackPlayer(GameObject player)
    {
        Vector3 Direction = (player.transform.position - BackCoordinate.position).normalized;
        Vector3 ForceDirection = new Vector3(Direction.x, 0, Direction.z);

        ImpactReceiver impactReceiver = player.GetComponent<ImpactReceiver>();
        impactReceiver.AddImpact(ForceDirection * force);
    }

    public void PlayerGetDamage()
    {

    }


}
