using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("---------能量---------")]
    public float healthDecreaseRate = 5f;
    public float healthDecreaseRateBurning = 7.5f;
    public float normalDashEnergyCost = 10f;
    public float superDashEnergyCost = 15f;
    public float floatEnergyCostPerSceond = 10f;

    [Header("---------攻擊數值---------")]
    public float dashDamage = 10f;
    public float dashKnockbackForce = 10f;

    [Header("大跳")]
    public float superJumpFireDamage = 30f;
    public float superJumpFireJumpKnockbackForce = 8f;
    public float superJumpLandKnockbackForce = 14f;
    public float superJumpBurnTime = 2f;

    [Header("穿梭")]
    public float superDashDamage = 10f;
    public float superDashKnockbackForce = 10f;
    public float superDashBurnTime = 5f;

    [Header("大招")]
    public float windAltDamage = 20f;
    public float fireAltFireRate = 0.75f;
    public float fireAltFireBullet = 15f;
    public float fireAltFireTime = 5f;

    [Header("---------冷卻與移動---------")]
    [Header("Basic")]
    public LayerMask whatIsCombatDetectable;
    public LayerMask whatIsEnemy;
    public float rotationSpeed = 15f;
    public float rotateSmoothTime = 0.1f;
    public float zeroRangeDetectRadius = 1f;
    public float closeRangeDetectRadius = 2f;
    public float midRangeDetectRadius = 3f;
    public float longRangeDetectRadius = 4f;

    [Header("Move")]
    public Sound[] footStepSFX;
    public float walkSpeed = 3f;
    public float slowRunSpeed = 5f;
    public float fastRunSpeed = 8f;
    public float aimMoveSpeed = 2f;
    public float airMoveSpeed = 3f;

    [Header("Jump")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 1;
    public float jumpInpusStopYSpeedMultiplier = 0.5f;
    public float coyoteTime = 0.2f;
    public float frameOfDecaySpeed = 0.75f;

    [Header("InAir")]
    public Sound landSFX;
    public int maxFloatCount = 1;
    public float floatSpeed = -1f;
    public float floatHoldJumpTime = 0.2f;
    public float inAirMaxFloatTime = 3f;

    [Header("Dash")]
    public Sound dashSound;
    public float dashSpeed = 10f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1f;

    [Header("Super Jump")]
    public Sound superJumpStartSound;
    public Sound superJumpLandSound;
    public float superJumpVelocity = 30f;
    public int superJumpEnergyCost = 3;
    public float superJumpFallAddForce = 10f;
    public float superJumpFallStartVelocity = -6f;

    [Header("Super Dash")]
    public Sound superDashHitSFX;
    public float maxSuperDashSpeed = 25f;
    public AnimationCurve superDashSpeedGraph;
    public float speedUpTime = 0.5f;
    public float maxSuperDashTime = 2f;
    public float superDashCooldown = 0.75f;
    public float afterSuperDashMultiplier = 0.65f;
    public float targetYOffset = 1f;

    [Header("Super Dash Jump")]
    public float superDashJumpVelocity = 10f;
    public Vector3 superDashFootDetectBox;
    public float superDashJumpKnockbackSpeed = 10f;

    [Header("Fireball Fall")]
    public float fireballSpeed = 4f;
    public float fireballForceValue = 12.5f;
    public float fireballMaxYVelocity = -0.5f;
    public float fireballMinYVelocity = -2f;
    public float fireballMaxTime = 3f;

    public int altEnergyCost = 6;
    [Header("WindAlt")]
    public float windBulletTimeDuration = 0.2f;
    public float windAltMaxTime = 5f;
    public float windAltSpeedUpTime = 2.5f;
    public float windAltMaxSpeed = 24f;
    public AnimationCurve windAltSpeedCurve;

    [Header("FireAlt")]

    [Header("Death and Respawn")]
    public float deathAnimationTime = 2.5f;
    public float respawnTime = 2f;
}
