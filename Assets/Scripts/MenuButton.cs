using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public MenuButtonController menuButtonController;
    public int thisIndex;
    private bool isCurrNav = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (menuButtonController.index == thisIndex) {

          // Navigation
          if (menuButtonController.currNavIndex == thisIndex) {
            if (!isCurrNav) {
              Debug.Log("Pressed nav for index: " + thisIndex);
              isCurrNav = true;
            }
          } else {
            isCurrNav = false;
          }

          // Calling
          
        } 
    }
}
