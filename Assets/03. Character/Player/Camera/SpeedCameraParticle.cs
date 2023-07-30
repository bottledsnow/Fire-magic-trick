using MoreMountains.Feedbacks;
using UnityEngine;
using System.Threading.Tasks;

public class SpeedCameraParticle : MonoBehaviour
{
    [Header("Feedbacks")]
    [SerializeField] private MMF_Player Feedbacks_Start;
    [SerializeField] private MMF_Player Feedbacks_End;

    public void OpenParticle()
    {
        Feedbacks_Start.PlayFeedbacks();
    }
    public async void OpenParticle(int Delay)
    {
        await Task.Delay(Delay);
        Feedbacks_Start.PlayFeedbacks();
    }
    public void CloseParticle() 
    {
        Feedbacks_End.PlayFeedbacks();
    }
    public async void CloseParticle(int Delay)
    {
        await Task.Delay(Delay);
        Feedbacks_End.PlayFeedbacks();
    }
}
