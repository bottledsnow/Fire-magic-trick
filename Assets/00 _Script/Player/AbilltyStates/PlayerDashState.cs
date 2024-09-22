using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    private Vector2 v2Workspace;
    private bool canUseDash;
    private int currentFrame;
    private List<GameObject> damagedObjs;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.InputHandler.UseDashInput();
        player.VFXController.SetDashVFX(true);
        stats.SetInvincible(true);

        damagedObjs = new();
        v2Workspace = new();

        movement.SetGravityZero();
        movement.SetVelocityZero();

        canUseDash = false;
        currentFrame = 0;

        stats.Health.Increase(playerData.normalDashEnergyCost);
        AudioManager.Instance.PlaySoundFX(playerData.dashSound, player.transform, AudioManager.SoundType.twoD);
    }

    public override void Exit()
    {
        base.Exit();

        player.VFXController.SetDashVFX(false);
        movement.SetGravityOrginal();
        stats.SetInvincible(false);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        foreach(var obj in SphereDetection(playerData.zeroRangeDetectRadius))
        {
            if (!damagedObjs.Contains(obj))
            {
                obj.TryGetComponent(out IDamageable damageable);
                damageable?.Damage(playerData.dashDamage, player.transform.position);
                obj.TryGetComponent(out IKnockbackable knockbackable);
                knockbackable?.Knockback(player.transform.position, playerData.dashKnockbackForce);

                damagedObjs.Add(obj);
            }
        }

        if(currentFrame <= 3)
        {
            Rotate(playerData.rotationSpeed * 20f, 0.01f);
        }

        if (currentFrame == 3)
        {
            if (MovementInput.y > 0f)
            {
                player.CardSystem.ChangeCard(CardSystem.CardType.Fire);
            }
            else
            {
                player.CardSystem.ChangeCard(CardSystem.CardType.Wind);
            }
        }

        currentFrame++;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        v2Workspace.Set(movement.ParentTransform.forward.x, movement.ParentTransform.forward.z);
        movement.SetVelocity(playerData.dashSpeed, v2Workspace);

        if (Time.time > StartTime + playerData.dashTime)
        {
            if (collisionSenses.Ground)
            {
                stateMachine.ChangeState(player.RunningState);
            }
            else
            {
                isAbilityDone = true;
            }
        }
    }

    public bool CanDash() => canUseDash && stats.Health.GapBetweenCurrentAndMax >= playerData.normalDashEnergyCost && (Time.time >= ExitTime + playerData.dashCooldown || ExitTime == 0f);

    public void ResetCanDash() => canUseDash = true;
}
