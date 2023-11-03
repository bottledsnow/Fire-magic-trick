using System.Runtime.CompilerServices;
using UnityEngine;

public class GameMannager1103 : MonoBehaviour
{
    private Transform player;

    [SerializeField] private Transform Area_A;
    [Header("StartA")]
    [SerializeField] private bool StartToArea_A = false;
    [SerializeField] private SenceManager senceMannager;
    private LimitForTeach TeachLimit;
    private GameObject Player;

    private ProgressSystem _progressSystem;
    [SerializeField] private ProgressCheckPoint _progresCheck_A;
    [SerializeField] private ProgressCheckPoint _progresCheck_Battle;

    private void Start()
    {
        player = GameManager.singleton.Player;
        _progressSystem = GameManager.singleton.GetComponent<ProgressSystem>();
        TeachLimit = GameManager.singleton.GetComponent<LimitForTeach>();
        Player = GameManager.singleton.Player.gameObject;

        StartAreaCheck();   
    }
    private void Update()
    {
        TestTool();
    }
    private void TestTool()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToAreaA();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            ToBattle();
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            reBirthPlayerSkill();
        }
    }
    private void ToAreaA()
    {
        _progressSystem.ProgressCheckPoint = _progresCheck_A.transform;
        ToTeleport(_progresCheck_A);
        reBirthPlayerSkill();
    }
    private void ToBattle()
    {
        _progressSystem.ProgressCheckPoint = _progresCheck_Battle.transform;
        ToTeleport(_progresCheck_Battle);
        reBirthPlayerSkill();
    }
    private void ToTeleport(ProgressCheckPoint point)
    {
        player.transform.position = point.transform.position;
    }
    private void reBirthPlayerSkill()
    {
        TeachLimit.useTeach = false;
        TeachLimit.Initialization();
    }
    private void PassTeaching()
    {
        player.transform.position = Area_A.transform.position;
    }
    private void StartAreaCheck()
    {
        if(StartToArea_A)
        {
            StartArea_A();
            TeachLimit.useTeach = false;
            TeachLimit.Initialization();
            senceMannager.PassTeach = true;
        }else
        {
            TeachLimit.useTeach = true;
            TeachLimit.Initialization();
            senceMannager.PassTeach = false;
        }
    }
    private void StartArea_A()
    {
        PassTeaching();
    }
}
