using UnityEngine;

public class Shooting_Charge : MonoBehaviour
{
    private ControllerInput _input;

    [SerializeField] private float chargeInterval;

    [Header("Level")]
    [SerializeField] private int maxLevel;
    [SerializeField] private int fireLevel;

    [Header("Bullet")]
    [SerializeField] private GameObject pfChargeBall_Normal;
    [SerializeField] private GameObject pfChargeBall_Fire;
    [SerializeField] private GameObject fire;
    private float timer;
    private bool isCharge;
    private bool RT;
    private bool isFire;
    private bool max;
    private int level;

    private void Start()
    {
        _input = GameManager.singleton._input;
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
    
    private void LevelAddFeedback()
    {
        Debug.Log("Level:" + level);
    }
    private void LevelFireFeedback()
    {
        Debug.Log("Fire");
    }
    private void LevelMaxFeedback()
    {
        Debug.Log("MaxLevel");
    }
    private void ChargeShoot()
    {
        Debug.Log("ChargeShoot");
    }
}
