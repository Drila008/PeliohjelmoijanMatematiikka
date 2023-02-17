using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalToWorld : MonoBehaviour
{

    public GameObject parent;
    public float localX = 0.0f;
    public float localY = 0.0f;

    private void OnDrawGizmos()
    {
        // Local to world
        Vector2 worldPoint = transform.position + localX * transform.right + localY * transform.up;

        // world axes
        DrawVector(Vector3.zero, new Vector3(3, 0, 0), Color.red);
        DrawVector(Vector3.zero, new Vector3(0,3, 0), Color.green);

        // local axes
        DrawVector(transform.position, transform.position + transform.right, Color.red);
        DrawVector(transform.position, transform.position + transform.up, Color.green);

        //Debug.Log(transform.localToWorldMatrix.ToString());

        Gizmos.color = Color.black;
        Gizmos.DrawSphere(worldPoint, 0.05f);
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
