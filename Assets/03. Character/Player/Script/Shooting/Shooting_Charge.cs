using UnityEngine;

public class Shooting_Charge : MonoBehaviour
{
    private ControllerInput _input;

    [SerializeField] private float chargeInterval;

    [Header("Level")]
    [SerializeField] private int maxLevel;
    [SerializeField] private int fireLevel;

    [Header("Bullet")]
    [SerializeField] private Transform FirePosition;
    [SerializeField] private float chargeSpeed;
    [SerializeField] private float chargeLifeTime;
    [SerializeField] private GameObject pfBullet;

    [Header("Particle")]
    [SerializeField] private GameObject pfChargeBall_Normal;
    [SerializeField] private GameObject pfChargeBall_Fire;
    [SerializeField] private GameObject pfFire;
    [SerializeField] private GameObject pfHit_Normal;
    [SerializeField] private GameObject pfHit_Fire;

    private Shooting_Check _shootin_Check;
    private PlayerState _playerState;
    private GameObject chargeBullet;
    private float timer;
    private bool isCharge;
    private bool RT;
    private bool isFire;
    private bool max;
    private int level;

    private void Start()
    {
        _input = GameManager.singleton._input;
        _shootin_Check = GameManager.singleton.ShootingSystem.GetComponent<Shooting_Check>();
        _playerState = GameManager.singleton.Player.GetComponent<PlayerState>();
    }

    private void Update()
    {
        CheckRT();
        Charge();
        Timer();
        Initialization();
    }
    #region Key
    private void Initialization()
    {
        if (!_input.RT && RT)
        {
            RT = false;
            Debug.Log("Initialize");
        }
    }
    private void CheckRT()
    {
        if (_input.RT && !RT)
        {
            RT = true;
            isCharge = true;
            chargeStart();
            Debug.Log("RT");
        }
    }
    #endregion
    #region Timer
    private void Charge()
    {
        if(timer >= chargeInterval)
        {
            if(level == maxLevel && !max)
            {
                LevelMaxFeedback();
                max = true;
            }
            if(level < maxLevel)
            {
                LevelAddFeedback();
                level++;
            }
            if (level >= fireLevel && !isFire)
            {
                LevelFireFeedback();
                isFire = true;
            }
            timer = 0;
        }
    }
    private void Timer()
    {
        if(RT)
        {
            _playerState.TurnToAimDirection();
            timer += Time.deltaTime;
        }else
        {
            if(isCharge)
            {
                ChargeShoot();
                isCharge = false;
            }
            InitializeTimer();
        }
    }
    private void InitializeTimer()
    {
        timer = 0;
        level = 0;
        isFire = false;
        max = false;
    }
    #endregion
    private void chargeStart()
    {
        chargeBullet = Instantiate(pfBullet);
        chargeBullet.transform.SetParent(FirePosition);
        chargeBullet.transform.position = FirePosition.position;
    }
    private void LevelAddFeedback()
    {
        Debug.Log("Level:" + level);
        if(level < fireLevel)
        {
            instantiateBulletBall(pfChargeBall_Normal);
        }else
        {
            instantiateBulletBall(pfChargeBall_Fire);
        }
    }
    private void LevelFireFeedback()
    {
        Debug.Log("Fire");
        instantiateBulletFire();
    }
    private void LevelMaxFeedback()
    {
        Debug.Log("MaxLevel");
    }
    private void ChargeShoot()
    {
        Debug.Log("ChargeShoot");
        Vector3 aimDir = (_shootin_Check.mouseWorldPosition - FirePosition.position).normalized;
        chargeBullet.transform.SetParent(null);
        chargeBullet.transform.rotation = Quaternion.LookRotation(aimDir, Vector3.up);
        pfCharge pfCharge = chargeBullet.AddComponent<pfCharge>();

        pfCharge.InitializeValue(chargeSpeed, chargeLifeTime,pfHit_Normal,pfHit_Fire);
        pfCharge.IsFire = isFire;
    }
    private void instantiateBulletBall(GameObject pf)
    {
        if(chargeBullet != null)
        {
            float x = Random.Range(0, 360);
            float y = Random.Range(0, 360);
            float z = Random.Range(0, 360);
            Quaternion rotation = Quaternion.Euler(x, y, z);
            GameObject Ball = Instantiate(pf, chargeBullet.transform.position, rotation);
            Ball.transform.SetParent(chargeBullet.transform);
        }
    }
    private void instantiateBulletFire()
    {
        if(chargeBullet != null)
        {
            GameObject Fire = Instantiate(pfFire, chargeBullet.transform.position, chargeBullet.transform.rotation);
            Fire.transform.SetParent(chargeBullet.transform);
        }
    }
}
