using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    private ControllerInput _input;

    public bool isPause = false;
    [Header("UI")]
    [SerializeField] private GameObject pauseUI;

    private bool triggerButton = false;

    private void Start()
    {
        _input = GameManager.singleton._input;
    }

    private void Update()
    {
        pauseSystem();
        Debug.Log(Time.timeScale);
    }
    private void pauseSystem()
    {
        StartPauseCheck();
        Initialization();
    }
    private void StartPauseCheck()
    {
        if (_input.Option)
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
        //Pause Game.
        Time.timeScale = 0;
        pauseUI.SetActive(true);
    }
    private void StopPause()
    {
        //Stop Pause Game.
        Time.timeScale = 1;
        pauseUI.SetActive(false);
    }
    private void Initialization()
    {
        if (!_input.Option && triggerButton) 
        {
            triggerButton = false;
        }
    }
}
