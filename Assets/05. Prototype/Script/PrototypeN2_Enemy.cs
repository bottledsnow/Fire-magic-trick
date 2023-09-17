using MoreMountains.Feedbacks;
using UnityEngine;

public class PrototypeN2_Enemy : MonoBehaviour
{
    [SerializeField] private PrototypeN2 N3;
    [SerializeField] private MMF_Player HitFeedbacks;
    [SerializeField] private MMF_Player KillFeedbacks;

    private int hitNumber = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            hitNumber++;

            if (hitNumber == 1)
            {
                Boom();
            }
            if (hitNumber == 2)
            {
                Kill();
            }
        }
    }
    
    private void Boom()
    {
        HitFeedbacks.PlayFeedbacks();
        Debug.Log("Boom");
    }
    private void Kill()
    {
        Debug.Log("Kill");
        KillFeedbacks.PlayFeedbacks();
        Destroy(gameObject);
    }
}
