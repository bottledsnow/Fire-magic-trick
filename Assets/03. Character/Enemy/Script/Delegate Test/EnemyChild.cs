using UnityEngine;

public class EnemyChild : MonoBehaviour
{
    [SerializeField] private EventHandler eventHandler;

    private void Start()
    {
        eventHandler.OnEnemyFightToPlayer += DebugMessage;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            eventHandler.OnEnemyFightToPlayer -= DebugMessage;
        }
    }
    private void DebugMessage()
    {
        Debug.Log("123 Hit Player");
    }
}
