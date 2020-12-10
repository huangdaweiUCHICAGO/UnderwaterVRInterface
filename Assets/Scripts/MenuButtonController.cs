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
    public GameObject forearm;
    public Transform player;
    public GameObject crewmateInfoPrefab;

    private int item_h = 25;
    private CrewInfo[] crewInformation;

    /* Update Menu */
    private ArrayList oldItems;
    private CrewInfo currCrewSelected;
    private int temp_index;

    [SerializeField] RectTransform rectTransform;

  
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        oldItems = new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {
        DestroyItems();
        AddItems();
    }

    private GameObject CreateItem(CrewInfo crewmate) {
        GameObject newItem = Instantiate(crewmateInfoPrefab, this.transform);
        MenuButton menuButton = newItem.GetComponent<MenuButton>();
        menuButton.thisIndex = temp_index;
        menuButton.crewmate = crewmate;

        menuButton.menuButtonController = this.GetComponent<MenuButtonController>();

        Text newItemText = newItem.transform.GetChild(0).GetComponent<Text>();
        newItemText.text = crewmate.name;
        return newItem;
    }

    void AddItems() {
      oldItems = new ArrayList();
      crewInformation = ctm.GetCrewmatesInformation();
      maxIndex = crewInformation.Length-1;

      if (maxIndex < 2) {
        rectTransform.offsetMax = Vector2.zero;
      }

      temp_index = 0;

      /* instantiate menu item for each crewmate */
      foreach(CrewInfo crewmate in crewInformation)
        {
          GameObject crewmateItem = CreateItem(crewmate);
          oldItems.Add(crewmateItem);
          temp_index++;
        }
    }

    void DestroyItems()
    {
      foreach (GameObject item in oldItems) {
        Destroy(item);
      }
    }

    /* Scrolling logic implementation based on:
    https://pavcreations.com/scrollable-menu-in-unity-with-button-or-key-controller/*/
    public void pressDown()
    {
      if (index < maxIndex) {
        index ++;
        if (index > 1 && index < maxIndex) {
          rectTransform.offsetMax -= (new Vector2(0, -item_h));
        }
        else if (index == maxIndex) {
          if (maxIndex > 1) {
            rectTransform.offsetMax = new Vector2(0, (maxIndex - 2)*item_h+item_h/2);
          }
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
        if (maxIndex > 1) {
          rectTransform.offsetMax = new Vector2(0, (maxIndex - 2)*item_h+item_h/2);
        }
      }
    }

    public void pressNav()
    {
      im.SetTracking(crewInformation[index]);
    }

    public void cancelNav()
    {
      im.ClearTracking(false);
    }

    public void callCrew()
    {
      SimpleDial sd = forearm.GetComponent<SimpleDial>();
      sd.QuickDial(crewInformation[index].frequency);
    }
}
