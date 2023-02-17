using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldToLocal : MonoBehaviour
{
    public GameObject WorldPoint;

    public float Local_X, Local_Y;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(WorldPoint.transform.position, 0.05f);

        Vector2 v = WorldPoint.transform.position - transform.position;
        // Compute the local coorpds
        Local_X = Vector2.Dot(v, transform.right);
        Local_Y = Vector2.Dot(v, transform.up);
    }
}
