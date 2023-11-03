using MoreMountains.Feedbacks;
using UnityEngine;

public class BulletTimeSystem : MonoBehaviour
{
    [Header("Bullet Time Scale")]
    [SerializeField] private MMF_Player Feedbacks_BulletTime_slow;
    [SerializeField] private MMF_Player Feedbacks_BulletTime_mid;
    [SerializeField] private MMF_Player Feedbacks_BulletTime_nearNormal;
    [SerializeField] private MMF_Player Feedbacks_BulletTime_Normal;

    public void BulletTimeSlow()
    {
        Feedbacks_BulletTime_slow.PlayFeedbacks();
    }
    public void BulletTimeMid()
    {
        Feedbacks_BulletTime_mid.PlayFeedbacks();
    }
    public void BulletTimeNearNormal()
    {
        Feedbacks_BulletTime_nearNormal.PlayFeedbacks();
    }
    public void BulletTimeNormal()
    {
          Feedbacks_BulletTime_Normal.PlayFeedbacks();
    }
}
