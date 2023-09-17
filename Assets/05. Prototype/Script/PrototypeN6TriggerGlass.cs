using MoreMountains.Feedbacks;
using UnityEngine;

public class PrototypeN6TriggerGlass : MonoBehaviour
{
    [SerializeField] private MMF_Player Feedbacks;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="ChargeBullet")
        {
            Feedbacks.PlayFeedbacks();
            this.gameObject.SetActive(false);
        }
    }
}
