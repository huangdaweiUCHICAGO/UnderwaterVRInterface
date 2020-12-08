using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnderwaterMonitor : MonoBehaviour
{
    private Text timeTextBox;
    private Text elevationTextBox;
    private Text alertTextBox;
    public InformationManager iM;

    // Start is called before the first frame update
    void Start()
    {
        timeTextBox = this.GetComponentsInChildren<Text>()[0];
        elevationTextBox = this.GetComponentsInChildren<Text>()[1];
        alertTextBox = this.GetComponentsInChildren<Text>()[2];
    }

    // Update is called once per frame
    void Update()
    {
        //set time underwater
        timeTextBox.text = "Time Underwater: " + iM.GetUnderwaterTime();

        //set current water depth
        string temp = iM.GetDepth().ToString();
        elevationTextBox.text = "Elevation: " + temp.Substring(0, temp.IndexOf('.') + 3) + " m";

        //Display Warning Text if Battery or Oxygen Low
        if ((((int) iM.GetOxygenLevel()) <= 15) && (((int) iM.GetBatteryLevel()) <= 15)) {
            alertTextBox.text = "Battery and Oxygen Low";
        } else if (((int) iM.GetOxygenLevel()) <= 15) {
            alertTextBox.text = "Oxygen Low Exit Water";
        } else if (((int) iM.GetBatteryLevel()) <= 15) {
            alertTextBox.text = "Battery Low";
        } else {
            alertTextBox.text = " ";
        }
    }
}
