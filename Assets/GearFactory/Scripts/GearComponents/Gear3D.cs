using System.Collections.Generic;
using UnityEngine;

namespace GearFactory
{
    /// <summary>
    /// Class holding the properties of three
    /// dimensional gear generated upon gear2D.
    /// </summary>
    public class Gear3D : GearBase
    {
        private float thickness;
        private List<Vector3> originalVertices;
        private List<int> originalTriangles;
        private Vector2[] originalUVs;

        public void Init(GearStructure structure, List<Vector3> vertices, List<int> triangles, Vector2[] uvs, float thickness)
        {
            originalVertices = vertices;
            originalTriangles = triangles;
            originalUVs = uvs;
            this.thickness = thickness;

            sides = structure.sides;
            innerRadius = structure.innerRadius;
            rootRadius = structure.rootRadius;
            rootAngleShiftMultiplier = structure.rootAngleShiftMultiplier;
            outerRadius = structure.outerRadius;
            outerAngleShiftMultiplier = structure.outerAngleShiftMultiplier;
            tipRadius = structure.tipRadius;
            tipAngleShiftMultiplier = structure.tipAngleShiftMultiplier;
            teethSlant = structure.teethSlant;

            C_MR = gameObject.GetOrAddComponent<MeshRenderer>();
            C_MF = gameObject.GetOrAddComponent<MeshFilter>();
            C_EC2D = gameObject.GetOrAddComponent<PolygonCollider2D>();
        }

        public void CopyMeshFrom(Mesh sourceMesh)
        {
            C_MF.sharedMesh = Instantiate(sourceMesh);
        }

        public override void Rebuild()
        {
            //reassign the 2D values
            C_MF.sharedMesh.Clear();
            C_MF.sharedMesh.vertices = originalVertices.ToArray();
            C_MF.sharedMesh.triangles = originalTriangles.ToArray();
            C_MF.sharedMesh.uv = originalUVs;
            //convert to 3D and assign material
            C_MF.sharedMesh = Gear3DConversion.Convert(C_MF.sharedMesh, sides, thickness);
            C_MR.material = DefaultMaterialProvider.GetDefaultMaterial3D(true);
        }

        public override int RemoveAllJoints()
        {
            Joint[] joints = GetComponents<Joint>();
            for (int i = 0; i < joints.Length; i++)
            {
                DestroyImmediate(joints[i]);
            }
            return joints.Length;
        }

        public GearStructure3D GetStructure()
        {
            return new GearStructure3D
            {
                innerRadius = innerRadius,
                rootRadius = rootRadius,
                rootAngleShiftMultiplier = rootAngleShiftMultiplier,
                outerRadius = outerRadius,
                outerAngleShiftMultiplier = outerAngleShiftMultiplier,
                tipRadius = tipRadius,
                tipAngleShiftMultiplier = tipAngleShiftMultiplier,
                sides = sides,
                teethSlant = teethSlant,
                thickness = thickness,
            };
        }
        
        #region Joints

        //remove old components to prevent conflicts
        public void Remove2DComponents()
        {
            DestroyImmediate(GetComponent<Joint2D>());
            DestroyImmediate(GetComponent<Collider2D>());
            DestroyImmediate(GetComponent<Rigidbody2D>());
        }

        //add HingeJoint at the center of the object and attach it to background
        public HingeJoint AddHingeJoint()
        {
            return AddHingeJoint(transform.position, Vector3.forward);
        }

        //add HingeJoint to the object and attach it to background
        public HingeJoint AddHingeJoint(Vector3 position, Vector3 axis)
        {
            Remove2DComponents();

            HingeJoint C_HJ = gameObject.GetOrAddComponent<HingeJoint>();
            C_HJ.axis = axis;
            C_HJ.anchor = transform.InverseTransformPoint(position);
            return C_HJ;
        }

        //specify motor - by default on center
        public HingeJoint AddHingeJoint(JointMotor C_JM)
        {
            return AddHingeJoint(C_JM, transform.position, Vector3.forward);
        }

        //specify motor
        public HingeJoint AddHingeJoint(JointMotor C_JM, Vector3 position, Vector3 axis)
        {
            Remove2DComponents();

            HingeJoint C_HJ = gameObject.GetOrAddComponent<HingeJoint>();
            C_HJ.axis = axis;
            C_HJ.anchor = transform.InverseTransformPoint(position);
            C_HJ.motor = C_JM;
            C_HJ.useMotor = true;
            return C_HJ;
        }

        //specify motor and connected body (on center, by default)
        public HingeJoint AddHingeJoint(JointMotor C_JM, Rigidbody connectedBody)
        {
            return AddHingeJoint(C_JM, connectedBody, transform.position, Vector3.forward);
        }

        //specify motor and connected body
        public HingeJoint AddHingeJoint(JointMotor C_JM, Rigidbody connectedBody, Vector3 position, Vector3 axis)
        {
            Remove2DComponents();

            HingeJoint C_HJ = gameObject.GetOrAddComponent<HingeJoint>();
            C_HJ.anchor = transform.InverseTransformPoint(position);
            C_HJ.axis = axis;
            C_HJ.motor = C_JM;
            C_HJ.useMotor = true;
            C_HJ.connectedBody = connectedBody;
            return C_HJ;
        }

        //fix object to background
        public FixedJoint AddFixedJoint()
        {
            Remove2DComponents();

            FixedJoint C_HJ = gameObject.GetOrAddComponent<FixedJoint>();
            C_HJ.anchor = transform.InverseTransformPoint(transform.position);
            return C_HJ;
        }

        #endregion
    }
}
