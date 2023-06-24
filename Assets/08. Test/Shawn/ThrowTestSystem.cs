using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Threading.Tasks;

public class ThrowTestSystem : MonoBehaviour
{
    private bool ThrowCD;
    private bool isThrow = false;
    private PokerBullet pokerBullet;
    private GameObject Clone;
    [SerializeField] private GameObject AimCamera;
    [SerializeField] private ControllerInput _Input;
    [SerializeField] private Transform ThrowPoint;
    [SerializeField] private Transform Poker;
    private void Update()
    {
        Throw();
        Aim();
    }
    private void Aim()
    {
        if(isThrow)
        {
            CloseAim(true);
        }
        else
        {
            ToAim();
            CloseAim();
        }
    }

    private void ToAim()
    {
        if(_Input.LT)
        {
            AimCamera.SetActive(true);
        }
    }

    private void CloseAim()
    {
        if (!_Input.LT)
        {
            AimCamera.SetActive(false);
        }
    }
    private void CloseAim(bool force)
    {
        if(force)
            AimCamera.SetActive(false);
    }
    private void Throw()
    {
        if (_Input.LB)
        {
            if (!isThrow)
            {
                SpawnPoker();
                pokerBullet = Clone.GetComponent<PokerBullet>();
                pokerBullet.initialization_Hori(_Input.RightStick.x);
                pokerBullet.initialization_Vert(-_Input.RightStick.y);
                isThrow = true;
                ThrowCDSystem();
            }
        }

        if (!_Input.LB && !ThrowCD)
        {
            isThrow = false;
        }
    }
    private void SpawnPoker()
    {
        Clone = Instantiate(Poker, ThrowPoint.position,Quaternion.identity).gameObject;
    }

    private async void ThrowCDSystem()
    {
        ThrowCD = true;
        await Task.Delay(1500);
        ThrowCD = false;
    }
}
