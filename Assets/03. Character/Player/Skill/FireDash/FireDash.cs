using UnityEngine;
using System.Threading.Tasks;
using StarterAssets;
using System.Collections;

public class FireDash : MonoBehaviour
{
    private ThirdPersonController _playerController;
    private ControllerInput _input;
    private CharacterController _characterController;
    private bool dashedButton = false;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    private void Start()
    {
        _playerController = GetComponent<ThirdPersonController>();
        _input = GetComponent<ControllerInput>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        DashSystem();
    }
    #region Dash Systsem
    private void DashSystem()
    {
        triggerDash();
        triggerLimit();
    }
    private void triggerDash()
    {
        if (_input.ButtonB && !dashedButton)
        {
            dashedButton = true;
            //When press Button,triiger once
            StartCoroutine(Dash());
        }
    }
    private void triggerLimit()
    {
        if(!_input.ButtonB)
        {
            
            dashedButton = false;
        }
    }
    #endregion 
    IEnumerator Dash()
    {
        float StartTime = Time.time;

        while(Time.time<StartTime + dashTime)
        {
            Vector3 Dir = Quaternion.Euler(0, _playerController.PlayerRotation, 0) * Vector3.forward;
            _characterController.Move(Dir * dashSpeed * Time.deltaTime);
            yield return null;

        }
    }
}
