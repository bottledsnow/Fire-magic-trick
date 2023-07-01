#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FireCheck))]
public class FireCheckEditor : Editor
{
    private void OnSceneGUI()
    {
        FireCheck fireCheck = (FireCheck)target;

        // 繪製區域的中心位置
        Handles.color = Color.white;
        Handles.DrawWireDisc(fireCheck.transform.position, fireCheck.transform.up, fireCheck.size);

        // 計算區域的起始和終止角度
        float startAngle = fireCheck.transform.eulerAngles.y - fireCheck.angle / 2f;
        float endAngle = fireCheck.transform.eulerAngles.y + fireCheck.angle / 2f;

        // 轉換為 Unity 的弧度制
        startAngle *= Mathf.Deg2Rad;
        endAngle *= Mathf.Deg2Rad;

        // 計算起始和終止點的位置
        Vector3 startPoint = fireCheck.transform.position + Quaternion.Euler(0f, startAngle * Mathf.Rad2Deg, 0f) * fireCheck.transform.forward * fireCheck.distance;
        Vector3 endPoint = fireCheck.transform.position + Quaternion.Euler(0f, endAngle * Mathf.Rad2Deg, 0f) * fireCheck.transform.forward * fireCheck.distance;

        // 繪製區域的起始和終止點
        Handles.color = Color.yellow;
        Handles.DrawLine(fireCheck.transform.position, startPoint);
        Handles.DrawLine(fireCheck.transform.position, endPoint);

        // 更新距離屬性
        EditorGUI.BeginChangeCheck();
        float newDistance = Handles.RadiusHandle(Quaternion.identity, fireCheck.transform.position, fireCheck.distance);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(fireCheck, "Change Distance");
            fireCheck.distance = newDistance;
        }
    }
}
#endif