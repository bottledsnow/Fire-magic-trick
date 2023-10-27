using UnityEngine;

public class GameMannager1103 : MonoBehaviour
{
    private Transform player;

    [SerializeField] private Transform Area_A;
    [Header("Test")]
    [SerializeField] private bool StartToArea_A = false;
    [SerializeField] private GameObject TeachSystem;
    private void Start()
    {
        player = GameManager.singleton.Player;

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
}
