using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    private ThirdPersonController controller;
    public bool nearGround;
    public bool isGround;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float rayDistance = 1f;

    [Header("Gravity")]
    [SerializeField] private float gravityFire;
    private float gravityNormal;


    private void Start()
    {
        controller = GetComponent<ThirdPersonController>();
        gravityNormal = controller.Gravity;
    }
    private void Update()
    {
        CheckGround();
        getIsGround();  
    }
    private void CheckGround()
    {
        Ray ray = new Ray(this.transform.position, -this.transform.up);
        RaycastHit hit;
        RayHitCheck(ray, out hit);
    }
    private void RayHitCheck(Ray ray, out RaycastHit hit)
    {
        nearGround = Physics.Raycast(ray, out hit, rayDistance, mask);
    }
    public void TakeControl()
    {
        controller.useGravity = true;
        controller.useMove = true;
    }
    public void OutControl()
    {
        controller.useGravity = false;
        controller.useMove = false;
    }
    public void SetGravityActive(bool active)
    {
        controller.useGravity = active;
    }
    public void SetMoveActive(bool active)
    {
        controller.useMove = active;
    }   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(this.transform.position, -this.transform.up * rayDistance);
    }
    public void SetGravityToFire()
    {
        controller.Gravity = gravityFire;
    }
    public void SetGravityToNormal()
    {
        controller.Gravity = gravityNormal;
    }
    private void getIsGround()
    {
        isGround = controller.Grounded;
    }
}
