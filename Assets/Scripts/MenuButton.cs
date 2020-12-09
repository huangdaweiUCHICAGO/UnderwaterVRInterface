using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public MenuButtonController menuButtonController;
    public int thisIndex;
    public CrewInfo crewmate;
    private bool isCurrNav = false;
    private Text textDisplay;

    // Start is called before the first frame update
    void Start()
    {
        textDisplay = this.transform.GetChild(0).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (menuButtonController.index == thisIndex) {
          textDisplay.text = "> " + crewmate.name;

          /* Navigation */
          if (menuButtonController.currNavIndex == thisIndex) {
            if (!isCurrNav) {
              Debug.Log("Pressed nav for index: " + thisIndex);
              isCurrNav = true;
            }
          } else {
            isCurrNav = false;
          }

          // Calling

        } else {
          textDisplay.text = crewmate.name;
        } 
    }
}
