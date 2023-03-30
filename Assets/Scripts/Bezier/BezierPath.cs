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
                    Gizmos.DrawLine(tempPos + rot1 * road2D.vertices[road2D.vertices.Length-1].point, tempPos + rot1 * road2D.vertices[0].point);
                    //Gizmos.color = Color.blue;
                }

                Gizmos.DrawLine(tempPos + rot1 * roadPoint, nextPos + rot1 * roadPoint);
            }
            Gizmos.color = Color.red;

            currIndex++;
            //Debug.Log(currIndex);
        }
        currIndex = 0;
    }

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
}
