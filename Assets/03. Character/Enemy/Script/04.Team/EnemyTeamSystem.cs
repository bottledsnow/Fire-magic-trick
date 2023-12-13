using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class EnemyTeamSystem : MonoBehaviour
{
    [Header("EnemyType")]
    [SerializeField] public AttackingStyle attackingStyle;

    public enum AttackingStyle { Melee, Ranged }

    void Start()
    {

    }

    void Update()
    {

    }
}
