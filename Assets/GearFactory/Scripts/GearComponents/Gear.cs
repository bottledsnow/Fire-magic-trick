using System.Collections.Generic;
using UnityEngine;

namespace GearFactory
{
    /// <summary>
    /// The core object of GearFactory.
    /// 
    /// Static building - get gameobject by Gear.AddGear
    /// 
    /// Building the mesh - setting the geometry
    /// 
    /// Mesh material - both visual and physics
    /// 
    /// Joints - physical joints
    /// 
    /// </summary>
    public class Gear : GearBase
    {

        #region Static Building

        public static Gear AddGear(
            Vector3 position,
            int sides,
            float innerRadius,
            float rootRadius,
            float rootAngleShiftMultiplier,
            float outerRadius,
            float outerAngleShiftMultiplier,
            float tipRadius,
            float tipAngleShiftMultiplier,
            float teethSlant,
            Material meshMatt = null,
            bool attachRigidbody = true)
        {
            GameObject gear = new GameObject();
            gear.transform.position = position;
            Gear gearComponent = gear.AddComponent<Gear>();
            gearComponent.Build(sides, innerRadius, rootRadius, rootAngleShiftMultiplier, outerRadius,
                outerAngleShiftMultiplier, tipRadius, tipAngleShiftMultiplier, teethSlant, meshMatt);
            if (attachRigidbody)
            {
                gear.AddComponent<Rigidbody2D>();
            }
            return gearComponent;
        }

        public static Gear AddGear(Vector3 position, Gear structure, Material meshMatt = null, bool attachRigidbody = true)
        {
            return AddGear(position, structure.sides, structure.innerRadius,
                    structure.rootRadius, structure.rootAngleShiftMultiplier, structure.outerRadius,
                    structure.outerAngleShiftMultiplier, structure.tipRadius,
                    structure.tipAngleShiftMultiplier,
                    structure.teethSlant, meshMatt, attachRigidbody);
        }

        #endregion

        //assign variables, get components and build mesh
        public void Build(
            int sides,
            float innerRadius,
            float rootRadius,
            float rootAngleShiftMultiplier,
            float outerRadius,
            float outerAngleShiftMultiplier,
            float tipRadius,
            float tipAngleShiftMultiplier,
            float teethSlant,
            Material meshMatt = null)
        {
            this.sides = sides;
            this.innerRadius = innerRadius;
            this.rootRadius = rootRadius;
            this.rootAngleShiftMultiplier = rootAngleShiftMultiplier;
            this.outerRadius = outerRadius;
            this.outerAngleShiftMultiplier = outerAngleShiftMultiplier;
            this.tipRadius = tipRadius;
            this.tipAngleShiftMultiplier = tipAngleShiftMultiplier;
            this.teethSlant = teethSlant;

            BuildMesh(meshMatt);
        }

        public override void Rebuild()
        {
            Build(sides, innerRadius, rootRadius, rootAngleShiftMultiplier,
                outerRadius, outerAngleShiftMultiplier, tipRadius,
                tipAngleShiftMultiplier, teethSlant, C_MR.sharedMaterial);
        }

        public override int RemoveAllJoints()
        {
            Joint2D[] joints3D = GetComponents<Joint2D>();
            for (int i = 0; i < joints3D.Length; i++)
            {
                DestroyImmediate(joints3D[i]);
            }
            Joint[] joints = GetComponents<Joint>();
            for (int i = 0; i < joints.Length; i++)
            {
                DestroyImmediate(joints[i]);
            }
            return joints3D.Length + joints.Length;
        }

        public GearStructure GetStructure()
        {
            return new GearStructure
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
            };
        }

        //perform 3D conversion on mesh's components
        public void ConvertTo3D(float thickness)
        {
            Mesh newMesh = Instantiate(C_MF.sharedMesh);
            C_MF.sharedMesh = Gear3DConversion.Convert(newMesh, sides, thickness);
            C_MR.material = DefaultMaterialProvider.GetDefaultMaterial3D(true);
        }

        #region Building the mesh

        //create mesh from given data
        private void BuildMesh(Material meshMatt)
        {
            if (IgnoreValidation || ValidateMesh())
            {
                //if material is null, replace it with default
                if (meshMatt == null)
                {
                    //load default material for sprites
                    meshMatt = DefaultMaterialProvider.GetDefaultMaterial(true);
                }

                _Mesh = new Mesh();
                GetOrAddComponents();
                C_MR.material = meshMatt;

                BuildMeshComponents();
                UpdateMeshFilter();
                UpdateCollider();
            }
            else
            {
                Debug.LogError("Gear::BuildMesh: " + name + " generation failed");
            }
        }

        //update mesh in MeshFilter component
        public void UpdateMeshFilter()
        {
            _Mesh.Clear();
            _Mesh.vertices = vertices.ToArray();
            _Mesh.triangles = triangles.ToArray();
            _Mesh.uv = uvs;
            _Mesh.normals = GearHelper.AddGearNormals(vertices.Count);
            C_MF.mesh = _Mesh;
        }

