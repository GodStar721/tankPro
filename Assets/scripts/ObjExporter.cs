using UnityEngine;
using System.Collections.Generic;
using System.IO;

public static class ObjExporter
{
    public static void MeshToFile(Mesh mesh, Material[] materials, string filePath)
    {
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            sw.WriteLine("g " + mesh.name);

            // Export vertices
            foreach (Vector3 v in mesh.vertices)
                sw.WriteLine("v " + v.x + " " + v.y + " " + v.z);

            // Export UVs
            foreach (Vector2 uv in mesh.uv)
                sw.WriteLine("vt " + uv.x + " " + uv.y);

            // Export normals
            foreach (Vector3 n in mesh.normals)
                sw.WriteLine("vn " + n.x + " " + n.y + " " + n.z);

            // Export submeshes
            for (int materialIndex = 0; materialIndex < mesh.subMeshCount; materialIndex++)
            {
                int[] triangles = mesh.GetTriangles(materialIndex);
                Material material = materials[materialIndex];

                sw.WriteLine("usemtl " + material.name);

                for (int i = 0; i < triangles.Length; i += 3)
                {
                    int triangleIndex0 = triangles[i] + 1;
                    int triangleIndex1 = triangles[i + 1] + 1;
                    int triangleIndex2 = triangles[i + 2] + 1;

                    sw.WriteLine("f " + triangleIndex0 + "/" + triangleIndex0 + "/" + triangleIndex0 +
                                 " " + triangleIndex1 + "/" + triangleIndex1 + "/" + triangleIndex1 +
                                 " " + triangleIndex2 + "/" + triangleIndex2 + "/" + triangleIndex2);
                }
            }
        }
        
        Debug.Log("Mesh exported to: " + filePath);
    }
}
