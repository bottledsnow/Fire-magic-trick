using UnityEditor;
using UnityEngine;

namespace GearFactory
{
    [CustomEditor(typeof(GearCreator), true)]
    public class GearCreatorEditor : Editor
    {
        private bool hasGuiChanged;
        private const float physicsWarningThreshold = 0.25f;

        //standard override
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GearCreator gearCreatorScript = target as GearCreator;
            HandleUndo(gearCreatorScript);

            DrawMeshTypeInspector(gearCreatorScript);

            if (GUILayout.Button("Build"))
            {
                Build(gearCreatorScript);
            }
        }

        private void OnSceneGUI()
        {
            GearCreator mc = (GearCreator)target;

            //cache positions
            Vector2 p2 = mc.transform.position;
            Vector3 p3 = mc.transform.position;

            DrawHandles(mc, p2, p3);
        }

        private void DrawHandles(GearCreator gearCreator, Vector2 p2, Vector3 p3)
        {
            // save current GearCreator state
            Undo.RecordObject(gearCreator, "tets");
            Vector2 inner = gearCreator.gearInnerControlPoint;
            Vector2 root = gearCreator.gearRootControlPoint;
            Vector2 outer = gearCreator.gearOuterControlPoint;
            Vector2 tip = gearCreator.gearTipControlPoint;

            gearCreator.gearInnerControlPoint =
                GearHelper.SetVectorLength(gearCreator.gearInnerControlPoint, gearCreator.gearInnerRadius);
            gearCreator.gearRootControlPoint =
                GearHelper.SetVectorLength(gearCreator.gearRootControlPoint, gearCreator.gearRootRadius);
            gearCreator.gearOuterControlPoint =
                GearHelper.SetVectorLength(gearCreator.gearOuterControlPoint, gearCreator.gearOuterRadius);
            gearCreator.gearTipControlPoint = GearHelper.SetVectorLength(gearCreator.gearTipControlPoint, gearCreator.gearTipRadius);

            gearCreator.gearInnerControlPoint =
                Handles.DoPositionHandle(p2 + gearCreator.gearInnerControlPoint, Quaternion.identity) - p3;
            gearCreator.gearRootControlPoint =
                Handles.DoPositionHandle(p2 + gearCreator.gearRootControlPoint, Quaternion.identity) - p3;
            gearCreator.gearOuterControlPoint =
                Handles.DoPositionHandle(p2 + gearCreator.gearOuterControlPoint, Quaternion.identity) - p3;
            gearCreator.gearTipControlPoint =
                Handles.DoPositionHandle(p2 + gearCreator.gearTipControlPoint, Quaternion.identity) - p3;

            gearCreator.gearInnerRadius = gearCreator.gearInnerControlPoint.magnitude;
            gearCreator.gearRootRadius = gearCreator.gearRootControlPoint.magnitude;
            gearCreator.gearOuterRadius = gearCreator.gearOuterControlPoint.magnitude;
            gearCreator.gearTipRadius = gearCreator.gearTipControlPoint.magnitude;

            // remove undo if nothing has changed
            if (inner == gearCreator.gearInnerControlPoint && root == gearCreator.gearRootControlPoint && outer == gearCreator.gearOuterControlPoint && tip == gearCreator.gearTipControlPoint)
            {
                Undo.ClearUndo(gearCreator);
            }
        }

        private void DrawMeshTypeInspector(GearCreator gearCreator)
        {
            gearCreator.gearInnerRadius = EditorGUILayout.Slider("Inner radius",
                gearCreator.gearInnerRadius, 0.0001f, gearCreator.gearRootRadius);
            gearCreator.gearRootRadius = EditorGUILayout.Slider("Root radius",
                gearCreator.gearRootRadius, gearCreator.gearInnerRadius,
                gearCreator.gearOuterRadius);
            gearCreator.gearRootAngleShiftMultiplier = EditorGUILayout.Slider("Root angle shift",
                gearCreator.gearRootAngleShiftMultiplier, -0.5f, 0.5f);
            gearCreator.gearOuterRadius = EditorGUILayout.Slider("Outer radius",
                gearCreator.gearOuterRadius, gearCreator.gearRootRadius,
                gearCreator.gearTipRadius);
            gearCreator.gearOuterAngleShiftMultiplier = EditorGUILayout.Slider("Outer angle shift",
                gearCreator.gearOuterAngleShiftMultiplier, -0.5f, 0.5f);
            gearCreator.gearTipRadius = EditorGUILayout.Slider("Tip radius", gearCreator.gearTipRadius,
                gearCreator.gearOuterRadius, 16);
            gearCreator.gearTipAngleShiftMultiplier = EditorGUILayout.Slider("Tip angle shift",
                gearCreator.gearTipAngleShiftMultiplier, -0.5f, 0.5f);
            gearCreator.gearTeethSlant =
                EditorGUILayout.Slider("Teeth slant", gearCreator.gearTeethSlant, -0.5f, 0.5f);
            gearCreator.gearSides = EditorGUILayout.IntSlider("Sides", gearCreator.gearSides, 3, 128);

            SceneView.RepaintAll();
            if (GUI.changed)
            {
                hasGuiChanged = true;
            }

            if (gearCreator.gearTipRadius < physicsWarningThreshold)
            {
                EditorGUILayout.HelpBox("It looks like this gear is very tiny! Precise physics may be unstable.", MessageType.Warning);
            }
        }

        //create gear from Inspector values
        private static void Build(GearCreator gc)
        {
            Gear createdGear = Gear.AddGear(gc.transform.position, gc.gearSides, gc.gearInnerRadius,
                gc.gearRootRadius, gc.gearRootAngleShiftMultiplier, gc.gearOuterRadius,
                gc.gearOuterAngleShiftMultiplier, gc.gearTipRadius,
                gc.gearTipAngleShiftMultiplier,
                gc.gearTeethSlant, gc.material, gc.attachRigidbody);
            createdGear.name = "Gear";

            createdGear.SetPhysicsMaterialProperties(gc.bounciness, gc.friction);
            Undo.RegisterCreatedObjectUndo(createdGear.gameObject, "creating " + createdGear.name);
        }

        private void HandleUndo(GearCreator gearCreatorScript)
        {
            if (hasGuiChanged)
            {
                Undo.RecordObject(gearCreatorScript, "Edit GearCreator");
                hasGuiChanged = false;
            }
        }
    }
}
