using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerFSMBaseState
{
    private bool jumpInput;
    private bool dashInput;
    protected bool aimInput;

    protected bool isGrounded;
    protected bool isOnSlope;

    private float lastTimePlayStepSound;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetAmountOfJumpsLeft();
        player.DashState.ResetCanDash();
        player.InAirState.ResetFloatCount();
        player.CardSystem.SetKickStrongShoot(false);
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = collisionSenses.Ground;
    }

    public override void Exit()
    {
        base.Exit();

        player.Anim.ResetTrigger("land");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        jumpInput = player.InputHandler.JumpInput;
        dashInput = player.InputHandler.DashInput;
        aimInput = player.InputHandler.AimInput;

        CheckIfShouldShoot();

        if (!isExitingState)
        {
            if (jumpInput)
            {
                stateMachine.ChangeState(player.JumpState);
            }
            else if (!isGrounded)
            {
                player.InAirState.StartCoyoteTime();
                stateMachine.ChangeState(player.InAirState);
            }
            else if (dashInput && player.DashState.CanDash())
            {
                stateMachine.ChangeState(player.DashState);
            }
            else if (player.InputHandler.FireTransferInput && player.SuperDashState.CanSuperDash())
            {
                player.SuperDashState.SetTarget(player.CardSystem.SuperDashTarget);
                stateMachine.ChangeState(player.SuperDashState);
            }
            else if (player.InputHandler.SuperJumpInput && player.SuperJumpState.CanUseAbility())
            {
                stateMachine.ChangeState(player.SuperJumpState);
            }
            else if (player.InputHandler.SkillInput && player.CardSystem.CheckCardEnergy(playerData.altEnergyCost))
            {
                if(player.CardSystem.CurrentEquipedCard == CardSystem.CardType.Wind && EnemyDetection(playerData.longRangeDetectRadius).Count > 0)
                {
                    // TODO: 現在範圍沒有目標也能開
                    stateMachine.ChangeState(player.WindAltState);
                }
                else if(player.CardSystem.CurrentEquipedCard == CardSystem.CardType.Fire)
                {
                    stateMachine.ChangeState(player.FireAltState);
                }
            }
        }
    }

    protected void CheckPlayStepSound(float waitTime)
    {
        if(Time.time >= waitTime + lastTimePlayStepSound)
        {
            lastTimePlayStepSound = Time.time;

            AudioManager.Instance.PlayRandomSoundFX(playerData.footStepSFX, player.transform, AudioManager.SoundType.twoD);
        }
    }

}
