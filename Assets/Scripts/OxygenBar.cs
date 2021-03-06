﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class OxygenBar : MonoBehaviour
{

    private Slider slider;
    private Image fill;
    private Text percentTextBox;

    public InformationManager iM;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        fill = slider.GetComponentsInChildren<Image>()[1];
        percentTextBox = slider.GetComponentsInChildren<Text>()[1];

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
        percentTextBox.text = ((int) level).ToString() + " %";
    }

    private void Update()
    {
        SetOxygenLevel(iM.GetOxygenLevel());

        //if the oxygen level is less than 20 turn red
        if (slider.value <= 15) {
            fill.color = Color.red;

        //if the oxygen level is less than 40 turn yellow
        } else if (slider.value <= 30) {
            fill.color = Color.yellow;

        //default color is white
        } else {
            fill.color = Color.green;
        }
		


    }


}
