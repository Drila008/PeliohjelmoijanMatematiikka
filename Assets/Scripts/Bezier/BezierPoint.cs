using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BezierPoint : MonoBehaviour
{
    //public Transform anchor;
    public Transform control0;
    public Transform control1;

    public bool tangentLock = false;
    public Transform Anchor { get { return gameObject.transform; } }


    public void OnDrawGizmos()
    {
        //DrawVector(transform.position, control0.position, Color.green);
        //DrawVector(transform.position, control1.position, Color.red);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, control0.position);
        Gizmos.DrawLine(transform.position, control1.position);

        if(tangentLock)
        {
            control1.transform.localPosition = -control0.transform.localPosition;
        }

        //Handles.DrawBezier
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
