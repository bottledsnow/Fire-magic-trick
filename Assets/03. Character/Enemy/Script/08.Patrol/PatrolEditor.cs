using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyPatrolSystem))]
public class PatrolEditor : Editor
{
    EnemyPatrolSystem patrol;

    private void OnSceneGUI()
    {
        patrol = (EnemyPatrolSystem)target;

        if (patrol.waypoints == null || patrol.waypoints.Length == 0)
        {
            return;
        }
        
        if(patrol.showPatrolEditor) DrawPatrolPath();
    }

    private void DrawPatrolPath()
    {
        for (int i = 0; i < patrol.waypoints.Length; i++)
        {
            if (patrol.waypoints[i] != null)
            {
                EditorGUI.BeginChangeCheck();
                Vector3 newWaypointPosition = Handles.PositionHandle(patrol.waypoints[i].position, Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(patrol.waypoints[i], "Move Waypoint");
                    patrol.waypoints[i].position = newWaypointPosition;
                }

                Handles.color = Color.blue;

                if (i < patrol.waypoints.Length - 1 && patrol.waypoints[i + 1] != null)
                {
                    Handles.DrawLine(patrol.waypoints[i].position, patrol.waypoints[i + 1].position, 5);
                }
                else if (patrol.waypoints.Length > 1 && patrol.waypoints[0] != null && patrol.looping)
                {
                    Handles.DrawLine(patrol.waypoints[i].position, patrol.waypoints[0].position, 5);
                }
            }
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}