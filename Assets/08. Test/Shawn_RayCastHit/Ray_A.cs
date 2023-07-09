using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class Ray_A : MonoBehaviour
{
    [SerializeField] private float MaxDistance;
    [SerializeField] private LayerMask mask;
    private Vector3 m_position;
    private Vector3 direction;
    private void Start()
    {
    }
    private void Update()
    {
        m_position = transform.position;
        direction = transform.forward;
    }
    private void RayCastHitTest()
    {
        Ray ray = new Ray(m_position, direction);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, MaxDistance, mask))
        {
            Debug.Log("Hit Target");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * MaxDistance);
    }
}
