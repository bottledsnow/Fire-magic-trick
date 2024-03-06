using StarterAssets;
using UnityEngine;
using System.Threading.Tasks;
using MoreMountains.Feedbacks;

public class PlayerState : MonoBehaviour
{
    private ThirdPersonController _controller;
    
    [SerializeField] private LayerMask mask;
    [SerializeField] private float groundRayDistance = 1f;
    [Header("Gravity")]
    [SerializeField] private float gravityFire;
    [SerializeField] private float gravityFloat;

    [Header("Obj")]
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject[] Sctipts;

    //Script
    private Shooting_Check _shooting_check;
    private PlayerAnimator playerAnimator;

    //variable
    private float gravityNormal;
    private float aimTimer;
    private bool isToAim;
    private float toAimTime = 0.15f;
    private float targetRotationSpeed;

    public bool nearGround;
    public bool isGround;
    public bool isFire;
    public bool isFloat;
    public bool canFloat;

    [Header("Feedbacks")]
    [SerializeField] private MMF_Player Feedbacks_Debuff;

    private void Awake()
    {
        _controller = GetComponent<ThirdPersonController>();
        playerAnimator = GetComponent<PlayerAnimator>();
    }
    private void Start()
    {
        _shooting_check = GameManager.singleton.ShootingSystem.GetComponent<Shooting_Check>();
        gravityNormal = _controller.Gravity;
    }
    private void Update()
    {
        CheckGround();
        CheckFloat();
        getIsGround();
        turnToAimDirection();
        toAimTimerSystem();
    }
    private void toAimTimerSystem()
    {
        if(isToAim)
        {
            aimTimer += Time.deltaTime;
        }
        if(aimTimer >= toAimTime)
        {
            SetIsToAim(false);
            aimTimer = 0;
        }
    }
    private void turnToAimDirection()
    {
        if(isToAim)
        {
            Vector3 worldAimTarget = _shooting_check.mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(aimDirection, transform.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * targetRotationSpeed);
        }
    }
    private void CheckGround()
    {
        Ray ray = new Ray(this.transform.position, -this.transform.up);
        RaycastHit hit;
        RayHitCheck(ray, out hit);
    }
    private void RayHitCheck(Ray ray, out RaycastHit hit)
    {
        nearGround = Physics.Raycast(ray, out hit, groundRayDistance, mask);
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
    public async void TakeControl_Dialogue()
    {
        playerAnimator.EndDialogue();
        _controller.useGravity = true;
        _controller.useMove = true;

        await Task.Delay(250);

        for (int i = 0; i < Sctipts.Length; i++)
        {
            Sctipts[i].SetActive(true);
        }
    }
    public void OutControl()
    {
        _controller.useMove = false;
    }
    public void OutControl_Dialogue()
    {
        for (int i = 0; i < Sctipts.Length; i++)
        {
            Sctipts[i].SetActive(false);
        }

        playerAnimator.ToDialogue_Idel();
        _controller.useMove = false;
    }
    public void SetUseGravity(bool value)
    {
        _controller.useGravity = value;
    }
    public void SetUseMove(bool value)
    {
        _controller.useMove = value;
    }
    public void SetCollider(bool value)
    {
        if(value ==true)
        {
            this.gameObject.layer = 6;
        }
        if(value == false)
        {
            this.gameObject.layer = 17;
        }
    }
    public void SetGravityActive(bool active)
    {
        _controller.useGravity = active;
    }
    public void SetMoveActive(bool active)
    {
        _controller.useMove = active;
    }   
    public void setModel(bool active) { model.SetActive(active); }
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
    public void SetGravity(float value)
    {
        _controller.Gravity = value;
    }
    private void getIsGround()
    {
        isGround = _controller.Grounded;
    }
    public void SetIsFloat(bool active)
    {
        _controller.isFloat = active;
    }
    public void SetVerticalVelocity(float value)
    {
        _controller.SetVerticalVelocity(value);
    }
    public void AddVerticalVelocity(float Value)
    {
        _controller.AddVerticalVelocity(Value);
    }
    public void TurnToAimDirection()
    {
        Vector3 worldAimTarget = _shooting_check.mouseWorldPosition;
        worldAimTarget.y = transform.position.y;
        Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(aimDirection, transform.up);
        transform.rotation = targetRotation;
        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 7.5f);
    }
    public void TurnToAimDirection(float rotationSpeed)
    {
        targetRotationSpeed = rotationSpeed;
        SetIsToAim(true);
    }
    public void TurnToNewDirection(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction, transform.up);
        transform.rotation = targetRotation;
    }
    public void SetUseCameraRotate(bool active)
    {
        _controller.useCameraRotate = active;
    }
    public async void DebuffPlay(float debuffTime)
    {
        OutControl();
        Feedbacks_Debuff.PlayFeedbacks();
        await Task.Delay((int)(debuffTime * 1000));
        Feedbacks_Debuff.StopFeedbacks();
        TakeControl();
    }
    private void SetIsToAim(bool active)
    {
        isToAim = active;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(this.transform.position, -this.transform.up * groundRayDistance);
    }
}
