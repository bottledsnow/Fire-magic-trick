using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;

public class FireFloat : MonoBehaviour
{
    private ControllerInput _input;
    private PlayerState _playerState;

    [SerializeField] private float floatForce;

    private void Start()
    {
        _playerState = GameManager.singleton._playerState;
        _input = GameManager.singleton._input;
    }
    private void Update()
    {
        FloatSystem();
    }
    private void FloatSystem()
    {
        if(_input.ButtonA && _playerState.canFloat && !_playerState.nearGround)
        {
            Debug.Log("floating");
            _playerState.SetGravityToFloat();
            _playerState.SetIsFloat(true);
        }else
        {
            _playerState.SetGravityToNormal();
            _playerState.SetIsFloat(false);
        }
    }
}
