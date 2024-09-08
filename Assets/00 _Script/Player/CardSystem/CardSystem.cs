using Cinemachine;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class CardSystem : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerVFXController playerVFXController;
    [SerializeField] private GameObject normalCardPrefab;
    [SerializeField] private GameObject windCardPrefab;
    [SerializeField] private GameObject fireCardPrefab;
    [SerializeField] private GameObject strongWindCardPrefab;
    [SerializeField] private GameObject strongFireCardPrefab;
    [field: SerializeField] public Sound[] ShootSFXs { get; private set; }
    [SerializeField] private CinemachineImpulseSource impulseSource;

    [Header("Spawn Position")]
    [SerializeField] private Transform frontSpawnPos;
    [SerializeField] private Transform backSpawnPos;
    [SerializeField] private Transform[] fireAltSpawnPos;

    [Header("Shoot Check")]
    [SerializeField] private float strongShootBulletTimeDuration;
    [SerializeField] private LayerMask aimColliderLayerMask;
    [SerializeField] public Transform debugTransform;
    [SerializeField] private float maxShootDistance;
    [SerializeField] private float minShootDistance;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float cardStateDuration = 5f;
    private bool bulletTimeShoot;
    private bool strongShoot;
    private bool kickStrongShoot;

    [Header("Super Dash")]
    [SerializeField] private LayerMask whatIsSuperDashTarget;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float superDashDistance = 10f;
    public Transform SuperDashTarget { get; private set; }
    public bool HasSuperDashTarget { get; private set; }

    private Vector3 targetPosition;
    private Vector2 screenCenterPoint;

    /// <summary>
    /// 負責能否射出屬性牌的狀態
    /// </summary>
    private CardType currentCardType;
    /// <summary>
    /// 負責當前裝備的屬性牌
    /// </summary>
    public CardType CurrentEquipedCard { get; private set; }
    public enum CardType
    {
        Normal,
        Wind,
        Fire
    }

    private float startCardStateTime;
    private float endCardStateTime;

    private float startShootTime;

    [SerializeField] private int windMaxEnergy;
    [SerializeField] private int fireMaxEnergy;
    public int WindCardEnergy { get; private set; }
    public int FireCardEnergy { get; private set; }
    public event Action<int> OnWindCardEnergyChanged;
    public event Action<int> OnFireCardEnergyChanged;

    public void Init()
    {
        currentCardType = CardType.Normal;
        CurrentEquipedCard = CardType.Fire;
        startCardStateTime = 0f;
        endCardStateTime = 0f;
        startShootTime = 0f;

        DecreaseWindCardEnergy(WindCardEnergy);
        DecreaseFireCardEnergy(FireCardEnergy);
    }

    private void OnEnable()
    {
        targetPosition = Vector3.zero;
        screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);

        currentCardType = CardType.Normal;
        CurrentEquipedCard = CardType.Fire;
        startCardStateTime = 0f;
        endCardStateTime = 0f;
        startShootTime = 0f;

        HasSuperDashTarget = false;
        bulletTimeShoot = false;
        strongShoot = false;
        kickStrongShoot = false;
    }

    private void Start()
    {
        OnWindCardEnergyChanged += HandleWindEnergyChange;
        OnFireCardEnergyChanged += HandleFireEnergyChange;
    }

    private void OnDestroy()
    {
        OnWindCardEnergyChanged -= HandleWindEnergyChange;
        OnFireCardEnergyChanged -= HandleFireEnergyChange;
    }

    private void HandleWindEnergyChange(int value)
    {
        playerVFXController.SetWindCountVFX(value);
        UIManager.Instance.HudUI.CardCount.SetWindCount(value);

        if (value == windMaxEnergy)
        {
            playerVFXController.SetWindMaxStar(true);
            playerVFXController.PlayWindMax();
        }
        else
        {
            playerVFXController.SetWindMaxStar(false);
        }
    }

    private void HandleFireEnergyChange(int value)
    {
        playerVFXController.SetFireCountVFX(value);
        UIManager.Instance.HudUI.CardCount.SetFireCount(value);

        if (value == fireMaxEnergy)
        {
            playerVFXController.SetFireMaxStar(true);
            playerVFXController.PlayFireMax();
        }
        else
        {
            playerVFXController.SetFireMaxStar(false);
        }
    }

    private void Update()
    {
        ShootRay();

        if (player.InputHandler.DebugInput)
        {
            player.InputHandler.UseDebugInput();
            AddFireCardEnergy();
            AddWindCardEnergy();
            player.Stats.Health.Init();
        }

        if(currentCardType != CardType.Normal)
        {
            if(Time.time >= endCardStateTime)
            {
                ChangeCard(CardType.Normal);
            }
        }
    }

    #region Shoot

    private void ShootRay()
    {
        Ray ray = player.InputHandler.MainCam.ScreenPointToRay(screenCenterPoint);

        bool shootRaycastHit = Physics.Raycast(ray, out RaycastHit hit, 999f, aimColliderLayerMask);
        bool superDashRaycastHit = Physics.Raycast(ray, out RaycastHit superDashHit, superDashDistance, whatIsSuperDashTarget);

        if (shootRaycastHit)
        {
            debugTransform.position = hit.point;
            targetPosition = hit.point;
        }
        else
        {
            debugTransform.position = ray.origin + ray.direction * maxShootDistance;
            targetPosition = debugTransform.position;
        }

        if (superDashRaycastHit)
        {
            bool groundHit = Physics.Raycast(ray, superDashHit.distance, whatIsGround);

            if (!groundHit)
            {
                HasSuperDashTarget = true;
                SuperDashTarget = superDashHit.transform;
                UIManager.Instance.SetCrossRed();
            }
            else
            {
                HasSuperDashTarget = false;
                SuperDashTarget = null;
                UIManager.Instance.SetCrossWhite();
            }
        }
        else
        {
            HasSuperDashTarget = false;
            SuperDashTarget = null;
            UIManager.Instance.SetCrossWhite();
        }
    }

    public void Shoot()
    {
        if (Time.unscaledTime < startShootTime + shootCooldown)
        {
            return;
        }

        startShootTime = Time.unscaledTime;
        UIManager.Instance.CrosshairShooting();

        Vector3 aimDir = (targetPosition - frontSpawnPos.position).normalized;

        if(Vector3.Distance(targetPosition, frontSpawnPos.position) < minShootDistance)
        {
            aimDir = transform.forward;
        }

        player.Anim.SetTrigger("shoot");
        // Debug.Log(transform.forward - player.InputHandler.MainCam.transform.forward);

        if (bulletTimeShoot)
        {
            BulletTimeManager.Instance.BulletTime_Slow(strongShootBulletTimeDuration);

            Instantiate(normalCardPrefab, frontSpawnPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
        }
        else
        {
            if(kickStrongShoot && strongShoot)
            {
                strongShoot = false;
                kickStrongShoot = false;
                AudioManager.Instance.PlayRandomSoundFX(ShootSFXs, transform, AudioManager.SoundType.twoD);

                impulseSource.GenerateImpulse(2f);
                switch (currentCardType)
                {
                    case CardType.Normal:
                        Debug.LogError("Kick strong shoot can't be normal card");
                        break;
                    case CardType.Wind:
                        Instantiate(strongWindCardPrefab, frontSpawnPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
                        break;
                    case CardType.Fire:
                        Instantiate(strongFireCardPrefab, frontSpawnPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
                        break;
                }
            }
            else if (strongShoot)
            {
                strongShoot = false;
                switch (currentCardType)
                {
                    case CardType.Normal:
                        Debug.LogError("Strong shoot can't be normal card");
                        break;
                    case CardType.Wind:
                        ShootThreeWind(aimDir);
                        break;
                    case CardType.Fire:
                        impulseSource.GenerateImpulse(1.5f);
                        AudioManager.Instance.PlayRandomSoundFX(ShootSFXs, transform, AudioManager.SoundType.twoD);
                        Instantiate(fireCardPrefab, frontSpawnPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
                        Instantiate(fireCardPrefab, frontSpawnPos.position, Quaternion.LookRotation(aimDir, Vector3.up) * Quaternion.Euler(0f, 45f / 2f, 0f));
                        Instantiate(fireCardPrefab, frontSpawnPos.position, Quaternion.LookRotation(aimDir, Vector3.up) * Quaternion.Euler(0f, -45f / 2f, 0f));
                        break;
                }
            }
            else
            {
                AudioManager.Instance.PlayRandomSoundFX(ShootSFXs, transform, AudioManager.SoundType.twoD);
                impulseSource.GenerateImpulse(1f);
                switch (currentCardType)
                {
                    case CardType.Normal:
                        Instantiate(normalCardPrefab, frontSpawnPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
                        break;
                    case CardType.Wind:
                        Instantiate(windCardPrefab, frontSpawnPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
                        break;
                    case CardType.Fire:
                        Instantiate(fireCardPrefab, frontSpawnPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
                        break;
                }
            }
        }
    }

    private async void ShootThreeWind(Vector3 dir)
    {
        for (int i = 0; i < 3; i++)
        {
            impulseSource.GenerateImpulse(1f);
            AudioManager.Instance.PlayRandomSoundFX(ShootSFXs, transform, AudioManager.SoundType.twoD);
            Instantiate(windCardPrefab, frontSpawnPos.position, Quaternion.LookRotation(dir, Vector3.up));
            await Task.Delay(200);
        }
    }

    public void FireAltShoot()
    {
        impulseSource.GenerateImpulse(1.5f);
        Transform target = fireAltSpawnPos[UnityEngine.Random.Range(0, fireAltSpawnPos.Length)];
        Vector3 aimDir = ((target.position + new Vector3(UnityEngine.Random.Range(-0.25f, 0.25f), 0f, UnityEngine.Random.Range(-0.25f, 0.25f))) - transform.position).normalized;

        Instantiate(normalCardPrefab, target.position, Quaternion.LookRotation(aimDir, Vector3.up));
    }

    public void SetBulletTimeShoot(bool value)
    {
        bulletTimeShoot = value;
    }

    public void SetKickStrongShoot(bool value)
    {
        kickStrongShoot = value;
    }
    #endregion

    public void ChangeCard(CardType cardType)
    {
        if(cardType == CardType.Wind)
        {
            strongShoot = true;
            UIManager.Instance.HudUI.HudVFX.WindStateIndicater(true);
        }
        else if(cardType == CardType.Fire)
        {
            strongShoot = true;
            UIManager.Instance.HudUI.HudVFX.FireStateIndicater(true);
        }
        else
        {
            strongShoot = false;
            UIManager.Instance.HudUI.HudVFX.WindStateIndicater(false);
            UIManager.Instance.HudUI.HudVFX.FireStateIndicater(false);
        }

        ChangeCurrentEquipCard(cardType);

        startCardStateTime = Time.time;
        endCardStateTime = startCardStateTime + cardStateDuration;
        currentCardType = cardType;
    }

    private void ChangeCurrentEquipCard(CardType cardType)
    {
        if(CurrentEquipedCard == cardType)
        {
            return;
        }

        if (cardType == CardType.Wind)
        {
            UIManager.Instance.HudUI.HudVFX.FlipToWindCard();
            CurrentEquipedCard = CardType.Wind;
        }
        else if (cardType == CardType.Fire)
        {
            UIManager.Instance.HudUI.HudVFX.FlipToFireCard();
            CurrentEquipedCard = CardType.Fire;
        }
    }

    #region Card Energy

    public bool CheckCardEnergy(int amount)
    {
        if (WindCardEnergy == 0 && FireCardEnergy == 0)
        {
            return false;
        }

        if (CurrentEquipedCard == CardType.Wind)
        {
            if(WindCardEnergy >= amount)
            {
                return true;
            }
            else
            {
                if (FireCardEnergy >= amount)
                {
                    ChangeCurrentEquipCard(CardType.Fire);
                    return true;
                }
            }
        }
        else if (CurrentEquipedCard == CardType.Fire)
        {
            if (FireCardEnergy >= amount)
            {
                return true;
            }
            else
            {
                if (WindCardEnergy >= amount)
                {
                    ChangeCurrentEquipCard(CardType.Wind);
                    return true;
                }
            }
        }

        return false;
    }

    public void DecreaseCardEnergy(int amount)
    {
        if(CurrentEquipedCard == CardType.Wind)
        {
            DecreaseWindCardEnergy(amount);
        }
        else if(CurrentEquipedCard == CardType.Fire)
        {
            DecreaseFireCardEnergy(amount);
        }
    }

    public void AddWindCardEnergy()
    {
        if (WindCardEnergy == windMaxEnergy)
        {
            return;
        }
        WindCardEnergy++;
        WindCardEnergy = Mathf.Clamp(WindCardEnergy, 0, windMaxEnergy);
        OnWindCardEnergyChanged?.Invoke(WindCardEnergy);
    }

    public void DecreaseWindCardEnergy(int value)
    {
        WindCardEnergy -= value;
        WindCardEnergy = Mathf.Clamp(WindCardEnergy, 0, windMaxEnergy);
        OnWindCardEnergyChanged?.Invoke(WindCardEnergy);
    }

    public void AddFireCardEnergy()
    {
        FireCardEnergy++;
        FireCardEnergy = Mathf.Clamp(FireCardEnergy, 0, fireMaxEnergy);
        OnFireCardEnergyChanged?.Invoke(FireCardEnergy);
    }

    public void DecreaseFireCardEnergy(int value)
    {
        FireCardEnergy -= value;
        FireCardEnergy = Mathf.Clamp(FireCardEnergy, 0, fireMaxEnergy);
        OnFireCardEnergyChanged?.Invoke(FireCardEnergy);
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, superDashDistance);
    }
}
