using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolSystem : MonoBehaviour
{
    [Header("Editor")]
    public bool showPatrolEditor;

    [Header("PatrolSetting")]
    public Transform[] waypoints;  // 路徑點的陣列
    public float stoppingDistance = 0.2f; // 判定抵達路徑點的距離
    public bool looping; // 路徑是否為閉環

    [Header("Info")]
    [SerializeField] private int currentWaypointIndex = 0;
    public Transform currentWaypoint; // 即將前往的路徑點位置

    private bool isForth = true;

    private void Start()
    {
        if (waypoints.Length > 0)
        {
            currentWaypoint = waypoints[currentWaypointIndex];
        }
    }

    private void Update()
    {
        if (waypoints.Length == 0)
        {
            return;
        }

        if (Vector3.Distance(transform.position, currentWaypoint.position) <= stoppingDistance)
        {
            // 到達目標路徑點，選擇下一個路徑點
            if (!looping)
            {
                if (currentWaypointIndex == 0)
                {
                    isForth = true;
                }
                if (currentWaypointIndex == waypoints.Length - 1)
                {
                    isForth = false;
                }
                SelectNextWaypoint();
            }
            else
            {
                SelectNextWaypoint_Looping();
            }
        }
    }

    private void SelectNextWaypoint()
    {
        if (isForth)
        {
            currentWaypointIndex = currentWaypointIndex + 1;
        }
        else
        {
            currentWaypointIndex = currentWaypointIndex - 1;
        }
        currentWaypoint = waypoints[currentWaypointIndex];
    }

    private void SelectNextWaypoint_Looping()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        currentWaypoint = waypoints[currentWaypointIndex];
    }
}