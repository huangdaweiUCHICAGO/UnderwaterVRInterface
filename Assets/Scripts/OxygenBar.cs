using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class OxygenBar : MonoBehaviour
{

    private Slider slider;
    private Image fill;
    private Text textBox1;
    private Text textBox2;
    private Text textBox3;
    public InformationManager iM;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        fill = slider.GetComponentsInChildren<Image>()[0];
        textBox1 = slider.GetComponentsInChildren<Text>()[1];
        textBox2 = slider.GetComponentsInChildren<Text>()[2];
        textBox3 = slider.GetComponentsInChildren<Text>()[3];
        SetMaxOxygenLevel(iM.GetMaxOxygenLevel());
    }

    public void SetMaxOxygenLevel (float maxLevel)
    {
        slider.maxValue = maxLevel;
        slider.value = maxLevel; // Start with max!
    }

    public void SetOxygenLevel (float level)
    {
        slider.value = level;
        textBox1.text = ((int) level).ToString() + " %";
    }

    private void Update()
    {
        SetOxygenLevel(iM.GetOxygenLevel());

        //if the oxygen level is less than 20 turn red
        if (slider.value <= 20) {
            fill.color = Color.red;

        //if the oxygen level is less than 40 turn yellow
        } else if (slider.value <= 40) {
            fill.color = Color.yellow;

        //default color is white
        } else {
            fill.color = Color.white;
        }
		
		//set time underwater
        textBox2.text = "Time in Water: " + iM.GetUnderwaterTime();
		
		//set current water depth
		string temp = iM.GetDepth().ToString();
        textBox3.text = "Elevation: " + temp.Substring(0, temp.IndexOf('.') + 3);

    }


}
