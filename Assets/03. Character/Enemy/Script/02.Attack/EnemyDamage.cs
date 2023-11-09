using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float force = 20;
    public Transform forcePoint;
    Rigidbody rb;
    Vector3 forceDirection;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            EnergySystem energySystem = collider.gameObject.GetComponent<EnergySystem>();
            energySystem.Energy -= damage;

            if(forcePoint != null)
            {
                forceDirection = (forcePoint.position - collider.gameObject.transform.position);
                forceDirection = new Vector3(forceDirection.x,0,forceDirection.z);
            }
            

            ImpactReceiver impactReceiver = collider.gameObject.GetComponent<ImpactReceiver>();
            impactReceiver.AddImpact(forceDirection , force);
        }
    }
}
