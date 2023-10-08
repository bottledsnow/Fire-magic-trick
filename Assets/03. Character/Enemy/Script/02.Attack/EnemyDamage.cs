using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    Rigidbody rb;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            EnergySystem energySystem = collider.gameObject.GetComponent<EnergySystem>();
            energySystem.Energy -= damage;

            // rb = gameObject.GetComponent<Rigidbody>();
            // Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            // flatVel.Normalize();
            // collider.GetComponent<CharacterController>().Move(flatVel * Time.deltaTime);

            Destroy(gameObject);
        }
    }
}
