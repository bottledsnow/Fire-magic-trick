using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform[] waypoints;  // 路徑點的陣列
    public float moveSpeed = 3f;
    public float stoppingDistance = 0.2f;
    public bool looping;

    private int currentWaypointIndex = 0;
    private Transform currentWaypoint;

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

        MoveToWaypoint();
    }

    private void MoveToWaypoint()
    {
        Vector3 moveDirection = currentWaypoint.position - transform.position;
        moveDirection.y = 0f;
        moveDirection.Normalize();

        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, currentWaypoint.position) <= stoppingDistance)
        {
            // 到達目標路徑點，選擇下一個路徑點
            SelectNextWaypoint();
        }
    }

    private void SelectNextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        currentWaypoint = waypoints[currentWaypointIndex];
    }
}