using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrossProduct : MonoBehaviour
{
    public GameObject a, b;

    private void OnDrawGizmos()
    {
        // Draw axes
        DrawVector(transform.position, transform.position + transform.right, Color.red);
        DrawVector(transform.position, transform.position + transform.up, Color.green);

        // Draw a and b
        DrawVector(transform.position, a.transform.position, Color.white);
        DrawVector(transform.position, b.transform.position, Color.white);

        // normalize a and b vectors
        Vector3 a_hat = (a.transform.position - transform.position).normalized;
        Vector3 b_hat = (b.transform.position - transform.position).normalized;

        Vector3 cross_prod = Vector3.Cross(a_hat, b_hat);
        DrawVector(transform.position, transform.position + cross_prod, Color.magenta);
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
