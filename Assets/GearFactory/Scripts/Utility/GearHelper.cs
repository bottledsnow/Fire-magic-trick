using System;
using System.Collections.Generic;
using UnityEngine;

namespace GearFactory
{
    /// <summary>
    /// Collection of useful and reusable methods.
    /// </summary>
    public static class GearHelper
    {
        //returns component of given type, adding it in case it's not already attached
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            //DON'T use ?? operator
            T c = gameObject.GetComponent<T>();
            if (c == null)
            {
                c = gameObject.gameObject.AddComponent<T>();
            }
            return c;
        }

        //in case of sprites, all normals can be just {Vector3.up}
        public static Vector3[] AddGearNormals(int verticesLength)
        {
            Vector3[] normals = new Vector3[verticesLength];
            for (int i = 0; i < verticesLength; i++)
            {
                normals[i] = Vector3.up;
            }
            return normals;
        }

        public static Vector2 SetVectorLength(Vector2 vector, float length)
        {
            return vector.normalized * length;
        }

        public static void DrawArrowGizmos(Transform from, Transform to, Color color, float relativeArrowLength = 0.15f)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(from.position, to.position);
        }

        #region UV Unwrapping

        //get bounding box of supplied points
        private static Vector4 GetBounds(IList<Vector3> vec)
        {
            /* x - minX
             * y - minY
             * z - maxX
             * w - maxY
             */
            float x = Single.MaxValue;
            float y = Single.MaxValue;
            float z = Single.MinValue;
            float w = Single.MinValue;
            for (int i = 0; i < vec.Count; i++)
            {
                if (vec[i].x < x)
                {
                    x = vec[i].x;
                }
                if (vec[i].y < y)
                {
                    y = vec[i].y;
                }
                if (vec[i].x > z)
                {
                    z = vec[i].x;
                }
                if (vec[i].y > w)
                {
                    w = vec[i].y;
                }
            }
            return new Vector4(x, y, z, w);
        }

        //return array of Vector2 UVs
        public static Vector2[] UVUnwrap(IList<Vector3> vertices)
        {
            Vector2[] uv = new Vector2[vertices.Count];
            Vector4 boundingBox = GetBounds(vertices);
            float length = boundingBox.z - boundingBox.x;
            float width = boundingBox.w - boundingBox.y;
            for (int i = 0; i < vertices.Count; i++)
            {
                float ux = (vertices[i].x - boundingBox.x) / length;
                float uy = (vertices[i].y - boundingBox.y) / width;
                uv[i] = new Vector2(ux, uy);
            }
            return uv;
        }

        #endregion
    }

}