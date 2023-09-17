using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;

public class ShootingModeUI : MonoBehaviour
{
    [SerializeField] private MMF_Player Feedbacks;
    [SerializeField] private MMF_Player Feedbacks_Normal;
    [SerializeField] private MMF_Player Feedbacks_Charge;
    public void ChooseNormal()
    {
        Debug.Log("Normal UI");
        Feedbacks.PlayFeedbacks();
        Feedbacks_Normal.PlayFeedbacks();
    }
    public void ChooseCharge()
    {
        Debug.Log("Charge UI");
        Feedbacks.PlayFeedbacks();
        Feedbacks_Charge.PlayFeedbacks();
    }
}
