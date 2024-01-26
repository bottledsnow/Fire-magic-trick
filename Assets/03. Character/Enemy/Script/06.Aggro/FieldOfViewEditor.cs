using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyAggroSystem))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyAggroSystem fov = (EnemyAggroSystem)target;

        if (fov.showFovEditor)
        {
            Handles.color = Color.white;
            Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.fovRadius);

            Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.fovAngle / 2);
            Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.fovAngle / 2);

            Handles.color = Color.yellow;
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.fovRadius);
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.fovRadius);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
