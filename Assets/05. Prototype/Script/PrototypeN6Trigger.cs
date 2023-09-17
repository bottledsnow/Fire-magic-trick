using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeN6Trigger : MonoBehaviour
{
    [SerializeField] private MMF_Player Feedbacks;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Feedbacks.PlayFeedbacks();
        }
    }
}
