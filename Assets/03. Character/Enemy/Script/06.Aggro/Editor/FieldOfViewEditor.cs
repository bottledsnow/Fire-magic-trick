using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyAggroSystem))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyAggroSystem fov = (EnemyAggroSystem)target;
        Vector3 viewPosition = new Vector3(fov.transform.position.x , fov.transform.position.y + fov.viewHeight , fov.transform.position.z);

        if (fov.showFovEditor)
        {
            Handles.color = Color.white;
            Handles.DrawWireArc(viewPosition, Vector3.up, Vector3.forward, 360, fov.fovRadius);

            Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.fovAngle / 2);
            Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.fovAngle / 2);

            Handles.color = Color.yellow;
            Handles.DrawLine(viewPosition, viewPosition + viewAngle01 * fov.fovRadius);
            Handles.DrawLine(viewPosition, viewPosition + viewAngle02 * fov.fovRadius);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
