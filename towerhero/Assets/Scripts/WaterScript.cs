using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{

    public int Size;
    public float Height;

    void Start()
    {
        transform.position = new Vector3(93, -0.7f, -42.4f);
        transform.localScale = new Vector3(2, 1, 3);

        if (gameObject.GetComponent<MeshFilter>() == null)
        {
            MeshFilter cubeMesh = gameObject.AddComponent<MeshFilter>();
            Mesh mesh = cubeMesh.mesh;
            updateMesh(mesh, Size, Height);

            MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = mesh;
        }
        else
        {
            MeshFilter cubeMesh = gameObject.GetComponent<MeshFilter>();
            Mesh mesh = cubeMesh.mesh;
            updateMesh(mesh, Size, Height);
            MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
            meshCollider.sharedMesh = mesh;
        }

        if (gameObject.GetComponent<MeshRenderer>() == null)
        {
            MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
            renderer.material.shader = Shader.Find("WaterPhongShader");
        }
    }

    void updateMesh(Mesh mesh, int dimension, float height)
    {

        // transform 2D array into 1D array of vertices
        Vector3[] vertices = new Vector3[(dimension + 1) * (dimension + 1)];
        Color32[] colors = new Color32[vertices.Length];
        for (int i = 0, z = 0; z <= dimension; z++)
        {
            for (int x = 0; x <= dimension; x++, i++)
            {
                vertices[i] = new Vector3(x, height, z);
                colors[i] = new Color32(64, 164, 223,20);
            }
        }

        mesh.vertices = vertices;
        mesh.colors32 = colors;

        // Turn each quad into two triangles
        // like the below
        // ______
        // |\4  5|
        // |1\   |
        // |  \  |
        // |   \3|
        // |    \|
        // |0___2|

        // ti == triangle index
        // vi == vertices index
        int[] triangles = new int[vertices.Length * 6];
        for (int ti = 0, vi = 0, z = 0; z < dimension; z++, vi++)
        {
            for (int x = 0; x < dimension; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 1] = vi + dimension + 1;
                triangles[ti + 2] = vi + 1;

                triangles[ti + 3] = vi + 1;
                triangles[ti + 4] = vi + dimension + 1;
                triangles[ti + 5] = vi + dimension + 2;
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

    }
}
