using MoreMountains.Feedbacks;
using StarterAssets;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    //Script
    private ThirdPersonController thirdPersonController;
    private ControllerInput _input;
    private MenuSystem menuSystem;

    public bool isPause = false;
    [Header("UI")]
    [SerializeField] private GameObject pauseUI;
    [Header("Feedback")]
    [SerializeField] private MMF_Player feedback_pause;
    [SerializeField] private MMF_Player feedback_keepGame;

    private bool triggerButton = false;

    private void Start()
    {
        _input = GameManager.singleton._input;
        menuSystem = GameManager.singleton.GetComponent<MenuSystem>();
        thirdPersonController = GameManager.singleton.Player.GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        pauseSystem();
    }
    private void pauseSystem()
    {
        StartPauseCheck();
        Initialization();
    }
    private void StartPauseCheck()
    {
        if (_input.Window)
        {
            //Trigger Pause Button.
            if(!triggerButton) 
            {
                if(isPause)
                {
                    isPause = false;
                    StopPause();
                    pauseUI.SetActive(false);
                }
                else
                {
                    isPause = true;
                    Pause();
                    pauseUI.SetActive(true);
                }
                triggerButton = true;
            }
        }
    }
    public void Pause()
    {
        if(menuSystem.isStartGame)
        {
            if(menuSystem.enabled == true)
            {
                menuSystem.SetPlayMode(false);
            }
            feedback_pause.PlayFeedbacks();
            thirdPersonController.enabled = false;
        }
    }
    public void StopPause()
    {
        if(menuSystem.isStartGame)
        {
            if(menuSystem.enabled == true)
            {
                menuSystem.SetPlayMode(true);
            }
            feedback_keepGame.PlayFeedbacks();
            thirdPersonController.enabled = true;
        }
    }
    private void Initialization()
    {
        if (!_input.Window && triggerButton) 
        {
            triggerButton = false;
        }
    }
}
