using UnityEngine;

public class TestTool_Teleport : MonoBehaviour
{
    [SerializeField] private Transform point;

    private Transform player;

    private void Start()
    {
        player = GameManager.singleton.Player;

    }
    private void Update()
    {
        if(point != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                TeleportToPoint();
            }
        }
    }
    private void TeleportToPoint()
    {
        player.transform.position = point.position;
    }
}
