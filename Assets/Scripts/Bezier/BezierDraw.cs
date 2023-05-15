using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BezierDraw : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public Transform tangent1;
    public Transform tangent2;

    private void OnDrawGizmos()
    {
       // Handles.DrawBezier(point1.position, point2.position, tangent1.position, tangent2.position, Color.green, default, 3f);



    }
}
