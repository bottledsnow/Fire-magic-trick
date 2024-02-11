using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting_Check : MonoBehaviour
{
    [Header("Shoot Check")]
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] public Transform debugTransform;
    [SerializeField] private float MaxShootDistance;
    [HideInInspector] public Vector3 mouseWorldPosition = Vector3.zero;

    private void Update()
    {
        ShootRay();
    }
    private void ShootRay()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        RaycastHit hit;
        bool raycastHit = Physics.Raycast(ray, out hit, 999f, aimColliderLayerMask);

        if (raycastHit)
        {
            debugTransform.position = hit.point;
            mouseWorldPosition = hit.point;
        }
        else
        {
            debugTransform.position = ray.origin + ray.direction * MaxShootDistance;
            mouseWorldPosition = debugTransform.position;
        }
    }
}
