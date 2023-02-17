using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Gizmo : MonoBehaviour
{
    public GameObject A, B;
    public float scalarDot;
    public Vector3 startX = new Vector3(2, 0, 0);
    public Vector3 endX =   new Vector3(-2, 0, 0);
    public Vector3 endY = new Vector3(0, -2, 0);
    public Vector3 startY = new Vector3(0, 2, 0);

    private void OnDrawGizmos()
    {
        Handles.DrawWireDisc(new Vector3(0, 0, 0), Vector3.forward, 1);
        DrawVector(startX, endX, Color.red);
        DrawVector(startY, endY, Color.green);
        DrawVector(Vector3.zero, A.transform.position, Color.black);

        DrawVector(Vector3.zero, B.transform.position, Color.black);

        // Compute the dot procut
        Vector2 VecA = A.transform.position;
        Vector2 VecB = B.transform.position;

        // Draw normalized vector

        float vecAlen = VecA.magnitude;
        float vecBlen = VecB.magnitude;
        Vector2 vecNa = VecA / vecAlen;
        Vector2 vecNB = VecB / vecBlen;
        DrawVector(new Vector3(0, 0, 0), vecNa, Color.blue);
        DrawVector(Vector3.zero, vecNB, Color.cyan);

        scalarDot = Vector2.Dot(vecNa, vecNB);
        // Vector projection Dot(vecN, vecB * vecN;
        Vector2 vecProj = vecNa * scalarDot;
        DrawVector(Vector3.zero, vecProj, Color.magenta);
        

    }
    private void DrawVector(Vector3 from, Vector3 to, Color c)
    {
        Color curr = Gizmos.color;
        Gizmos.color = c;
        Gizmos.DrawLine(from, to);
        Vector3 loc = -(to - from);
        loc = Vector3.ClampMagnitude(loc, 0.1f);
        Quaternion rot30 = Quaternion.Euler(0, 0, 30);
        Vector3 loc1 = rot30 * loc;
        rot30 = Quaternion.Euler(0, 0, -30);
        Vector3 loc2 = rot30 * loc;
        Gizmos.DrawLine(to, to + loc1);
        Gizmos.DrawLine(to, to + loc2);
        Gizmos.color = curr;
    }
}
