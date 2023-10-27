using UnityEngine;

public class GameMannager1103 : MonoBehaviour
{
    private Transform player;

    [SerializeField] private Transform Area_A;
    private void Start()
    {
        player = GameManager.singleton.Player;
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
}
