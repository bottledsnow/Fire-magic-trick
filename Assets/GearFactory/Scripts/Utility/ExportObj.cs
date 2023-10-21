using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

/// <summary>
/// Static helper for exporting .obj files
/// </summary>
public static class ExportObj
{
    private static readonly StringBuilder commonStringBuilder = new StringBuilder();

    public static void ExportToObj(Mesh mesh, string name, bool separateSharedVertices = true)
    {
        Mesh workMesh = Object.Instantiate(mesh);

        if (separateSharedVertices)
        {
            SeparateSharedVertices(workMesh);
        }

        ClearStringBuilder();
        WriteName(name);
        WriteVertices(workMesh.vertices);
        WriteTriangles(workMesh.triangles);
        WriteNormals(workMesh.normals);
        WriteUVs(workMesh.uv);

        File.WriteAllText("Assets/" + name + ".obj", commonStringBuilder.ToString());
        Debug.Log("The .obj file saved: Assets/" + name + ".obj");
    }

    //meshes build within GearFactory contains shared vertices - get rid of them
    public static void SeparateSharedVertices(Mesh mesh)
    {
        List<Vector3> newVertices = new List<Vector3>(mesh.triangles.Length);
        List<int> newTriangles = new List<int>();
        for (int i = 0; i < mesh.triangles.Length; i++)
        {
            newVertices.Add(mesh.vertices[mesh.triangles[i]]);
            newTriangles.Add(i);
        }
        mesh.vertices = newVertices.ToArray();
        mesh.triangles = newTriangles.ToArray();
        mesh.RecalculateNormals();
    }

    //reset {commonStringBuilder}
    private static void ClearStringBuilder()
    {
        commonStringBuilder.Length = 0;
        commonStringBuilder.Capacity = 0;
    }

    #region Writing To StringBuilder
    
    //write "g {name}"
    private static void WriteName(string name)
    {
        commonStringBuilder.Append("g ").Append(name).Append("\n");
    }

    //write "v {x} {y} {z} ..."
    private static void WriteVertices(IList<Vector3> vertices)
    {
        for (int i = 0; i < vertices.Count; i++)
        {
            commonStringBuilder.Append('v')
                .Append(' ').Append(vertices[i].x)
                .Append(' ').Append(vertices[i].y)
                .Append(' ').Append(vertices[i].z)
                .Append('\n');
        }
    }

    //write "f {t}/{t}/{t} {t}/{t}/{t}..."
    private static void WriteTriangles(IList<int> triangles)
    {
        for (int i = 0; i < triangles.Count; i += 3)
        {
            commonStringBuilder.Append('f')
                .Append(' ')
                .Append(triangles[i] + 1).Append('/')
                .Append(triangles[i] + 1).Append('/')
                .Append(triangles[i] + 1)
                .Append(' ')
                .Append(triangles[i + 1] + 1).Append('/')
                .Append(triangles[i + 1] + 1).Append('/')
                .Append(triangles[i + 1] + 1)
                .Append(' ')
                .Append(triangles[i + 2] + 1).Append('/')
                .Append(triangles[i + 2] + 1).Append('/')
                .Append(triangles[i + 2] + 1)
                .Append('\n');
        }
    }

    //write "vn {x} {y} {z}..."
    private static void WriteNormals(IList<Vector3> normals)
    {
        for (int i = 0; i < normals.Count; i++)
        {
            commonStringBuilder.Append("vn")
                .Append(' ').Append(normals[i].x)
                .Append(' ').Append(normals[i].y)
                .Append(' ').Append(normals[i].z)
                .Append('\n');
        }
    }

    //write "vt {x} {y} ..."
    private static void WriteUVs(IList<Vector2> uvs)
    {
        for (int i = 0; i < uvs.Count; i++)
        {
            commonStringBuilder.Append("vt")
                .Append(' ').Append(uvs[i].x)
                .Append(' ').Append(uvs[i].y)
                .Append('\n');
        }
    }

    #endregion
}