using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private int damage;

    [Header("KickBack")]
    [SerializeField] private float force;
    [SerializeField] private Transform knockBackCoordinate;
    [SerializeField] private bool isVertical;

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
        if (knockBackCoordinate != null)
        {
            Vector3 Direction = (player.transform.position - knockBackCoordinate.position).normalized;
            Vector3 ForceDirection = Direction;

            if (!isVertical)
            {
                ForceDirection = new Vector3(Direction.x, 0, Direction.z);
            }
            else
            {
                ForceDirection = new Vector3(0, Direction.y, 0);
            }

            ImpactReceiver impactReceiver = player.GetComponent<ImpactReceiver>();
            impactReceiver.AddImpact(ForceDirection * force);
        }
    }

    public void PlayerGetDamage()
    {

    }

    public void DestroyObject()
    {
        Destroy(transform.parent.gameObject);
    }
}
