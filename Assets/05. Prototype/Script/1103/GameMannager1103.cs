using UnityEngine;

public class GameMannager1103 : MonoBehaviour
{
    private Transform player;

    [SerializeField] private Transform Area_A;
    [Header("StartA")]
    [SerializeField] private bool StartToArea_A = false;
    [SerializeField] private SenceManager senceMannager;
    private LimitForTeach TeachLimit;

    private void Start()
    {
        player = GameManager.singleton.Player;
        TeachLimit = GameManager.singleton.GetComponent<LimitForTeach>();

        StartAreaCheck();   
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
