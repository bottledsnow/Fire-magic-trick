using MoreMountains.Feedbacks;
using UnityEngine;

public class EnemyAreaHealth : MonoBehaviour
{
    [SerializeField] private MMF_Player Feedbacks;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag =="Bullet")
        {
            Feedbacks.PlayFeedbacks();
        }
    }
}
