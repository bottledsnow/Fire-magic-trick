using System.Collections.Generic;
using UnityEngine;

namespace GearFactory
{
    /// <summary>
    /// Create 3D mesh from gear mesh sprite.
    /// </summary>
    public static class Gear3DConversion
    {
        public static Mesh Convert(Mesh mesh, int sides, float thickness)
        {
            List<Vector3> vertices = new List<Vector3>(mesh.vertices);
            List<int> triangles = new List<int>(mesh.triangles);

            DoubleAndSetupVertices(vertices, thickness * 0.5f);
            DoubleAndFlipTriangles(triangles, (int)(vertices.Count * 0.5f));
            SetupTriangles(triangles, sides, (int)(vertices.Count * 0.5f));

            mesh.vertices = GetSeparatedVertices(vertices, triangles);
            mesh.triangles = GetSeparatedTriangles(triangles);

            mesh.normals = new Vector3[mesh.vertices.Length];
            mesh.RecalculateNormals();

            return mesh;
        }

        private static int[] GetSeparatedTriangles(IList<int> triangles)
        {
            List<int> newTriangles = new List<int>();
            for (int i = 0; i < triangles.Count; i++)
            {
                newTriangles.Add(i);
            }

            return newTriangles.ToArray();
        }

        private static Vector3[] GetSeparatedVertices(IList<Vector3> vertices, IList<int> triangles)
        {
            List<Vector3> newVertices = new List<Vector3>(triangles.Count);
            for (int i = 0; i < triangles.Count; i++)
            {
                newVertices.Add(vertices[triangles[i]]);
            }
            return newVertices.ToArray();
        }

        private static void SetupTriangles(IList<int> triangles, int sides, int verticesCountHalved)
        {

            #region Inner triangles

            for (int i = 0; i < sides * 2; i++)
            {
                triangles.Add((i + 1) % (sides * 2));
                triangles.Add(i);
                triangles.Add(verticesCountHalved + (i + 1) % (sides * 2));
                triangles.Add(i);
                triangles.Add(verticesCountHalved + i);
                triangles.Add(verticesCountHalved + (i + 1) % (sides * 2));
            }

            #endregion

            #region Root triangles

            for (int i = sides * 2 + 1; i < sides * 4 + 1; i += 2)
            {
                triangles.Add(i);
                int index = i + 1;
                if (index >= sides * 4)
                {
                    index -= sides * 2;
                }
                triangles.Add(index);
                triangles.Add(verticesCountHalved + i);
                triangles.Add(verticesCountHalved + i);
                triangles.Add(index);
                triangles.Add(verticesCountHalved + index);
            }

            #endregion

            #region Root-outer triangles

            for (int i = sides * 2; i < sides * 4; i += 2)
            {
                triangles.Add(i);
                triangles.Add(i + sides * 2);
                triangles.Add(verticesCountHalved + i);

                triangles.Add(verticesCountHalved + sides * 2 + i);
                triangles.Add(verticesCountHalved + i);
                triangles.Add(sides * 2 + i);
            }

            for (int i = sides * 2 + 1; i < sides * 4 + 1; i += 2)
            {

                int specialIndex = i;
                if (specialIndex >= sides * 4)
                {
                    specialIndex -= sides * 4;
                    triangles.Add(i);
                    triangles.Add(sides * 2 + specialIndex);
                    triangles.Add(verticesCountHalved + sides * 2 + specialIndex);
                    triangles.Add(verticesCountHalved + i);
                    triangles.Add(i);
                    triangles.Add(verticesCountHalved + 2 * sides + specialIndex);
                }
                else
                {
                    triangles.Add(i + sides * 2);
                    triangles.Add(i);
                    triangles.Add(verticesCountHalved + sides * 2 + i);
                    triangles.Add(i);
                    triangles.Add(verticesCountHalved + i);
                    triangles.Add(verticesCountHalved + 2 * sides + i);
                }
            }

            #endregion

            #region Outer-tip triangles

            for (int i = sides * 4; i < sides * 6; i += 2)
            {
                triangles.Add(i);
                triangles.Add(i + sides * 2);
                triangles.Add(verticesCountHalved + i);

                triangles.Add(verticesCountHalved + sides * 2 + i);
                triangles.Add(verticesCountHalved + i);
                triangles.Add(sides * 2 + i);
            }

            for (int i = sides * 4 + 1; i < sides * 6 + 1; i += 2)
            {

                int specialIndex = i;
                if (specialIndex >= sides * 6)
                {
                    specialIndex -= sides * 6;
                    triangles.Add(i);
                    triangles.Add(sides * 2 + specialIndex);
                    triangles.Add(verticesCountHalved + sides * 2 + specialIndex);
                    triangles.Add(verticesCountHalved + i);
                    triangles.Add(i);
                    triangles.Add(verticesCountHalved + 2 * sides + specialIndex);
                }
                else
                {
                    triangles.Add(i + sides * 2);
                    triangles.Add(i);
                    triangles.Add(verticesCountHalved + sides * 2 + i);
                    triangles.Add(i);
                    triangles.Add(verticesCountHalved + i);
                    triangles.Add(verticesCountHalved + 2 * sides + i);
                }
            }

            #endregion

            #region Tip triangles

            for (int i = sides * 6; i < sides * 8; i += 2)
            {
                int specialIndex = i + 1;
                if (specialIndex > sides * 8)
                {
                    specialIndex -= sides * 2;
                }
                triangles.Add(i);
                triangles.Add(specialIndex);
                triangles.Add(verticesCountHalved + i);
                triangles.Add(specialIndex);
                triangles.Add(verticesCountHalved + i + 1);
                triangles.Add(verticesCountHalved + i);

            }

            #endregion
        }

        //double triangles (for both sides) and flip the other side
        private static void DoubleAndFlipTriangles(List<int> triangles, int verticesCountHalved)
        {
            int oldTrianglesCount = triangles.Count;
            triangles.AddRange(triangles);
            for (int i = oldTrianglesCount; i < triangles.Count; i++)
            {
                triangles[i] += verticesCountHalved;
            }
            for (int i = oldTrianglesCount; i < triangles.Count; i += 3)
            {
                int t = triangles[i];
                triangles[i] = triangles[i + 1];
                triangles[i + 1] = t;
            }
        }

        //double vertices (for both sides)
        private static void DoubleAndSetupVertices(List<Vector3> vertices, float halfThickness)
        {
            int oldVerticesCount = vertices.Count;
            vertices.AddRange(vertices);
            for (int i = 0; i < oldVerticesCount; i++)
            {
                vertices[i] += Vector3.back * halfThickness;
            }
            for (int i = oldVerticesCount; i < vertices.Count; i++)
            {
                vertices[i] += Vector3.forward * halfThickness;
            }
        }
    }
}
