using MoreMountains.Feedbacks;
using UnityEngine;

public class PrototypeN4Trigger : MonoBehaviour
{
    [SerializeField] private BrokeGlassN4 Land;
    [SerializeField] private MMF_Player Feedbacks;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Land.Broke();
            Feedbacks.PlayFeedbacks();
            this.gameObject.SetActive(false);
        }
    }
}
