using StarterAssets;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private ThirdPersonController _controller;
    
    [SerializeField] private LayerMask mask;
    [SerializeField] private float rayDistance = 1f;

    [Header("Gravity")]
    [SerializeField] private float gravityFire;
    [SerializeField] private float gravityFloat;

    private Shooting_Check _shooting_check;
    private float gravityNormal;

    public bool nearGround;
    public bool isGround;
    public bool isFire;
    public bool isFloat;
    public bool canFloat;

    private void Start()
    {
        _shooting_check = GameManager.singleton.ShootingSystem.GetComponent<Shooting_Check>();
        _controller = GetComponent<ThirdPersonController>();
        gravityNormal = _controller.Gravity;
    }
    private void Update()
    {
        CheckGround();
        CheckFloat();
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
    private void CheckFloat()
    {
        if(_controller._verticalVelocity < _controller.Gravity * _controller.FallTimeout && !isFire)
        {
            canFloat = true;
        } else
        {
            canFloat = false;
        }
    }
    public void TakeControl()
    {
        _controller.useGravity = true;
        _controller.useMove = true;
    }
    public void OutControl()
    {
        _controller.useGravity = false;
        _controller.useMove = false;
    }
    public void SetGravityActive(bool active)
    {
        _controller.useGravity = active;
    }
    public void SetMoveActive(bool active)
    {
        _controller.useMove = active;
    }   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(this.transform.position, -this.transform.up * rayDistance);
    }
    
    public void SetGravityToNormal()
    {
        _controller.Gravity = gravityNormal;
        isFire = false;
        isFloat = false;
    }
    public void ResetVerticalvelocity()
    {
        _controller.SetVerticalVelocity(0);
    }
    public void SetGravityToFire()
    {
        _controller.Gravity = gravityFire;
        isFire = true;
    }
    public void SetGravityToFloat()
    {
        _controller.Gravity = gravityFloat;
        isFloat = true;
    }
    private void getIsGround()
    {
        isGround = _controller.Grounded;
    }
    public void SetIsFloat(bool active)
    {
        _controller.isFloat = active;
    }
    public void TurnToAimDirection()
    {
            Vector3 worldAimTarget = _shooting_check.mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(aimDirection, transform.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 7.5f);
    }
}
