using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Xml.Schema;

public class Progressbar : MonoBehaviour
{

    public Image progressbar;
    public float xScale = 1f;
    float value = 0f;
    float value2 = 0f;
    bool done = false;
    public EasingFunction.Ease ee;
    private EasingFunction.Function ef;
    // Start is called before the first frame update
    void Start()
    {
        
        ef = EasingFunction.GetEasingFunction(ee);
    }

    // Update is called once per frame
    void Update()
    {
       
        //ef(0, 1, xScale);
        //Debug.Log(ef);
        xScale = ef(0, 1, value);
        progressbar.transform.localScale = new Vector3(xScale, 1, 1);

        value2 += Time.deltaTime;
        if (value2 > 2f)
        {
            if (!done)
            {
                value += Time.deltaTime;
            }

            if (value >= 1)
            {
                done = true;
            }
        }
    }
}
