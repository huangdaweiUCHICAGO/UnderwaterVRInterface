using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnderwaterMonitor : MonoBehaviour
{
    private Text timeTextBox;
    private Text elevationTextBox;
    private Text alertTextBox;
    private Text impTextBox;

    private Image warning;
    private Image impWarning;

    public InformationManager iM;
    private AudioSource warningSource;
    public TTSManager audioManager;


    private bool impFinished = false;
    private bool noWarning= true;
    private bool firstBatteryLevel = false;

    // Start is called before the first frame update
    void Start()
    {
        timeTextBox = this.GetComponentsInChildren<Text>()[0];
        elevationTextBox = this.GetComponentsInChildren<Text>()[1];

        warning = this.GetComponentsInChildren<Image>()[0];
        impWarning = this.GetComponentsInChildren<Image>()[1];
        alertTextBox = warning.GetComponentsInChildren<Text>()[0];
        impTextBox = impWarning.GetComponentsInChildren<Text>()[0];

        warningSource = this.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update() {
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


        //The Warning Boxes

            //Display Warning Text if Battery or Oxygen Low
            if ((((int) iM.GetOxygenLevel()) <= 15) && (((int) iM.GetBatteryLevel()) <= 15) && noWarning) {
                StartCoroutine(SoundWarning("Battery and Oxygen Low"));

            } else if ((((int) iM.GetOxygenLevel()) <= 15) && (iM.GetDepth() < 0f) && noWarning) {

                StartCoroutine(SoundWarning("Oxygen Low - Exit Water"));

            } else if (((((int) iM.GetBatteryLevel()) <= 30)) && (!firstBatteryLevel)){
                firstBatteryLevel = true;
                audioManager.SayText("Warning: Battery Level under 30 percent");

            } else if (((int) iM.GetBatteryLevel()) <= 15) {
                
                if (iM.GetDepth() < 0f && noWarning) {
                    StartCoroutine(SoundWarning("Battery Low - Exit Water"));

                } else if (iM.GetDepth() >0f && !impFinished) {
                    impWarning.gameObject.SetActive (true);
                    impTextBox.text = "Battery Low - Stay on Land";
                    StartCoroutine(ImpSound("Battery Low - Stay on Land"));
                }

            } else {
                warning.gameObject.SetActive (false);
                impWarning.gameObject.SetActive(false);
                alertTextBox.text = "";
                impTextBox.text = " ";
            }
        }

    IEnumerator SoundWarning (string display) {
        noWarning = false;
        warning.gameObject.SetActive(true);
        alertTextBox.text = display;

        warningSource.Play();
        yield return new WaitForSeconds(2);
        warningSource.Pause();

        audioManager.SayText(display);
        yield return new WaitForSeconds(2);

        warning.gameObject.SetActive (false);
        alertTextBox.text = " ";
    }
     IEnumerator ImpSound (string display) {
        impFinished= true;
        noWarning = true;
        warningSource.Play();
        yield return new WaitForSeconds(2);
        warningSource.Pause();

        audioManager.SayText(display);
        yield return new WaitForSeconds(2);
    }
}
