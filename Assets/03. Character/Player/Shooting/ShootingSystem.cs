using UnityEngine;
using StarterAssets;
using Cinemachine;
using MoreMountains.Feedbacks;

public class ShootingSystem : MonoBehaviour
{
    [Header("Shooting System Component")]
    public Shooting _Shooting;
    public Shooting_Check _ShootingCheck;
    public Shooting_Aim _ShootingAim;
    public Shooting_Normal _ShootingNormal;
    public Shooting_Charge _ShootingCharge;
}
