using UnityEngine;
using System.Threading.Tasks;

public class MenuTest : MonoBehaviour
{
    [SerializeField] private GameObject StartGame;
    [SerializeField] private GameObject Credit;
    [SerializeField] private GameObject ExitGame;

    private ControllerInput _input;
    private int index = 0;
    private bool Trigger = false;
    private void Start()
    {
        _input = GameManager.singleton._input;
        Selection_StartGame();
    }
    private void Update()
    {
        SelectionSystem();
    }
    private void SelectionSystem()
    {
        if(_input.ArrowKeyRight && !Trigger)
        {
            if(index > 0)
            {
                index--;
                ChangeSelection(index);
                Trigger = true;
            }
        }
        else
        if(_input.ArrowKeyLeft && !Trigger)
        {
            if (index < 2)
            {
                index++;
                ChangeSelection(index);
                Trigger = true;
            }
        }
        else
        if(_input.ButtonX)
        {
            Selection(index);
            Trigger = true;
        }
    }
    private async void RecoverTrigger()
    {
            await Task.Delay(1000);
            Trigger = false;
    }
    private void ChangeSelection(int index)
    {
        switch (index)
        {
            case 0:
                Selection_StartGame();
                break;
            case 1:
                Selection_Credit();
                break;
            case 2:
                Selection_ExitGame();
                break;
        }
        RecoverTrigger();
    }
    private void Selection(int index)
    {
          switch(index)
        {
            case 0:
                CameraToPlayer();
                break;
            case 1:
                
                break;
            case 2:
                
                break;
        }
    }
    private void CameraToPlayer()
    {
        SetCM_StartGame(false);
        SetCM_Credit(false);
        SetCM_ExitGame(false);
    }
    private void Selection_StartGame()
    {
        SetCM_StartGame(true);
        SetCM_Credit(false);
        SetCM_ExitGame(false);
    }
    private void Selection_Credit()
    {
        SetCM_StartGame(false);
        SetCM_Credit(true);
        SetCM_ExitGame(false);
    }
    private void Selection_ExitGame()
    {
        SetCM_StartGame(false);
        SetCM_Credit(false);
        SetCM_ExitGame(true);
    }
    private void SetCM_StartGame(bool value)
    {
        StartGame.SetActive(value);
    }
    private void SetCM_Credit(bool value)
    {
        Credit.SetActive(value);
    }
    private void SetCM_ExitGame(bool value)
    {
        ExitGame.SetActive(value);
    }
}
