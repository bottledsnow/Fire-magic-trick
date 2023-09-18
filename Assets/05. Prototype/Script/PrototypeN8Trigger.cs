using MoreMountains.Feedbacks;
using UnityEngine;

public class PrototypeN8Trigger : MonoBehaviour
{
    [SerializeField] private MMF_Player Feedback;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Feedback.PlayFeedbacks();
        }
    }
}
