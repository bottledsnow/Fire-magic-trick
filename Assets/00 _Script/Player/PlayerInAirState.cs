using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerFSMBaseState
{
    private bool jumpInput;
    private bool jumpInputStop;
    private bool dashInput;

    private float minYVelocity;
    private float maxYVelocity;

    private bool isGrounded;

    private float jumpStartTime;
    public bool IsJumping { get; private set; }
    private bool coyoteTime;

    private float inAirMovementSpeed;

    private bool setAirControlSpeed;

    private bool isFloating;
    private float startFloatingTime;
    private int floatCount;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        isGrounded = false;
        IsJumping = false;
        coyoteTime = false;
        inAirMovementSpeed = playerData.airMoveSpeed;

        jumpStartTime = 0f;
    }

    public override void Enter()
    {
        base.Enter();
        isFloating = false;
        startFloatingTime = 0f;

        if (!setAirControlSpeed)
        {
            if (movement.CurrentVelocityXZMagnitude > playerData.airMoveSpeed)
            {
                if (movement.CurrentVelocityXZMagnitude > playerData.dashSpeed)
                {
                    SetAirControlSpeed(playerData.dashSpeed);
                }
                else
                {
                    SetAirControlSpeed(movement.CurrentVelocityXZMagnitude);
                }
            }
            else
            {
                SetAirControlSpeed(playerData.airMoveSpeed);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        jumpStartTime = 0f;

        minYVelocity = Mathf.Infinity;
        maxYVelocity = Mathf.NegativeInfinity;
        isGrounded = false;
        IsJumping = false;
        coyoteTime = false;
        inAirMovementSpeed = playerData.airMoveSpeed;
        setAirControlSpeed = false;

        player.VFXController.SetCanComboVFX(false);
        player.VFXController.SetFloatVFX(false);
        player.CardSystem.SetBulletTimeShoot(false);
    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (collisionSenses)
        {
            isGrounded = collisionSenses.Ground;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckIfShouldShoot();
        CheckCoyoteTime();

        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;
        dashInput = player.InputHandler.DashInput;

        CheckJumpMultiplier();

        if (minYVelocity > movement.CurrentVelocity.y)
        {
            minYVelocity = movement.CurrentVelocity.y;
        }
        if (maxYVelocity < movement.CurrentVelocity.y)
        {
            maxYVelocity = movement.CurrentVelocity.y;
        }

        if (collisionSenses.Ground && !IsJumping)
        {
            if(movement.CurrentVelocity.y < -1f)
            {
                player.Anim.SetTrigger("land");
                AudioManager.Instance.PlaySoundFX(playerData.landSFX, player.transform, AudioManager.SoundType.twoD);
            }

            if (inAirMovementSpeed < playerData.slowRunSpeed && MovementInput != Vector2.zero)
            {
                stateMachine.ChangeState(player.WalkingState);
            }
            else if (MovementInput != Vector2.zero)
            {
                stateMachine.ChangeState(player.RunningState);
            }
            else
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
        else if (player.JumpState.CanJump() && jumpInput && Time.time - player.InputHandler.JumpInputStartTime < playerData.floatHoldJumpTime)
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (dashInput && player.DashState.CanDash())
        {
            stateMachine.ChangeState(player.DashState);
        }
        else if(player.InputHandler.SuperJumpInput && player.SuperJumpState.CanUseAbility())
        {
            stateMachine.ChangeState(player.SuperJumpState);
        }
        else if (player.InputHandler.FireTransferInput && player.SuperDashState.CanSuperDash())
        {
            player.SuperDashState.SetTarget(player.CardSystem.SuperDashTarget);
            stateMachine.ChangeState(player.SuperDashState);
        }
        else if (player.InputHandler.SkillInput && player.CardSystem.CheckCardEnergy(playerData.altEnergyCost))
        {
            if (player.CardSystem.CurrentEquipedCard == CardSystem.CardType.Wind && EnemyDetection(playerData.longRangeDetectRadius).Count > 0)
            {
                stateMachine.ChangeState(player.WindAltState);
            }
            else if (player.CardSystem.CurrentEquipedCard == CardSystem.CardType.Fire)
            {
                stateMachine.ChangeState(player.FireAltState);
            }
        }
        else
        {
            MoveRelateWithCam(inAirMovementSpeed, 0f, true);

            if(!isFloating && floatCount < playerData.maxFloatCount && minYVelocity < -1f && !collisionSenses.LongGround)
            {
                player.VFXController.SetCanComboVFX(true);
            }
            else
            {
                player.VFXController.SetCanComboVFX(false);
            }

            if (!isFloating && floatCount < playerData.maxFloatCount && stats.Health.GapBetweenCurrentAndMax > playerData.floatEnergyCostPerSceond * 0.5f 
                && player.InputHandler.OrgJumpInput && minYVelocity < -1f && !collisionSenses.LongGround)
            {
                floatCount++;
                player.InputHandler.UseJumpInput();
                isFloating = true;
                startFloatingTime = Time.time;
                player.VFXController.SetFloatVFX(true);
                player.CardSystem.SetBulletTimeShoot(true);
            }
            else if (isFloating && (!player.InputHandler.OrgJumpInput || Time.time - startFloatingTime > playerData.inAirMaxFloatTime || collisionSenses.LongGround || stats.Health.CurrentValue == stats.Health.MaxValue))
            {
                isFloating = false;
                player.VFXController.SetFloatVFX(false);
                player.CardSystem.SetBulletTimeShoot(false);
            }

            if (isFloating)
            {
                movement.SetVelocityY(playerData.floatSpeed);
                stats.Health.Increase(playerData.floatEnergyCostPerSceond * Time.deltaTime);
            }

            inAirMovementSpeed = Mathf.Lerp(inAirMovementSpeed, playerData.airMoveSpeed, playerData.frameOfDecaySpeed * Time.deltaTime);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (MovementInput != Vector2.zero)
        {
            Rotate(playerData.rotationSpeed, playerData.rotateSmoothTime);
        }
    }

    private void CheckJumpMultiplier()
    {
        if (IsJumping)
        {
            if (jumpInputStop && Time.time - jumpStartTime > 2f * Time.fixedDeltaTime)
            {
                movement.SetVelocityY(movement.CurrentVelocity.y * playerData.jumpInpusStopYSpeedMultiplier);

                IsJumping = false;
            }
            else if (minYVelocity < -1f)
            {
                IsJumping = false;
            }
        }
    }

    public void ResetFloatCount()
    {
        floatCount = 0;
    }

    public bool CheckCanFloat()
    {
        return floatCount < playerData.maxFloatCount;
    }

    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time >= StartTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    public void SetIsJumping()
    {
        jumpStartTime = Time.time;

        IsJumping = true;
    }

    public void SetIsJumpingFalse()
    {
        IsJumping = false;
    }

    public void StartCoyoteTime()
    {
        coyoteTime = true;
    }

    public void SetAirControlSpeed(float speed)
    {
        inAirMovementSpeed = speed;
        setAirControlSpeed = true;
    }
}
