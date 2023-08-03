using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.UI;

public class ShootingMode_UI : MonoBehaviour
{
    [Header("Mode Icon")]
    [SerializeField] private Image ShootingMode_Normal;
    [SerializeField] private Image ShootingMode_Charge;
    [Header("Choose Feedbacks")]
    [SerializeField] private MMF_Player ChooseNormal;
    [SerializeField] private MMF_Player ChooseCharge;
    private ControllerInput _input;

    private void Start()
    {
        _input = GameManager.singleton._input;
    }
    public void ToChooseNormal()
    {
        ChooseNormal.PlayFeedbacks();
        ChooseCharge.StopFeedbacks();
    }
    public void ToChooseCharge()
    {
        ChooseNormal.StopFeedbacks();
        ChooseCharge.PlayFeedbacks();
    }
}
