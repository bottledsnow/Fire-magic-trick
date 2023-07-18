using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card_Rotate : MonoBehaviour
{
    public Transform Target;
    [SerializeField] private float angleSpeed;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask mask;

    private Vector3 direction;
    private Quaternion rotateToTarget;

    private void Update()
    {
        RotateSystem();
        Check();
    }
    private void RotateSystem()
    {
        if(Target!=null)
        {
            GetDirection();
            Rotate();
        }
    }
    private void GetDirection()
    {
        direction = Target.position - transform.position;
        rotateToTarget = Quaternion.LookRotation(direction);
    }
    private void Rotate()
    {
        if (Vector3.Angle(direction, transform.forward) > 0.1f)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, rotateToTarget, angleSpeed);
        }
    }
    private void Check()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance, mask))
        {
            if(hit.collider.CompareTag("CombustiblesObj"))
            {
                Target = hit.collider.transform;
            }
        }
    }

}
