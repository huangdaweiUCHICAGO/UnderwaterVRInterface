using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnderwaterMonitor : MonoBehaviour
{
    private Text timeTextBox;
    private Text elevationTextBox;
    private Text alertTextBox;
    private Image warning;
    public InformationManager iM;
    private bool WarningFinished = true;

    // Start is called before the first frame update
    void Start()
    {
        timeTextBox = this.GetComponentsInChildren<Text>()[0];
        elevationTextBox = this.GetComponentsInChildren<Text>()[1];
        warning = this.GetComponentsInChildren<Image>()[0];
        alertTextBox = warning.GetComponentsInChildren<Text>()[0];

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

        if (WarningFinished) {
            //Display Warning Text if Battery or Oxygen Low
            if ((((int) iM.GetOxygenLevel()) <= 15) && (((int) iM.GetBatteryLevel()) <= 15)) {
                warning.gameObject.SetActive (true);
                alertTextBox.text = "Battery and Oxygen Low";
            } else if (((int) iM.GetOxygenLevel()) <= 15) {
                warning.gameObject.SetActive (true);
                alertTextBox.text = "Oxygen Low Exit Water";
            } else if (((int) iM.GetBatteryLevel()) <= 15) {
                warning.gameObject.SetActive (true);
                if (iM.GetDepth() < 0f) {
                    StartCoroutine(SoundWarning("Battery Low Exit Water"));
                   // alertTextBox.text = "Battery Low Exit Water";
                } else {
                    StartCoroutine(SoundWarning("Battery Low Stay in Land"));
                    //alertTextBox.text = "Battery Low Stay in Land";
                }
            } else {
                warning.gameObject.SetActive (false);
                alertTextBox.text = " ";
            }
        }
    }
    IEnumerator SoundWarning (string display) {
        WarningFinished = false;
        warning.gameObject.SetActive (true);
        alertTextBox.text = display;
        yield return new WaitForSeconds(5);
        warning.gameObject.SetActive (false);
        alertTextBox.text = " ";
    }
}
