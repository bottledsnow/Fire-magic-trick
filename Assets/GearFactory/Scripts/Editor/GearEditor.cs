using System.IO;
using UnityEngine;
using UnityEditor;

namespace GearFactory
{
    /// <summary>
    /// 
    /// Utility for common actions for Gears.
    /// 
    /// Saving:
    /// Meshes and objects will appear in GearFactory/ directories by default.
    /// To load, simply drag object to scene or load it by script.
    /// Watch out for overwritting saved assets!
    /// 
    /// </summary>
    [CustomEditor(typeof(GearBase), true)]
    public class GearEditor : Editor
    {
        public const string PATH_ASSETS = "Assets";
        public const string PATH_GEAR_FACTORY = "GearFactory";

        private float thickness = 0.2f;

        //standard override
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GearBase targetScript = (GearBase)target;

            DrawOptions(targetScript);
        }
        
        private void DrawOptions(GearBase targetScript)
        {
            DrawCommonOptions(targetScript);

            Gear gear2D = targetScript as Gear;
            if (gear2D)
            {
                DrawGear2DOptions(gear2D, targetScript);
            }
            Gear3D gear3D = targetScript as Gear3D;
            if (gear3D)
            {
                DrawGear3DOptions(gear3D);
            }
        }

        private void DrawCommonOptions(GearBase targetScript)
        {
            //export the mesh
            if (GUILayout.Button("Export to obj"))
            {
                //check the type of GearBase
                if (targetScript as Gear)
                {
                    //gear 2D, we need to separate vertices for proper shading
                    ExportObj.ExportToObj(targetScript.C_MF.sharedMesh, targetScript.name);
                }
                else
                {
                    //gear 3D, we have already separated the vertices
                    ExportObj.ExportToObj(targetScript.C_MF.sharedMesh, targetScript.name, false);
                }

            }

            //save GameObject
            if (GUILayout.Button("Save Prefab"))
            {
                SavePrefabToFile(targetScript, targetScript.name);
            }

            //...
            if (GUILayout.Button("Remove connected joints"))
            {
                targetScript.RemoveAllJoints();
            }

            //make the object single user of material
            if (GUILayout.Button("Detach material"))
            {
                targetScript.ReassignMaterial();
            }

            //rebuild the mesh, in case it has disappeared from window
            if (GUILayout.Button("Rebuild"))
            {
                targetScript.Rebuild();
            }
        }

        private void DrawGear2DOptions(Gear gear2D, GearBase targetScript)
        {
            //add hinge on the center
            if (GUILayout.Button("Add hinge joint 2D"))
            {
                gear2D.AddHingeJoint2D();
            }

            //fix gear to background
            if (GUILayout.Button("Add fixed joint 2D"))
            {
                gear2D.AddFixedJoint2D();
            }

            //add motor to gear
            if (GUILayout.Button("Add motor 2D"))
            {
                JointMotor2D C_JM = new JointMotor2D
                {
                    motorSpeed = 36,
                    maxMotorTorque = 2000,
                };
                gear2D.AddHingeJoint2D(C_JM);
            }

            //convert gear to 3D and assign other script
            if (GUILayout.Button("3Dize"))
            {
                gear2D.ConvertTo3D(thickness);
                Gear3D newGearComponent = Undo.AddComponent<Gear3D>(gear2D.gameObject);
                newGearComponent.Init(gear2D.GetStructure(), gear2D.Vertices, gear2D.Triangles,
                    gear2D.UVs, thickness);
                DestroyImmediate(targetScript);
            }

            //convert gear to 3D and assign other script
            if (GUILayout.Button("Create 3D gear"))
            {
                GameObject gear3D = new GameObject(name + "3D");
                Gear3D gear3DComponent = gear3D.AddComponent<Gear3D>();
                gear3DComponent.Init(gear2D.GetStructure(), gear2D.Vertices, gear2D.Triangles,
                    gear2D.UVs, thickness);
                gear3DComponent.CopyMeshFrom(targetScript.C_MF.sharedMesh);
                gear3DComponent.Rebuild();
            }

            //adjust thickness
            thickness = EditorGUILayout.FloatField("Thickness", thickness);
            if (GearBase.IgnoreValidation == false)
            {
                if (thickness < 0)
                {
                    thickness = 0;
                }
            }
        }

        private void DrawGear3DOptions(Gear3D gear3D)
        {
            //add hinge on the center
            if (GUILayout.Button("Add hinge joint"))
            {
                gear3D.AddHingeJoint();
            }

            //fix gear to background
            if (GUILayout.Button("Add fixed joint"))
            {
                gear3D.AddFixedJoint();
            }

            //add motor to gear
            if (GUILayout.Button("Add motor"))
            {
                JointMotor C_JM = new JointMotor
                {
                    force = 1000,
                    freeSpin = true,
                    targetVelocity = 36,
                };
                gear3D.AddHingeJoint(C_JM);
            }

        }

        //save MeshFilter's mesh
        private void SaveMeshToFile(Mesh mesh, Material material, string name)
        {
            CheckFolders("Saved meshes");

            //save material
            if (material != null)
            {
                SaveMaterial(material, name);
            }

            try
            {
                AssetDatabase.CreateAsset(mesh, "Assets/GearFactory/Saved meshes/" + name + ".asset");
                Debug.Log("Mesh \"" + name + ".asset\" saved succesfully at GearFactory/Saved meshes");
            }
            catch (System.Exception e)
            {
                Debug.LogError("GearFactory::Mesh Generation failed! (" + e + ")");
            }
        }

        //save entire GameObject
        private void SavePrefabToFile(GearBase gearMesh, string name)
        {
            CheckFolders("Saved prefabs");

            //mesh and it's material need to be saved too
            SaveMeshToFile(gearMesh.C_MF.sharedMesh, gearMesh.C_MR.sharedMaterial, name + "'mesh");

            PrefabUtility.CreatePrefab("Assets/GearFactory/Saved prefabs/" + name + ".prefab", gearMesh.gameObject);

            Debug.Log("Prefab \"" + name + ".prefab\" saved succesfully at GearFactory/Saved prefabs");
        }

        //save material if necessary
        private void SaveMaterial(Material material, string name)
        {
            if (string.IsNullOrEmpty(AssetDatabase.GetAssetPath(material)))
            {
                AssetDatabase.CreateAsset(material, "Assets/GearFactory/Saved meshes/" + name + "'s material.asset");
            }
        }

        //utility: check if folder exists in GF directory
        private void CheckFolders(string targetFolder)
        {
            //check for GearFactory
            string path = Path.Combine(PATH_ASSETS, PATH_GEAR_FACTORY);
            if (!AssetDatabase.IsValidFolder(path))
            {
                AssetDatabase.CreateFolder(PATH_ASSETS, PATH_GEAR_FACTORY);
            }

            //check for {targetFolder}
            if (!AssetDatabase.IsValidFolder(Path.Combine(path, targetFolder)))
            {
                AssetDatabase.CreateFolder(path, targetFolder);
            }
        }

    }
}
