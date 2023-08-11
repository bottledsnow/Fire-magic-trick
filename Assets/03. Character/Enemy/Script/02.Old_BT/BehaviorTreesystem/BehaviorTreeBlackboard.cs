using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTreeBlackboard : MonoBehaviour
{
    // 玩家物件
    public GameObject player;
    // 與玩家距離
    public float distanceToPlayer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position,player.transform.position);
    }
}
