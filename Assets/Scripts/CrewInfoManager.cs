using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrewInfoManager : MonoBehaviour
{
    public CallTowerManager ctm;
    private Text textBox;

    // Start is called before the first frame update
    void Start()
    {
        textBox = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        CrewInfo[] crewInfo = ctm.GetCrewmatesInformation();
        string crewText = "";

        foreach (CrewInfo c in crewInfo)
        {
            crewText += c.name + " (" + c.worldLocation.ToString() + ")\n";
        }

        textBox.text = crewText;
    }
}
