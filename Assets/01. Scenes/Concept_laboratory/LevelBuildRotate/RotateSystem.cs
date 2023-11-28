using BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent;
using System.Threading;
using UnityEngine;

public class RotateSystem : MonoBehaviour
{
    [Header("X Axis")]
    [SerializeField] private bool useRotateX;
    [SerializeField] private float speedX;
    [Header("Y Axis")]
    [SerializeField] private bool useRotateY;
    [SerializeField] private float speedY;
    [Header("Z Axis")]
    [SerializeField] private bool useRotateZ;
    [SerializeField] private float speedZ;
    [Header("Stop")]
    [SerializeField] private bool usePause;
    [SerializeField] private float rotateTime;
    [SerializeField] private float pauseTime;

    private float rotateX;
    private float rotateY;
    private float rotateZ;
    private float rotateTimer;
    private float pauseTimer;
    private bool isRotateTimer =true;
    private bool isPauseTimer;
    private bool isRotate= true;

    private void Update()
    {
        if (usePause)
        {
            pauseSystem();
            pauseTimerSystem();
        }

        if (isRotate)
        {
            if(!usePause)
            {
                toRotate();
                SetTimer(0);
            }

            RotateX_Check();
            RotateY_Check();
            RotateZ_Check();
            RotateThis();
        }

        
    }
    #region RotateXYZ
    private void RotateX_Check()
    {
        if(useRotateX)
        {
            RotateX();
        }else
        {
            rotateX = 0;
        }
    }
    private void RotateX()
    {
        rotateX = speedX * Time.deltaTime;
    }
    private void RotateY_Check()
    {
        if(useRotateY) 
        {
            Rotate_Y();
        }else
        {
            rotateY = 0;
        }
    }
    private void Rotate_Y()
    {
        rotateY = speedY * Time.deltaTime;
    }
    private void RotateZ_Check()
    {
        if(useRotateZ)
        {
            Rotate_Z();
        }else
        {
            rotateZ = 0;
        }
    }
    private void Rotate_Z()
    {
        rotateZ = speedZ * Time.deltaTime;
    }
    private void RotateThis()
    {
        this.transform.Rotate(rotateX, rotateY, rotateZ);
    }
    #endregion
    #region Pause
    private void pauseSystem()
    {
        if(isRotateTimer)
        {
            toRotate();
        }

        if(isPauseTimer)
        {
            toPause();
        }
    }
    private void toPause()
    {
        SetIsRotate(false);
    }
    private void toRotate()
    {
        SetIsRotate(true);
    }
    private void pauseTimerSystem()
    {
        if(isRotateTimer)
        {
            rotateTimer += Time.deltaTime;
        }

        if(rotateTimer >= rotateTime)
        {
            SetIsRotateTimer(false);
            SetIsPauseTimer(true);
            SetTimer(0);
            toRotate();
        }

        if(isPauseTimer)
        {
            pauseTimer += Time.deltaTime;
        }

        if(pauseTimer >= pauseTime)
        {
            SetIsRotateTimer(true);
            SetIsPauseTimer(false);
            SetTimer(0);
            toPause();
        }
    }
    private void SetIsRotateTimer(bool value)
    {
        isRotateTimer = value;
    }
    private void SetIsPauseTimer(bool value)
    {
        isPauseTimer = value;
    }
    private void SetTimer(int value)
    {
        rotateTimer = value;
        pauseTimer = value;
    }
    private void SetIsRotate(bool value)
    {
        isRotate = value;
    }
    #endregion
}
