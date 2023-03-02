using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrossProductPlacer : MonoBehaviour
{
    public GameObject player;
    public GameObject objectToSpawn;

    private void Update()
    {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, 100f))
                {
                    Debug.DrawLine(player.transform.position, hit.point, Color.red, 10f);
                    Debug.Log(hit.point);
                    GameObject spawned = Instantiate(objectToSpawn, hit.point, Quaternion.identity);
                    spawned.transform.position = hit.point;
                    spawned.transform.right = Vector3.Cross(hit.normal, player.transform.forward);
                    spawned.transform.forward = Vector3.Cross(spawned.transform.right, spawned.transform.up);
                }
        }

    }
}
