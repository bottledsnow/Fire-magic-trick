using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TeleportExplode : MonoBehaviour
{
    private Fire_Teleport teleportSystem;

    private void Start()
    {
        teleportSystem = GameManager.singleton._input.GetComponent<Fire_Teleport>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Trigger Obj:" + other.attachedRigidbody.gameObject.name);
            hitEnemy(other);
        }
    }

    private void hitEnemy(Collider other)
    {
        IHealth health = other.attachedRigidbody.GetComponent<IHealth>();
        health.TakeDamage(teleportSystem.TeleportDamage);
    }
}