using UnityEngine;

public class GameMannager1103 : MonoBehaviour
{
    private Transform player;

    [SerializeField] private Transform Area_A;
    [Header("Test")]
    [SerializeField] private bool StartToArea_A = false;
    [SerializeField] private GameObject TeachSystem;
    [Header("Mannager")]
    [SerializeField] PrototypeN2_Enemy _prototypeN2_Enemy;
    private void Start()
    {
        player = GameManager.singleton.Player;

        StartAreaCheck();   
    }
    private void Update()
    {
        TestTool();
        TestRebirth();
    }
    private void TestTool()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            PassTeaching();
        }
        
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
        }
    }
    private void StartArea_A()
    {
        TeachSystem.SetActive(false);
        PassTeaching();
    }
    private void TestRebirth()
    {
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            _prototypeN2_Enemy.ReSetState();
        }
    }
}
