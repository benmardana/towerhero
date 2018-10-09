using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshMaker : MonoBehaviour {
	
	public int xSize = 10;
	public int zSize = 10;
	
	void Start() {
		MeshFilter planeMesh = this.gameObject.AddComponent<MeshFilter>();
		planeMesh.mesh = this.CreatePlaneMesh();
		MeshRenderer renderer = this.gameObject.AddComponent<MeshRenderer>();
		renderer.material.shader = Shader.Find("Polybrush/Standard Texture Blend Bump");
		
	}
	
	Mesh CreatePlaneMesh()
	{
		Mesh m = new Mesh();
		m.name = "Plane";

		// Define the vertices. 
		Vector3[] vertices = new Vector3[(xSize + 1) * (zSize + 1)];
		for (int i = 0, z = 0; z <= zSize; z++)
		{
			for (int x = 0; x <= zSize; x++, i++)
			{
				vertices[i] = new Vector3(x, 0.0f, z);
			}
		}
		m.vertices = vertices;

		// Automatically define the triangles based on the number of vertices
		int[] triangles = new int[xSize * zSize * 6];
		for (int ti = 0, vi = 0, y = 0; y < zSize; y++, vi++)
		{
			for (int x = 0; x < xSize; x++, ti += 6, vi++)
			{
				triangles[ti] = vi;
				triangles[ti + 3] = triangles[ti + 2] = vi + 1;
				triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
				triangles[ti + 5] = vi + xSize + 2;
			}
		}

		m.triangles = triangles;

		return m;
	}
}