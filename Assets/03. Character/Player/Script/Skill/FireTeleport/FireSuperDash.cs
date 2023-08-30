using UnityEngine;
using System.Collections;

public class FireSuperDash : MonoBehaviour
{
    [SerializeField] private GameObject Target;
    [SerializeField] private AnimationCurve superDashIncreaseSpeed;
    [SerializeField] private float superDashMaxSpeed;
    [SerializeField] private float SuperDashTime;
    private CharacterController _characterController;
    private ControllerInput _input;
    private PlayerState _playerState;
    private GameObject player;

    private Vector3 startPosition;
    private Vector3 targertPosition;
    private float superDashSpeed = 0;
    private float superDashTimer = 0;
    private bool isSuperDash;

    private void Start()
    {
        _input = GameManager.singleton._input;
        _playerState = GameManager.singleton._playerState;
        _characterController = _playerState.GetComponent<CharacterController>();
        player = _playerState.gameObject;
    }
    private void Update()
    {
        Test();
        SuperDashSystem();
    }
    private void Test()
    {
        if (_input.LB)
        {
            Debug.Log("Q");
            isSuperDash = !isSuperDash;
        }
    }
    private void SuperDashSystem()
    {
        if (isSuperDash)
        {
            _playerState.OutControl();
            speedIncrease();
            move();
        } else
        {
            _playerState.TakeControl();
            superDashSpeed = 0;
            superDashTimer = 0;
        }
    }
    private void move()
    {
        Vector3 Direction = CalculateDirection(player.transform.position, Target.transform.position).normalized;
        _characterController.Move(Direction * superDashSpeed * Time.deltaTime);
    }
    private Vector3 CalculateDirection(Vector3 start, Vector3 end)
    {
        return end - start;
    }
    private void speedIncrease()
    {
       superDashTimer = speedTimer(superDashTimer);
        Debug.Log(superDashTimer);
        superDashSpeed = superDashIncreaseSpeed.Evaluate(superDashTimer) * superDashMaxSpeed;
    }
    private float speedTimer(float timer)
    {
        if (timer <= 1f)
        {
            timer += Time.deltaTime/SuperDashTime;
        }else
        {
            timer = 1;
        }
        return timer;
    }
}
