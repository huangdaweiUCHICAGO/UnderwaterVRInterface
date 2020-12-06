using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class OxygenBar : MonoBehaviour
{

    private Slider slider;
    private Image fill;
    public InformationManager iM;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        fill = slider.GetComponentsInChildren<Image>()[0];
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
    }

    private void Update()
    {
        SetOxygenLevel(iM.GetOxygenLevel());

        //if the oxygen level is less than 40 turn orange
        if (slider.value < 40) {
            fill.color = Color.yellow;

        //if the oxygen level is less than 20 turn orange
        } else if (slider.value < 20) {
            fill.color = Color.red;

        //default color is white
        } else {
            fill.color = Color.white;
        }

    }


}
