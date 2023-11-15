using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class EnemyHook : MonoBehaviour
{
    public BehaviorTree bt;
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("player") && bt != null)
        {
            bt.SendEvent("HookHit");
        }
    }
}
