using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mun : MonoBehaviour
{
    [Range(3, 255)]
    public int N = 8;
    public int M = 8;
    public int R = 2;

    public float Radius = 1.0f;
    private float TAU = 2 * Mathf.PI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //MakeCircle();
        MakeDisc();
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


    void MakeDisc()
    {
        Mesh mesh = new Mesh();
        List<Vector3> verts = new List<Vector3>();

        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < N; i++)
        {
            float theta = TAU * i / N; // angle of current iteration ( in radians)
            Vector3 v = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0);
            verts.Add(v * Radius * R); // Inner points
            verts.Add(v * Radius * M); // Outer points

            Vector2 mid = new Vector2(0.5f, 0.5f);
            Vector2 s = v * 0.5f;
            uvs.Add(mid + s * (Radius / R));
            uvs.Add(mid + s);

            uvs.Add(new Vector2(i / (float)N, 0));
            uvs.Add(new Vector2(i / (float)N, 1));
        }

        List<int> tri_indices = new List<int>();
        for (int i = 0; i < N - 1; i++)
        {
            int InnerFirst = 2 * i;
            int OuterFirst = InnerFirst + 1;
            int InnerSecond = OuterFirst + 1;
            int OuterSecond = InnerSecond + 1;

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

        //tri_indices.Add(outSecond);
        //tri_indices.Add(inSecond);
        //tri_indices.Add(OutFirst);

        //tri_indices.Add(InFirst);
        //tri_indices.Add(OutFirst);
        //tri_indices.Add(inSecond);
        tri_indices.Add(InFirst);
        tri_indices.Add(OutFirst);
        tri_indices.Add(outSecond);

        // Second triangle
        tri_indices.Add(InFirst);
        tri_indices.Add(outSecond);
        tri_indices.Add(inSecond);

        mesh.SetVertices(verts);
        mesh.SetTriangles(tri_indices, 0);
        mesh.RecalculateNormals();
        mesh.SetUVs(0, uvs);
        GetComponent<MeshFilter>().sharedMesh = mesh;
    }
}
