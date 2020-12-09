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

        //set current water depth
        string temp = iM.GetDepth().ToString();
        if (iM.GetDepth() < 0f) {
            timeTextBox.text = "Time Underwater: " + iM.GetUnderwaterTime();
            string depth = temp.Substring(1, temp.IndexOf('.') + 1);
            elevationTextBox.text = "Depth: " + depth + " meters";
        } else {
            timeTextBox.text = " ";
            elevationTextBox.text = " ";
        }

        //Display Warning Text if Battery or Oxygen Low
        if ((((int) iM.GetOxygenLevel()) <= 15) && (((int) iM.GetBatteryLevel()) <= 15)) {
            alertTextBox.text = "Battery and Oxygen Low";
        } else if (((int) iM.GetOxygenLevel()) <= 15) {
            alertTextBox.text = "Oxygen Low Exit Water";
        } else if (((int) iM.GetBatteryLevel()) <= 15) {
            if (iM.GetDepth() < 0f) {
                alertTextBox.text = "Battery Low Exit Water";
            } else {
                alertTextBox.text = "Battery Low  Stay in Land";
            }
        } else {
            alertTextBox.text = " ";
        }
    }
}
