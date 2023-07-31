using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTreeBlackboard : MonoBehaviour
{
    public GameObject player;
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