        ///check if mesh parameters are valid
        protected bool ValidateMesh()
        {
            if (sides < 2)
            {
                Debug.LogWarning("GearMesh::BuildGear: sides count can't be less than two!");
                return false;
            }
            if (rootRadius == 0)
            {
                Debug.LogWarning("GearMesh::BuildGear: rootRadius can't be equal to zero!");
                return false;
            }
            if (outerRadius == 0)
            {
                Debug.LogWarning("GearMesh::BuildGear: outerRadius can't be equal to zero!");
                return false;
            }
            if (tipRadius == 0)
            {
                Debug.LogWarning("GearMesh::BuildGear: outerRadius can't be equal to zero!");
                return false;
            }
            if (innerRadius < 0)
            {
                innerRadius = -innerRadius;
            }
            if (rootRadius < 0)
            {
                rootRadius = -rootRadius;
            }
            if (innerRadius < 0)
            {
                outerRadius = -outerRadius;
            }
            if (tipRadius < 0)
            {
                tipRadius = -tipRadius;
            }
            return true;
        }

        //get vertices, triangles and UVs
        protected void BuildMeshComponents()
        {
            vertices = new List<Vector3>();
            triangles = new List<int>();

            float angleDelta = Mathf.PI / sides;
            float angleShift = angleDelta * 0.5f;
            float rootAngleShift = angleDelta * rootAngleShiftMultiplier;
            float outerAngleShift = angleDelta * outerAngleShiftMultiplier;
            float tipAngleShift = angleDelta * tipAngleShiftMultiplier;

            //inner vertices - {0, sides * 2 - 1}
            for (int i = 0; i < sides * 2; i++)
            {
                float angle = i * angleDelta + angleShift;
                vertices.Add(new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * innerRadius);
            }

            //root vertices - {sides * 2, sides * 4 - 1}
            for (int i = 0; i < sides * 2; i++)
            {
                float angle = i * angleDelta + angleShift + (i % 2 == 0 ? rootAngleShift : -rootAngleShift);
                vertices.Add(new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * rootRadius);
            }

            //inner-root triangles {0, sides * 4 - 1}
            for (int i = 0; i < sides * 2; i++)
            {
                triangles.Add(i);
                triangles.Add((i + 1) % (sides * 2));
                triangles.Add(i + sides * 2);

                triangles.Add((i + 1) % (sides * 2));
                triangles.Add(sides * 2 + (i + 1) % (sides * 2));
                triangles.Add(i + sides * 2);
            }

            //outer vertices - {sides * 4, sides * 6 - 1}
            for (int i = 0; i < sides; i++)
            {
                float angle = 2 * i * angleDelta + angleShift + outerAngleShift + teethSlant;
                vertices.Add(new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * outerRadius);
                angle = (2 * i + 1) * angleDelta + angleShift - outerAngleShift + teethSlant;
                vertices.Add(new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * outerRadius);
            }

            //root-outer triangles {sides * 4, sides * 6 - 1}
            for (int i = 0; i < sides; i++)
            {
                triangles.Add(2 * i + sides * 2);
                triangles.Add(2 * i + sides * 2 + 1);
                triangles.Add(2 * i + sides * 4);

                triangles.Add(2 * i + sides * 2 + 1);
                triangles.Add(2 * i + sides * 4 + 1);
                triangles.Add(2 * i + sides * 4);
            }

            //tip vertices - {sides * 6, sides * 8 - 1}
            for (int i = 0; i < sides; i++)
            {
                float angleLeft = 2 * i * angleDelta + angleShift + tipAngleShift + teethSlant * 2;
                vertices.Add(new Vector3(Mathf.Cos(angleLeft), Mathf.Sin(angleLeft)) * tipRadius);

                float angleRight = (2 * i + 1) * angleDelta + angleShift - tipAngleShift + teethSlant * 2;
                vertices.Add(new Vector3(Mathf.Cos(angleRight), Mathf.Sin(angleRight)) * tipRadius);
            }

            //outer-tip triangles {sides * 6, sides * 8 - 1}
            for (int i = 0; i < sides; i++)
            {
                triangles.Add(2 * i + sides * 4);
                triangles.Add(2 * i + sides * 4 + 1);
                triangles.Add(2 * i + sides * 6);

                triangles.Add(2 * i + sides * 4 + 1);
                triangles.Add(2 * i + sides * 6 + 1);
                triangles.Add(2 * i + sides * 6);
            }
            uvs = GearHelper.UVUnwrap(vertices);
        }

        //assign necessary components
        public void GetOrAddComponents()
        {
            C_EC2D = gameObject.GetOrAddComponent<PolygonCollider2D>();
            C_MR = gameObject.GetOrAddComponent<MeshRenderer>();
            C_MF = gameObject.GetOrAddComponent<MeshFilter>();
        }

