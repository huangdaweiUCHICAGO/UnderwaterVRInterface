using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class BatteryBar : MonoBehaviour
{   private Slider slider;
    private Image fill;
    private Text textBox;
    public InformationManager iM;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        fill = slider.GetComponentsInChildren<Image>()[0];
        textBox = slider.GetComponentsInChildren<Text>()[1];
        SetMaxBatteryLevel(iM.GetMaxBatteryLevel());
    }

    public void SetMaxBatteryLevel (float maxLevel)
    {
        slider.maxValue = maxLevel;
        slider.value = maxLevel; // Start with max!
    }
       public void SetBatteryLevel (float level)
    {
        slider.value = level;
        textBox.text = ((int) level).ToString() + " %";
    }

    // Update is called once per frame
    void Update()
    {
        SetBatteryLevel(iM.GetBatteryLevel());

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
        
    }
}
