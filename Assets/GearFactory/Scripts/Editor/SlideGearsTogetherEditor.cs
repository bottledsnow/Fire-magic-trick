using UnityEditor;
using UnityEngine;

namespace GearFactory
{
    [CustomEditor(typeof(SlideGearsTogether))]
    public class SlideGearsTogetherEditor : Editor
    {
        //standard override
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            SlideGearsTogether targetScript = (SlideGearsTogether)target;

            if (targetScript.CheckGears())
            {
                HandleSliding(targetScript);
            }
            else
            {
                ShowInfo();
            }
        }

        private static void HandleSliding(SlideGearsTogether targetScript)
        {
            bool buttonWasPressed = GUILayout.Button("Slide gears");

            if (buttonWasPressed || targetScript.liveUpdate)
            {
                SlideGearsTogether.SlideGears(
                    targetScript.gearA,
                    targetScript.gearB,
                    targetScript.slideFactor);
            }
        }

        private static void ShowInfo()
        {
            EditorGUILayout.HelpBox("Assign gear objects first.", MessageType.Info);
        }

    }
}
