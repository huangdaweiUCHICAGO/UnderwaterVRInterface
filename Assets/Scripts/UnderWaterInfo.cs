using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnderWaterInfo : MonoBehaviour
{
    public InformationManager im;
    private Text time;
    private float start;

    // Start is called before the first frame update
    void Start()
    {
        time = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {   

    }
}
