using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float force = 20;
    [SerializeField] private bool destroyAfterHit = false;
    Rigidbody rb;
    public Vector3 forceDirection;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            EnergySystem energySystem = collider.gameObject.GetComponent<EnergySystem>();
            energySystem.Energy -= damage;

            forceDirection = (transform.position - collider.gameObject.transform.position);
            forceDirection = new Vector3(forceDirection.x,forceDirection.y*0,forceDirection.z);

            ImpactReceiver impactReceiver = collider.gameObject.GetComponent<ImpactReceiver>();
            impactReceiver.AddImpact(forceDirection , force);

            if(destroyAfterHit)
            {
                Destroy(gameObject);
            }
           
        }
    }
}
