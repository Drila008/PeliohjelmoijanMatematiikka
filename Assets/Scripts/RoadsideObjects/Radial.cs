using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Radial : MonoBehaviour
{
    public GameObject target;
    public float radius = 2f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Pow((target.transform.position.x - transform.position.x), 2)
    + Mathf.Pow((target.transform.position.y - transform.position.y), 2)
    + Mathf.Pow((target.transform.position.z - transform.position.z), 2) < Mathf.Pow(radius, 2))
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else
            GetComponent<Renderer>().material.color = Color.magenta;
    }
}
