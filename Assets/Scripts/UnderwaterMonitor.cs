using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnderwaterMonitor : MonoBehaviour
{
    private Text timeTextBox;
    private Text elevationTextBox;
    public InformationManager iM;

    // Start is called before the first frame update
    void Start()
    {
        timeTextBox = this.GetComponentsInChildren<Text>()[0];
        elevationTextBox = this.GetComponentsInChildren<Text>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        //set time underwater
        timeTextBox.text = "Time Underwater: " + iM.GetUnderwaterTime();

        //set current water depth
        string temp = iM.GetDepth().ToString();
        elevationTextBox.text = "Elevation: " + temp.Substring(0, temp.IndexOf('.') + 3) + " m";
    }
}
