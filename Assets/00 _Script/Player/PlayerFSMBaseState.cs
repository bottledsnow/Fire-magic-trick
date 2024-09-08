using System.Collections.Generic;
using UnityEngine;

public class PlayerFSMBaseState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected Core core;
    protected Movement movement;
    protected CollisionSenses collisionSenses;
    protected Stats stats;

    protected bool isAnimationFinished;
    protected bool isAnimationStartMovement;
    protected bool isExitingState;

    protected float StartTime;
    protected float ExitTime;

    private string animBoolName;

    protected Vector2 MovementInput { get; private set; }
    protected bool AttackInput { get; private set; }
    private Vector3 v3Workspace;
    private Vector2 v2Workspace;


    public PlayerFSMBaseState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;

        core = player.Core;
        movement = core.GetCoreComponent<Movement>();
        collisionSenses = core.GetCoreComponent<CollisionSenses>();
        stats = core.GetCoreComponent<Stats>();

        StartTime = 0f;
        ExitTime = 0f;
    }
    
    public virtual void Enter()
    {
        DoChecks();
        StartTime = Time.time;
        player.Anim.SetBool(animBoolName, true);

        isAnimationFinished = false;
        isExitingState = false;
        isAnimationStartMovement = false;
    }
    public virtual void Exit()
    {
        player.Anim.SetBool(animBoolName, false);
        isExitingState = true;
        ExitTime = Time.time;
    }

    public virtual void LogicUpdate()
    {
        MovementInput = player.InputHandler.RawMovementInput;
        AttackInput = player.InputHandler.AttackInput;
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    public virtual void DoChecks() { }

    public virtual void AnimationActionTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;

    public virtual void AnimationStartMovementTrigger() { isAnimationStartMovement = true; }

    public virtual void AnimationStopMovementTrigger() { isAnimationStartMovement = false; }

    public virtual void AnimationSFXTrigger() { }

    private float targetRotation;

    protected void Rotate(float rotationSpeed, float rotateSmoothTime, bool turnRelateWithInput = true)
    {
        if (turnRelateWithInput)
        {
            if (player.InputHandler.RawMovementInput != Vector2.zero)
            {
                targetRotation = Mathf.Atan2(player.InputHandler.RawMovementInput.x, player.InputHandler.RawMovementInput.y) * Mathf.Rad2Deg
                    + player.InputHandler.MainCam.transform.eulerAngles.y;
            }
            else
            {
                targetRotation = Mathf.Atan2(0f, -1f) * Mathf.Rad2Deg + player.InputHandler.MainCam.transform.eulerAngles.y;
            }
        }
        else
        {
            targetRotation = player.InputHandler.MainCam.transform.eulerAngles.y;
        }

        float rotation = Mathf.SmoothDampAngle(player.transform.eulerAngles.y, targetRotation, ref rotationSpeed, rotateSmoothTime * Time.fixedDeltaTime);

        movement.Rotate(rotation);
    }

    protected void MoveWithInput(float minSpeed, float maxSpeed, bool ignoreSlope = false)
    {
        v3Workspace.Set(MovementInput.x, 0f, MovementInput.y);

        if (v3Workspace.magnitude > 1f)
        {
            v3Workspace.Normalize();
        }

        float speed = GetRelativeSpeedWithInput(minSpeed, maxSpeed, v3Workspace.magnitude);

        Vector3 targetDirection = movement.ParentTransform.forward * v3Workspace.z + movement.ParentTransform.right * v3Workspace.x;
        v2Workspace.Set(targetDirection.x, targetDirection.z);

        movement.SetVelocity(speed, v2Workspace, ignoreSlope);
    }

    protected void MoveRelateWithCam(float originalMinSpeed, float originalMaxSpeed = 0f, bool ignoreSlope = false, bool overwriteSpeed = true)
    {
        v3Workspace.Set(MovementInput.x, 0f, MovementInput.y);

        if (v3Workspace.magnitude > 1f)
        {
            v3Workspace.Normalize();
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

        if (v3Workspace.magnitude != 0f)
        {
            float speed = GetRelativeSpeedWithInput(originalMinSpeed, originalMaxSpeed, v3Workspace.magnitude);

            v2Workspace.Set(targetDirection.x, targetDirection.z);

            if (overwriteSpeed)
            {
                movement.SetVelocity(speed, v2Workspace, ignoreSlope);
            }
            else
            {
                movement.AddForce(speed, targetDirection);
            }
        }
    }

    private float GetRelativeSpeedWithInput(float min, float max, float magnitude)
    {
        float speed;

        if (max == 0f)
        {
            speed = min * magnitude;
        }
        else
        {
            speed = Mathf.Lerp(min, max, magnitude);
        }

        return speed;
    }

    protected void MoveWithFacingDir(float speed, bool ignoreSlope = false)
    {
        v3Workspace.Set(MovementInput.x, 0f, MovementInput.y);

        if (v3Workspace.magnitude > 1f)
        {
            v3Workspace.Normalize();
        }

        Vector3 targetDirection = movement.ParentTransform.forward * v3Workspace.z + movement.ParentTransform.right * v3Workspace.x;
        v2Workspace.Set(targetDirection.x, targetDirection.z);

        movement.SetVelocity(speed, v2Workspace, ignoreSlope);
    }

    protected void CheckIfShouldShoot(bool resetAttackInput = false)
    {
        if (AttackInput)
        {
            if(resetAttackInput)
            {
                player.InputHandler.UseAttackInput();
            }
            player.CardSystem.Shoot();
        }
    }

    protected List<GameObject> EnemyDetection(float detectRadius)
    {
        List<GameObject> objs = new();
        foreach (var col in Physics.OverlapSphere(player.transform.position, detectRadius, playerData.whatIsEnemy))
        {
            if (col.gameObject == player.gameObject)
            {
                continue;
            }

            if (objs.Contains(col.gameObject))
            {
                continue;
            }

            objs.Add(col.gameObject);
        }
        return objs;
    }
protected List<GameObject> SphereDetection(float detectRadius)
    {
        List<GameObject> objs = new();
        foreach(var col in Physics.OverlapSphere(player.transform.position, detectRadius, playerData.whatIsCombatDetectable))
        {
            if(col.gameObject == player.gameObject)
            {
                continue;
            }

            if(objs.Contains(col.gameObject))
            {
                continue;
            }

            objs.Add(col.gameObject);
        }
        return objs;
    }
}
