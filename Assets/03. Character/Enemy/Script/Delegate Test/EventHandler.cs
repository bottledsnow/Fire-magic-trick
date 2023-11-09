using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public delegate void EnemyFightHandler();
    public event EnemyFightHandler OnEnemyFightToPlayer;

    private void Start()
    {
        OnEnemyFightToPlayer += DebugMessage;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnEnemyFightToPlayer?.Invoke();
        }
    }
    private void DebugMessage()
    {
        Debug.Log("Enemy Fight To Player");
    }
}
