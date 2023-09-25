using UnityEngine;
using System.Threading.Tasks;
using MoreMountains.Feedbacks;

public class StarCore : MonoBehaviour
{
    [SerializeField] private MMF_Player CircleFeedbacks;
    [SerializeField] private int msDelay = 1000;

    private void Start()
    {
        RotationCircle();
    }
    private async void RotationCircle()
    {
        for(int i = 0;i<10;i++)
        {
            await Task.Delay(msDelay);
            CircleFeedbacks.PlayFeedbacks();
            Debug.Log("rotation");
        }
    }
}
