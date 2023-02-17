using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Rectangle : MonoBehaviour
{
    public float x, y;
    public Vector3 pos = Vector3.zero;

    // Start is called before the first frame update
    private void OnDrawGizmos()
    {
        DrawBox(pos, x, y);
        DrawBox(pos, x * 0.75f, y * 0.75f);
    }

    void DrawBox(Vector3 pos, float x, float y)
    {
        Handles.DrawWireCube(pos, new Vector3(x, y, 0));
    }

    



}
