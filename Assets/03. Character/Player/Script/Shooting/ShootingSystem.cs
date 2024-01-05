using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    private ControllerInput _input;
    //private bool isChooseNormal = true;

    [Header("Shooting System Component")]
    public Shooting _Shooting;
    public Shooting_Check _ShootingCheck;
    public Shooting_Aim _ShootingAim;
    public Shooting_Normal _ShootingNormal;
    private void Awake()
    {
        _ShootingCheck = _Shooting.GetComponent<Shooting_Check>();
        _ShootingAim = _Shooting.GetComponent<Shooting_Aim>();
        _ShootingNormal = _Shooting.GetComponent<Shooting_Normal>();
    }
}
