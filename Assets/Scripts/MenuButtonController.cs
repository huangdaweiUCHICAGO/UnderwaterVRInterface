using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour
{
    public int index;
    public int maxIndex;
    
    public CallTowerManager ctm;
    public InformationManager im;

    private int item_h = 25;
    public int currNavIndex = -1;
    private int currCallIndex;

    public GameObject crewmateInfoPrefab;

    [SerializeField] RectTransform rectTransform;

 
    public void OnEnable() {
      CrewInfo[] crewInformation = ctm.GetCrewmatesInformation();
      maxIndex = crewInformation.Length-1;

      int temp_index = 0;
      foreach(CrewInfo currCrewmate in crewInformation)
        {
          GameObject newCrewmate = Instantiate(crewmateInfoPrefab, this.transform);
          MenuButton menuButton = newCrewmate.GetComponent<MenuButton>();
          menuButton.thisIndex = temp_index;
          menuButton.menuButtonController = this.GetComponent<MenuButtonController>();

          Text newCrewmateText = newCrewmate.transform.GetChild(0).GetComponent<Text>();
          newCrewmateText.text = currCrewmate.name;

          temp_index++;
        }
    }

  
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pressDown()
    {
      if (index < maxIndex) {
        index ++;
        if (index > 1 && index < maxIndex) {
          rectTransform.offsetMax -= (new Vector2(0, -item_h));
        }
        else if (index == maxIndex) {
          rectTransform.offsetMax = new Vector2(0, (maxIndex - 2)*item_h+item_h/2);
        }
      } else {
        index = 0;
        rectTransform.offsetMax = Vector2.zero;
      }
    }

    public void pressUp()
    {
      if (index > 0) {
        index --;
        if (index < maxIndex - 1 && index > 0) {
          rectTransform.offsetMax -= (new Vector2(0, item_h));
        }
        else if (index == 0) {
          rectTransform.offsetMax = Vector2.zero;
        }
      } else {
        index = maxIndex;
        rectTransform.offsetMax = new Vector2(0, (maxIndex - 2)*item_h+item_h/2);
      }
    }

    public void pressNav()
    {
      //Debug.Log("Press Nav called on " + index);
      currNavIndex = index;
    }
}
