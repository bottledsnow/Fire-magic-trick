using MoreMountains.Feedbacks;
using UnityEngine;

public class PrototypeN8Trigger : MonoBehaviour
{
    private bool Trigger;
    [SerializeField] private MMF_Player Feedback;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(!Trigger)
            {
                Trigger = true;
                Feedback.PlayFeedbacks();
            }
        }
    }
}
