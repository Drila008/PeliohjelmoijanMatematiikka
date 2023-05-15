using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlane : MonoBehaviour {

    [Range(1.0f, 1000.0f)]
    public float Size = 10.0f;

    [Range(2, 255)]
    public int Segments = 5;

    public float NoiseValue = 1.0f;
    public float FirstDiv, SecondDiv, ThirdDiv;
    public float AmplitudeFirst, AmplitudeSecond, AmplitudeThird;

    private Mesh mesh = null;

    private void OnEnable() {
        if (mesh == null) {
            mesh = new Mesh();
            gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
        }

        GenerateMesh();
    }

    private void GenerateMesh() {
        mesh.Clear();
        // vertices
        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();

        // Delta between segments
        float delta = Size / (float)Segments;
        
        // Generate the vertices
        float x = 0.0f;
        float y = 0.0f;

        for (int seg_x = 0; seg_x <= Segments; seg_x++) {
            x = (float)seg_x * delta;
            for (int seg_y = 0; seg_y <= Segments; seg_y++) 
            {
                float z1 = (Mathf.PerlinNoise((x / FirstDiv), (y / FirstDiv)) * AmplitudeFirst);
                float z2 = (Mathf.PerlinNoise((x / SecondDiv), (y / SecondDiv)) * AmplitudeSecond);
                float z3 = (Mathf.PerlinNoise((x / ThirdDiv), (y / ThirdDiv)) * AmplitudeThird);
                float z = z1 + z2 + z3;
                y = (float)seg_y * delta;
                verts.Add(new Vector3(x, z, y));
            }
        }

        // Generate the triangle indices
        for (int seg_x = 0; seg_x < Segments; seg_x++) {
            for (int seg_y = 0; seg_y < Segments; seg_y++) {
                // Total count of vertices per row & col is: Segments + 1
                int index = seg_x * (Segments+1) + seg_y;
                int index_lower = index + 1;
                int index_next_col = index + (Segments+1);
                int index_next_col_lower = index_next_col + 1;
                
                tris.Add(index);
                tris.Add(index_lower);
                tris.Add(index_next_col);

                tris.Add(index_next_col);
                tris.Add(index_lower);
                tris.Add(index_next_col_lower);
            }
        }
        
        mesh.SetVertices(verts);
        mesh.SetTriangles(tris, 0);
        mesh.RecalculateNormals();
    }

    private void OnValidate() {
        if (mesh == null) {
            mesh = new Mesh();
            gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
        }

        GenerateMesh();
    }
}
