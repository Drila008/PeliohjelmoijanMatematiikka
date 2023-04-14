using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BezierPath : MonoBehaviour
{

    [SerializeField]
    public Mesh2D road2D;
    public BezierPoint[] points;
    [Range(0.0f, 1.0f)]
    public float TValue;

    private void OnDrawGizmos()
    {
        for(int i = 0; i < points.Length - 1; i++)
        {
            Handles.DrawBezier(points[i].Anchor.position,
                    points[i + 1].Anchor.position,
                    points[i].control1.position,
                    points[i + 1].control0.position,
                    Color.magenta, default, 2f);
        }
        //Handles.DrawBezier(points[points.Length - 1].Anchor.position,
        //    points[0].Anchor.position,
        //    points[points.Length - 1].control1.position,
        //    points[0].control0.position,
        //    Color.magenta, default, 2f);

        Vector3 tPos = GetBezierPosition(TValue, points[0], points[1]);
        Vector3 tDir = GetBezierDir(TValue, points[0], points[1]);


        Gizmos.color = Color.red;
        Gizmos.DrawSphere(tPos, 0.10f);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(tPos, tPos + 6f * tDir);

        Quaternion rot = Quaternion.LookRotation(tDir);
        Handles.PositionHandle(tPos, rot);

        int currIndex = 0;
        foreach (var point in points)
        {
            if (currIndex == points.Length - 1)
                break;

            Vector3 tempDir = GetBezierDir(TValue, points[currIndex], points[currIndex + 1]);
            Quaternion rot1 = Quaternion.LookRotation(tempDir);
            Vector3 tempPos = GetBezierPosition(TValue, points[currIndex], points[currIndex + 1]);
            Vector3 nextPos = GetBezierPosition(TValue, points[currIndex + 1], points[currIndex + 1]);
            for (int i = 0; i < road2D.vertices.Length; i++)
            {
                Vector3 roadPoint = road2D.vertices[i].point;
                Gizmos.DrawSphere(tempPos + rot1 * roadPoint, 0.25f);
                if (i < road2D.vertices.Length - 1)
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(tempPos + rot1 * roadPoint, tempPos + rot1 * road2D.vertices[i + 1].point);
                    Gizmos.DrawLine(tempPos + rot1 * road2D.vertices[road2D.vertices.Length - 1].point, tempPos + rot1 * road2D.vertices[0].point);
                    //Gizmos.color = Color.blue;
                }

                Gizmos.DrawLine(tempPos + rot1 * roadPoint, nextPos + rot1 * roadPoint);
            }
            Gizmos.color = Color.red;

            currIndex++;
            //Debug.Log(currIndex);
        }
        currIndex = 0;
        GenerateRoadMesh();
    }

    //private void GenerateRoadMesh()
    //{
    //    // Create a new mesh
    //    Mesh mesh = new Mesh();
    //    GetComponent<MeshFilter>().mesh = mesh;

    //    // Create arrays to hold the vertices and triangles
    //    Vector3[] vertices = new Vector3[points.Length * road2D.vertices.Length];
    //    int[] triangles = new int[(points.Length - 1) * (road2D.vertices.Length - 1) * 6];

    //    // Fill the vertices array
    //    int index = 0;
    //    for (int i = 0; i < points.Length - 1; i++)
    //    {
    //        Quaternion rot = Quaternion.LookRotation(GetBezierDir(TValue, points[i], points[i + 1]));
    //        foreach (var vertex in road2D.vertices)
    //        {
    //            if (index >= vertices.Length)
    //            {
    //                break;
    //            }
    //            vertices[index] = points[i].transform.position + rot * vertex.point;
    //            index++;
    //        }
    //    }

    //    // Fill the triangles array
    //    index = 0;
    //    for (int i = 0; i < points.Length - 1; i++)
    //    {
    //        for (int j = 0; j < road2D.vertices.Length - 1; j++)
    //        {
    //            int a = i * road2D.vertices.Length + j;
    //            int b = i * road2D.vertices.Length + j + 1;
    //            int c = (i + 1) * road2D.vertices.Length + j;
    //            int d = (i + 1) * road2D.vertices.Length + j + 1;

    //            triangles[index] = a;
    //            triangles[index + 1] = c;
    //            triangles[index + 2] = b;

    //            triangles[index + 3] = b;
    //            triangles[index + 4] = c;
    //            triangles[index + 5] = d;

    //            index += 6;
    //        }
    //    }

    //    // Set the vertices and triangles of the mesh
    //    mesh.vertices = vertices;
    //    mesh.triangles = triangles;

    //    // Recalculate the normals and bounds of the mesh
    //    mesh.RecalculateNormals();
    //    mesh.RecalculateBounds();
    //}
    Vector3 GetBezierPosition(float t, BezierPoint bp1, BezierPoint bp2)
    {
        // 1st lerp
        Vector3 PtX = (1 - t) * bp1.Anchor.position + t * bp1.control1.position;
        Vector3 PtY = (1 - t) * bp1.control1.position + t * bp2.control0.position;
        Vector3 PtZ = (1 - t) * bp2.control0.position + t * bp2.Anchor.position;
        // 2nd lerp:
        Vector3 PtR = (1 - t) * PtX + t * PtY;
        Vector3 PtS = (1 - t) * PtY + t * PtZ;

        return (1 - t) * PtR + t * PtS;

    }

    Vector3 GetBezierDir(float t, BezierPoint bp1, BezierPoint bp2)
    {
        // 1st lerp
        Vector3 PtX = (1 - t) * bp1.Anchor.position + t * bp1.control1.position;
        Vector3 PtY = (1 - t) * bp1.control1.position + t * bp2.control0.position;
        Vector3 PtZ = (1 - t) * bp2.control0.position + t * bp2.Anchor.position;
        // 2nd lerp:
        Vector3 PtR = (1 - t) * PtX + t * PtY;
        Vector3 PtS = (1 - t) * PtY + t * PtZ;

        return (PtS - PtR).normalized;

    }
    public void GenerateRoadMesh()
    {
        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        int segmentCount = 10;
        int vertexCount = segmentCount * road2D.vertices.Length;

        for (int i = 0; i < segmentCount; i++)
        {
            float t = (float)i / (float)segmentCount;
            int currIndex = 0;
            foreach (var point in points)
            {
                if (currIndex == points.Length - 1)
                    break;

                Vector3 tempDir = GetBezierDir(t, points[currIndex], points[currIndex + 1]);
                Quaternion rot1 = Quaternion.LookRotation(tempDir);
                Vector3 tempPos = GetBezierPosition(t, points[currIndex], points[currIndex + 1]);
                foreach (var vertex in road2D.vertices)
                {
                    vertices.Add(tempPos + rot1 * vertex.point);
                }
                currIndex++;
            }
        }

        int vertexIndex = 0;
        for (int i = 0; i < segmentCount - 1; i++)
        {
            for (int j = 0; j < road2D.vertices.Length - 1; j++)
            {
                int topLeft = vertexIndex + j;
                int topRight = vertexIndex + j + 1;
                int bottomLeft = vertexIndex + j + road2D.vertices.Length;
                int bottomRight = vertexIndex + j + road2D.vertices.Length + 1;
                triangles.Add(topLeft);
                triangles.Add(bottomLeft);
                triangles.Add(topRight);
                triangles.Add(topRight);
                triangles.Add(bottomLeft);
                triangles.Add(bottomRight);
            }
            vertexIndex += road2D.vertices.Length;
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }
}
