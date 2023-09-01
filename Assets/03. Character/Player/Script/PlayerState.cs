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
    private float gravityNormal;

    public bool nearGround;
    public bool isGround;
    public bool canFloat;

    private void Start()
    {
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
        if(_controller._verticalVelocity < _controller.Gravity * _controller.FallTimeout)
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
    public void SetGravityToFire()
    {
        _controller.Gravity = gravityFire;
    }
    public void SetGravityToNormal()
    {
        _controller.Gravity = gravityNormal;
    }
    public void SetGravityToFloat()
    {
        _controller.Gravity = gravityFloat;
    }
    private void getIsGround()
    {
        isGround = _controller.Grounded;
    }
    public void SetIsFloat(bool active)
    {
        _controller.isFloat = active;
    }   
}
