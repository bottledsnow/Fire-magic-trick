using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;

public class PrototypeN4 : MonoBehaviour
{
    [SerializeField] private GameObject Trigger;
    private ControllerInput _input;
    private bool isTrigger;
    [SerializeField] private MMF_Player Feedbacks;

    private void Start()
    {
        _input = GameManager.singleton._input;
    }

    private void Update()
    {
        CheckSuperDashButton();
    }
    private void CheckSuperDashButton()
    {
        if(_input.LB && !isTrigger)
        {
            N4();
        }
    }
    private void N4()
    {
        Feedbacks.StopFeedbacks();
    }
}
