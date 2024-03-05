using MoreMountains.Feedbacks;
using UnityEngine;

public class SFX_Manager : MonoBehaviour
{
    [SerializeField] private MMF_Player feedbacks_FootStep;
    [SerializeField] private MMF_Player feedbacks_JumpLand;

    public void SFX_FootStep()
    {
        feedbacks_FootStep.PlayFeedbacks();
    }
    public void SFX_JumpLand()
    {
        feedbacks_JumpLand.PlayFeedbacks();
    }
}
