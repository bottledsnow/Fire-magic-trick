using MoreMountains.Feedbacks;
using UnityEngine;
using System.Threading.Tasks;

public class BulletTime : MonoBehaviour
{
    [Header("Bullet Time Scale")]
    [SerializeField] private MMF_Player Feedbacks_BulletTime_slow;
    [SerializeField] private MMF_Player Feedbacks_BulletTime_mid;
    [SerializeField] private MMF_Player Feedbacks_BulletTime_nearNormal;
    [SerializeField] private MMF_Player Feedbacks_BulletTime_Normal;

    public void BulletTime_Slow()
    {
        Feedbacks_BulletTime_slow.PlayFeedbacks();
    }
    public void BulletTime_Mid()
    {
        Feedbacks_BulletTime_mid.PlayFeedbacks();
    }
    public void BulletTime_NearNormal()
    {
        Feedbacks_BulletTime_nearNormal.PlayFeedbacks();
    }
    public void BulletTime_Normal()
    {
          Feedbacks_BulletTime_Normal.PlayFeedbacks();
    }
    public async void BulletTime_Slow(float time)
    {
        BulletTime_Slow();
        await Task.Delay((int)(time * 1000));
        BulletTime_Normal();
    }
    public async void BulletTime_Mid(float time)
    {
        BulletTime_Mid();
        await Task.Delay((int)(time * 1000));
        BulletTime_Normal();
    }
    public async void BulletTime_NearNormal(float time)
    {
        BulletTime_NearNormal();
        await Task.Delay((int)(time * 1000));
        BulletTime_Normal();
    }
}
