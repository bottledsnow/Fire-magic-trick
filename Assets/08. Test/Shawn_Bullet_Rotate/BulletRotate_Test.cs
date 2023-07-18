using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRotate_Test : MonoBehaviour
{
    public Transform Target;
    [SerializeField] private float angleSpeed;
    [SerializeField] private bool isRotate = true;

    private Vector3 direction;
    private Quaternion rotateToTarget;
    private void Update()
    {
        GetDirection();
        Rotate_Start();
        Rotate_End();
    }
    private void Rotate_Start()
    {
        if (isRotate)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, rotateToTarget, angleSpeed);
        }
    }
    private void Rotate_End()
    {
        if (Vector3.Angle(direction, transform.forward) < 0.1f)
        {
            isRotate = false;
        }
    }
    private void GetDirection()
    {
        direction = Target.position - transform.position;
        rotateToTarget = Quaternion.LookRotation(direction);
    }
}
