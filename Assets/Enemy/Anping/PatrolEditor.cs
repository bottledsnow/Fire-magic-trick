using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Patrol))]
public class PatrolEditor : Editor
{
    private void OnSceneGUI()
    {
        Patrol patrol = (Patrol)target;

        if (patrol.waypoints == null || patrol.waypoints.Length == 0)
        {
            return;
        }

        for (int i = 0; i < patrol.waypoints.Length; i++)
        {
            EditorGUI.BeginChangeCheck();
            Vector3 newWaypointPosition = Handles.PositionHandle(patrol.waypoints[i].position, Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(patrol.waypoints[i], "Move Waypoint");
                patrol.waypoints[i].position = newWaypointPosition;
            }

            if (i < patrol.waypoints.Length - 1)
            {
                Handles.DrawLine(patrol.waypoints[i].position, patrol.waypoints[i + 1].position);
            }
            else if (patrol.waypoints.Length > 1 && patrol.looping)
            {
                Handles.DrawLine(patrol.waypoints[i].position, patrol.waypoints[0].position);
            }
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}