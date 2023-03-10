using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMesh : MonoBehaviour
{
    [Range(3, 255)]
    public int N = 8;
    public int M = 8;

    public float Radius = 1.0f;
    private float TAU = 2 * Mathf.PI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MakeCircle();
        //JEbyu();
    }

    void MakePlane()
    {
        Mesh mesh = new Mesh();

        Vector3[] verts =
        {
            new Vector3(-1f,  1f, 0f),
            new Vector3( 1f,  1f, 0f),
            new Vector3(-1f, -1f, 0f),
            new Vector3( 1f,  -1f, 0f)
        };

        int[] tri_indices = { 0, 1, 2,
                              1, 3, 2};

        mesh.vertices = verts;
        mesh.SetTriangles(tri_indices, 0);
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().sharedMesh = mesh;

    }

    void MakeCircle()
    {
        Mesh mesh = new Mesh();

        // Circular mesh
        List<Vector3> verts = new List<Vector3>();
        verts.Add(Vector3.zero);
        //Vector3 v = Vector3.up * Radius;
        for (int i = 0; i < N; i++)
        {
            float theta = TAU * i / N; // angle of current iteration ( in radians)
            Debug.Log("Angle: " + theta);
            Vector3 v = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0);
            verts.Add(v * Radius);
        }

        mesh.SetVertices(verts);
        List<int> tri_indices = new List<int>();
        for (int i = 0; i < N - 1; i++)
        {
            tri_indices.Add(0);
            tri_indices.Add(i + 1);
            tri_indices.Add(i + 2);
        }
        tri_indices.Add(0);
        tri_indices.Add(N);
        tri_indices.Add(1);

        mesh.SetTriangles(tri_indices, 0);
        //mesh.RecalculateNormals();
        GetComponent<MeshFilter>().sharedMesh = mesh;
    }

    void SuperCircle()
    {
        Mesh mesh = new Mesh();

        List<Vector3> verts  = new List<Vector3>();

    }

    void JEbyu()
    {
        Mesh mesh = new Mesh();
        List<Vector3> verts = new List<Vector3>();
        verts.Add(Vector3.zero);

        List <int> tri_indices = new List<int>();
        for (int i = 0; i < N - 1; i++)
        {
            int InnerFirst = 2 * 1;
            int OuterFirst = InnerFirst + 1;
            int InnerSecond = OuterFirst + 1;
            int OuterSecond = InnerFirst + 1;

            tri_indices.Add((InnerFirst));
            tri_indices.Add((OuterFirst));
            tri_indices.Add((OuterSecond));

            tri_indices.Add((InnerFirst));
            tri_indices.Add((OuterSecond));
            tri_indices.Add((InnerSecond));
            
        }

        int InFirst = 2 * (N - 1);
        int OutFirst = InFirst + 1;
        int inSecond = 0;
        int outSecond = 1;

        tri_indices.Add((InFirst));
        tri_indices.Add(OutFirst);
        tri_indices.Add(inSecond);
        tri_indices.Add(outSecond);

        mesh.SetTriangles(tri_indices, 0);
        GetComponent<MeshFilter>().sharedMesh = mesh;

    }
}
