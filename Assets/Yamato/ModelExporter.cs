using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine.ProBuilder;

public class ModelExporter : MonoBehaviour
{
    [MenuItem("Tools/Export Objects as OBJ Files")]
    public static void ExportModelParts()
    {
        GameObject model = Selection.activeGameObject;
        if (model != null)
        {
            MeshFilter[] meshFilters = model.GetComponentsInChildren<MeshFilter>();

            foreach (MeshFilter filter in meshFilters)
            {
                string modelName = filter.gameObject.name;
                Mesh mesh = filter.sharedMesh;

                // Create a new game object to hold the individual model part
                GameObject partObject = new GameObject(modelName);

                // Attach the mesh filter and renderer components to the new game object
                MeshFilter partFilter = partObject.AddComponent<MeshFilter>();
                MeshRenderer partRenderer = partObject.AddComponent<MeshRenderer>();
                partFilter.sharedMesh = mesh;
                partRenderer.sharedMaterial = filter.GetComponent<MeshRenderer>().sharedMaterial;

                // Export the individual model part as an obj file
                string filePath = Application.dataPath + "/" + modelName + ".obj";
                ObjExporter.MeshToFile(partFilter.sharedMesh, filePath);

                // Destroy the temporary game object
                GameObject.DestroyImmediate(partObject);
            }

            Debug.Log("Model parts exported successfully.");
        }
    }
}
