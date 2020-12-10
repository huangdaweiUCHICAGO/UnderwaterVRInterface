using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public MenuButtonController menuButtonController;
    private int thisIndex = 0;

    public CrewInfo crewmate;

    private Text textDisplay;
    private Text distanceDisplay;
    private Image callIcon;
    private Image navigateIcon;
    
    public void SetIndex(int idx)
    {
        thisIndex = idx;
    }

    // Start is called before the first frame update
    void Start()
    {
        textDisplay = this.transform.GetChild(0).GetComponent<Text>();
        distanceDisplay = this.transform.GetChild(1).GetComponent<Text>();
        callIcon = this.transform.GetChild(2).GetComponent<Image>();
        navigateIcon = this.transform.GetChild(3).GetComponent<Image>();

        GetDistance();
        if (menuButtonController.GetIndex() == thisIndex) {
          textDisplay.text = "> " + crewmate.name;
        } else {
          textDisplay.text = crewmate.name;
        } 

        /* Navigation Icon */
        if (menuButtonController.im.IsTracking() &&
        menuButtonController.im.GetTracking().name == crewmate.name) {
          navigateIcon.enabled = true;
        } else {
          navigateIcon.enabled = false;
        }

        /* Call Icon */
        SimpleDial sd = menuButtonController.forearm.GetComponent<SimpleDial>();
        if ((!sd.IsEnded() || sd.IsPlaying()) && sd.GetCurrCrewmate() == crewmate.name) {
          callIcon.enabled = true;
        } else {
          callIcon.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (menuButtonController.GetIndex() == thisIndex)
        {
            textDisplay.text = "> " + crewmate.name;
        }
        else
        {
            textDisplay.text = crewmate.name;
        }

    }

    private void GetDistance()
    {
      Transform player = menuButtonController.player;
      float dist = Vector3.Distance(player.position, crewmate.worldLocation);
      distanceDisplay.text = dist.ToString("0") + " m";
    }
}
