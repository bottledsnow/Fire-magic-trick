using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(ArrayLayout))]
public class CustPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PrefixLabel(position, label);
        Rect newposition = position;
        newposition.y += 18f;
        SerializedProperty data = property.FindPropertyRelative("rows");

        for (int i = 0; i < 9; ++i)
        {
            SerializedProperty row = data.GetArrayElementAtIndex(i).FindPropertyRelative("row");

            newposition.height = 18f;

            if(row.arraySize !=9) row.arraySize = 9;

            newposition.width = 18;

            for(int j = 0; j < 9; ++j)
            {
                EditorGUI.PropertyField(newposition, row.GetArrayElementAtIndex(j), GUIContent.none);
                newposition.x += newposition.width;
            }

            newposition.x = position.x;
            newposition.y += 18f;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 18f * 11;
    }

}
