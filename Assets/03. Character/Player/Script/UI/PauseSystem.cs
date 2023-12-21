using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.UI;

public class PauseSystem : MonoBehaviour
{
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
                }
                else
                {
                    isPause = true;
                    Pause();
                }
                triggerButton = true;
            }
        }
    }
    private void Pause()
    {
        if(menuSystem.isStartGame)
        {
            menuSystem.SetPlayMode(false);
            feedback_pause.PlayFeedbacks();
            pauseUI.SetActive(true);
        }
    }
    private void StopPause()
    {
        if(menuSystem.isStartGame)
        {
            menuSystem.SetPlayMode(true);
            feedback_keepGame.PlayFeedbacks();
            pauseUI.SetActive(false);
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
