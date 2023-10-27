using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeN7Trigger : MonoBehaviour
{
    [SerializeField] private MMF_Player Feedbacks;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            Feedbacks.PlayFeedbacks();
            this.gameObject.SetActive(false);
        }   
    }
}
