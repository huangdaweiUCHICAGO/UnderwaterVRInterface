using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public MenuButtonController menuButtonController;
    public int thisIndex;

    public CrewInfo crewmate;

    private Text textDisplay;
    private Text distanceDisplay;
    

    // Start is called before the first frame update
    void Start()
    {
        textDisplay = this.transform.GetChild(0).GetComponent<Text>();
        distanceDisplay = this.transform.GetChild(1).GetComponent<Text>();
        GetDistance();
        if (menuButtonController.index == thisIndex) {
          textDisplay.text = "> " + crewmate.name;
        } else {
          textDisplay.text = crewmate.name;
        } 
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void GetDistance()
    {
      Transform player = menuButtonController.player;
      float dist = Vector3.Distance(player.position, crewmate.worldLocation);
      distanceDisplay.text = dist.ToString("0") + " m";
    }
}