        //update attached colliders
        public void UpdateCollider()
        {
            Vector2[] colliderPoints = new Vector2[8 * sides + 2];
            for (int i = 0; i < 2 * sides; i++)
            {
                colliderPoints[i] = vertices[i];
            }
            colliderPoints[2 * sides] = vertices[0];

            int c = 2 * sides + 1;
            for (int i = 0; i < sides; i++)
            {
                colliderPoints[c++] = vertices[(2 * sides + i * 2) % vertices.Count];
                colliderPoints[c++] = vertices[(4 * sides + i * 2) % vertices.Count];
                colliderPoints[c++] = vertices[(6 * sides + 2 * i) % vertices.Count];
                colliderPoints[c++] = vertices[(6 * sides + 1 + 2 * i) % vertices.Count];
                colliderPoints[c++] = vertices[(4 * sides + 1 + 2 * i) % vertices.Count];
                colliderPoints[c++] = vertices[(2 * sides + 1 + i * 2) % vertices.Count];
            }
            colliderPoints[c] = vertices[2 * sides];

            C_EC2D.points = colliderPoints;
        }

        #endregion

        #region Mesh material

        //set physics material properties
        public void SetPhysicsMaterialProperties(float bounciness, float friction)
        {
            PhysicsMaterial2D sharedMaterial = gameObject.GetComponent<Collider2D>().sharedMaterial;
            if (sharedMaterial == null)
            {
                sharedMaterial = new PhysicsMaterial2D(name + "_PhysicsMaterial2d");
                gameObject.GetComponent<Collider2D>().sharedMaterial = sharedMaterial;
            }
            sharedMaterial.bounciness = bounciness;
            sharedMaterial.friction = friction;
        }

        //set physics material properties
        public void SetPhysicsMaterial(PhysicsMaterial2D C_PS2D)
        {
            gameObject.GetComponent<Collider2D>().sharedMaterial = C_PS2D;
        }

        //set material to random color
        public void SetRandomColor()
        {
            C_MR.material.color = Random.ColorHSV();
        }

        //set color
        public void SetColor(Color color)
        {
            C_MR.material.color = color;
        }

        //set material
        public void SetMaterial(Material material)
        {
            C_MR.material = material;
        }

        //set material texture
        public void SetTexture(Texture texture)
        {
            C_MR.material.mainTexture = texture;
        }

        //set material and texture
        public void SetMaterial(Material material, Texture texture)
        {
            C_MR.material = material;
            C_MR.material.mainTexture = texture;
        }

        #endregion

        #region Joints

        //add HingeJoint2D at the center of the object and attach it to background
        public HingeJoint2D AddHingeJoint2D()
        {
            return AddHingeJoint2D(transform.position);
        }

        //add HingeJoint2D to the object and attach it to background
        public HingeJoint2D AddHingeJoint2D(Vector2 position)
        {
            HingeJoint2D C_HJ2D = gameObject.GetOrAddComponent<HingeJoint2D>();
            C_HJ2D.anchor = transform.InverseTransformPoint(position);
            return C_HJ2D;
        }

        //specify motor - by default on center
        public HingeJoint2D AddHingeJoint2D(JointMotor2D C_JM2D)
        {
            return AddHingeJoint2D(C_JM2D, transform.position);
        }

        //specify motor
        public HingeJoint2D AddHingeJoint2D(JointMotor2D C_JM2D, Vector2 position)
        {
            HingeJoint2D C_HJ2D = gameObject.GetOrAddComponent<HingeJoint2D>();
            C_HJ2D.anchor = transform.InverseTransformPoint(position);
            C_HJ2D.motor = C_JM2D;
            C_HJ2D.useMotor = true;
            return C_HJ2D;
        }

        //specify motor and connected body (on center, by default)
        public HingeJoint2D AddHingeJoint2D(JointMotor2D C_JM2D, Rigidbody2D connectedBody)
        {
            return AddHingeJoint2D(C_JM2D, connectedBody, transform.position);
        }

        //specify motor and connected body
        public HingeJoint2D AddHingeJoint2D(JointMotor2D C_JM2D, Rigidbody2D connectedBody, Vector2 position)
        {
            HingeJoint2D C_HJ2D = gameObject.GetOrAddComponent<HingeJoint2D>();
            C_HJ2D.anchor = transform.InverseTransformPoint(position);
            C_HJ2D.motor = C_JM2D;
            C_HJ2D.useMotor = true;
            C_HJ2D.connectedBody = connectedBody;
            return C_HJ2D;
        }

        //fix object to background
        public FixedJoint2D AddFixedJoint2D()
        {
            FixedJoint2D C_HJ2D = gameObject.GetOrAddComponent<FixedJoint2D>();
            C_HJ2D.anchor = transform.InverseTransformPoint(transform.position);
            return C_HJ2D;
        }

        #endregion

    }

}