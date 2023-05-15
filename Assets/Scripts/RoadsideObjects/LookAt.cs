using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LookAt : MonoBehaviour
{

    public Transform Target; // The target object to look at
    public float LookThreshold = 0.95f; // The threshold for looking directly at the target
    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Target.position - transform.position;
        direction.Normalize();
        float dot = Vector3.Dot(transform.forward, direction);
        if (dot >= LookThreshold)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.cyan;
        }
    }
}
