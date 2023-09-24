using StarterAssets;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float maxPreInputTime = 0.1f;

    private float preInputTimer = 0f;
    private bool isPreInput = false;
    private bool trigger = false;

    private ThirdPersonController _thirdPersonController;
    private ControllerInput _input;

    private void Start()
    {
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _input = GetComponent<ControllerInput>();
    }

    private void Update()
    {
        preInputSystem();
        jumpSystem();
    }
    #region PreInput
    #region System
    private void preInputSystem()
    {
        preInputButton();
        preInput();
        preInputEventCheck();
    }
    private void preInputButton()
    {
        if (_input.ButtonA && !trigger)
        {
            isPreInput = true;
            trigger = true;
        }
        if(!_input.ButtonA)
        {
            trigger = false;
        }
    }
    private void preInput()
    {
        if(isPreInput)
        {
            preInputTimer += Time.deltaTime;
        }

        if(preInputTimer>maxPreInputTime)
        {
            Initialization();
        }
    }
    private void Initialization()
    {
        preInputTimer = 0f;
        isPreInput = false;
    }
    #endregion
    private void preInputEventCheck()
    {
        if (isPreInput)
        {
            if(_thirdPersonController.Grounded)
            {
                jump();
                Initialization();
            }
        }
    }
    #endregion
    private void jumpSystem()
    {
        if(_thirdPersonController.useGravity)
        {
            if(_thirdPersonController.Grounded)
            { 
                if(_input.ButtonA)
                {
                    jump();
                }
            }
        }
    }
    private void jump()
    {
        _thirdPersonController.Jump();
    }
}
