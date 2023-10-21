using System.Collections.Generic;
using UnityEngine;

namespace GearFactory
{
    /// <summary>
    /// Base class for meshes in GearFactory.
    /// It contains basic mesh data and common
    /// physics 2D functions.
    /// </summary>
    public abstract class GearBase : MonoBehaviour
    {
        public static bool IgnoreValidation = false;

        //mesh data
        [SerializeField, HideInInspector]
        protected List<Vector3> vertices;
        public List<Vector3> Vertices { get { return vertices; } protected set { vertices = value; } }

        [SerializeField, HideInInspector]
        protected List<int> triangles;
        public List<int> Triangles { get { return triangles; } protected set { triangles = value; } }

        [SerializeField, HideInInspector]
        protected Vector2[] uvs;
        public Vector2[] UVs { get { return uvs; } protected set { uvs = value; } }

        //mesh components
        [SerializeField, HideInInspector]
        protected Mesh mesh;
        public Mesh _Mesh { get { return mesh; } protected set { mesh = value; } }

        [SerializeField, HideInInspector]
        protected MeshFilter c_mf;
        public MeshFilter C_MF { get { return c_mf; } protected set { c_mf = value; } }

        [SerializeField, HideInInspector]
        protected MeshRenderer c_mr;
        public MeshRenderer C_MR { get { return c_mr; } protected set { c_mr = value; } }

        protected PolygonCollider2D C_EC2D;

        //gear data
        [SerializeField, HideInInspector]
        protected int sides;
        [SerializeField, HideInInspector]
        protected float innerRadius;
        [SerializeField, HideInInspector]
        protected float rootRadius;
        [SerializeField, HideInInspector]
        protected float rootAngleShiftMultiplier;
        [SerializeField, HideInInspector]
        protected float outerRadius;
        [SerializeField, HideInInspector]
        protected float outerAngleShiftMultiplier;
        [SerializeField, HideInInspector]
        protected float tipRadius;
        [SerializeField, HideInInspector]
        protected float tipAngleShiftMultiplier;
        [SerializeField, HideInInspector]
        protected float teethSlant;

        [ContextMenu("Open in Gear Creator")]
        public void OpenInGearCreator()
        {
            GameObject gearCreator = new GameObject("GearCreator from " + name);
            gearCreator.transform.position = transform.position;
            GearCreator creatorComponent = gearCreator.AddComponent<GearCreator>();

            #region Copy current Gear Values

            creatorComponent.material = C_MR.material;

            //get any Rigidbody component
            Component rb = GetComponent<Rigidbody2D>();
            rb = GetComponent<Rigidbody>() ?? rb;
            creatorComponent.attachRigidbody = rb != null;

            //for 2d collider
            {
                Collider2D col = GetComponent<Collider2D>();
                if (col != null)
                {
                    creatorComponent.bounciness = col.bounciness;
                    creatorComponent.friction = col.bounciness;
                }
            }

            //for 3d collider
            {
                Collider col = GetComponent<Collider>();
                if (col != null)
                {
                    creatorComponent.bounciness = col.sharedMaterial.bounciness;
                    creatorComponent.friction = col.sharedMaterial.bounciness;
                }
            }

            creatorComponent.gearSides = sides;
            creatorComponent.gearInnerRadius = innerRadius;
            creatorComponent.gearRootRadius = rootRadius;
            creatorComponent.gearRootAngleShiftMultiplier = rootAngleShiftMultiplier;
            creatorComponent.gearOuterRadius = outerRadius;
            creatorComponent.gearOuterAngleShiftMultiplier = outerAngleShiftMultiplier;
            creatorComponent.gearTipRadius = tipRadius;
            creatorComponent.gearTipAngleShiftMultiplier = tipAngleShiftMultiplier;
            creatorComponent.gearTeethSlant = teethSlant;

            #endregion
        }

        public abstract void Rebuild();

        public void ReassignMaterial()
        {
            //todo czy to reassign jest potrzebne tylko w edytorze?
            C_MR.sharedMaterial = DefaultMaterialProvider.GetDefaultMaterial3D(true);
        }

        //get vertices transformed by {TransformPoint}
        public Vector3[] GetTransformedVertices()
        {
            Vector3[] verts = new Vector3[Vertices.Count];
            for (int i = 0; i < verts.Length; i++)
            {
                verts[i] = transform.TransformPoint(Vertices[i]);
            }
            return verts;
        }

        public abstract int RemoveAllJoints();

    }
}
