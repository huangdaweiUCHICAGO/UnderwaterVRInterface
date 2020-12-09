using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class InformationManager : MonoBehaviour
{
    [Header("User Information")]
    public Transform player;

    [Header("Water Information")]
    public Transform waterPlane;
    // TODO: delete this
    public CallTowerManager towerManager;

    // Oxygen info
    private float oxygenLevelMax = 100f;
    private float currentOxygenLevel = 100f;
    private float oxygenConsumpsionRate = -0.54f; // in oxygen/second.
    private float oxygenReplenishRate = 1.66f; // in oxygen/second.

    // Battery info
    private float batteryLevelMax = 100f;
    private float currentBatteryLevel = 100f;
    private float batteryConsumptionRate = -0.8f; // in batt/second.

    // Timer
    private float underwaterTime = 0f;
    private String underwaterTimeStr = "---";

    private CrewInfo trackedCrew;
    private bool isTracking = false;
    private bool initialFrame = true;

    

    /*
     * INFORMATION RETRIEVAL FUNCTIONS
     */

    public void Start()
    {
        
    }

    public String GetUnderwaterTime()
    {
        return underwaterTimeStr;
    }

    public void SetTracking(CrewInfo crew)
    {
        isTracking = true;
        trackedCrew = crew;
    }

    public void ClearTracking()
    {
        isTracking = false;
    }

    public bool IsTracking()
    {
        return isTracking;
    }

    public CrewInfo GetTracking()
    {
        return trackedCrew;
    }

    public float GetDepth()
    {
        return player.position.y - waterPlane.position.y;
    }

    public float GetBatteryLevel()
    {
        return currentBatteryLevel;
    }

    public float GetMaxBatteryLevel()
    {
        return batteryLevelMax;
    }

    public float GetOxygenLevel()
    {
        return currentOxygenLevel;
    }

    public float GetMaxOxygenLevel()
    {
        return oxygenLevelMax;
    }



    /*
     * UPDATE FUNCTIONS
     */
    
    private void BatteryUpdate ()
    {
        currentBatteryLevel = Mathf.Clamp(currentBatteryLevel + batteryConsumptionRate * Time.deltaTime, 0f, batteryLevelMax);

        if ((currentBatteryLevel <= 0.1f) && (GetDepth() < 0f))
            GameOverBattery();
    }

    private void OxygenUpdate ()
    {
        float factor = 0f;

        if (GetDepth() < 0f)
        {
            factor = oxygenConsumpsionRate;
        } else
        {
            factor = oxygenReplenishRate;
        }
        float oxygenDifference =  factor * Time.deltaTime;

        currentOxygenLevel = Mathf.Clamp(currentOxygenLevel + oxygenDifference, 0f, oxygenLevelMax);

        if (currentOxygenLevel <= 0.05f)
            GameOverOxygen();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Delete this
        if (initialFrame)
        {
            SetTracking(towerManager.GetCrewmatesInformation()[0]);
            initialFrame = false;
        }
        OxygenUpdate();
        BatteryUpdate();
        UnderwaterTimerUpdate();
    }

    void UnderwaterTimerUpdate()
    {
        if (GetDepth() >= 0f)
        {
            underwaterTime = 0f;
            underwaterTimeStr = "---";
        } else
        {
            underwaterTime += Time.deltaTime;
        }

        int seconds = (int) Math.Floor(underwaterTime % 60);
        String secondsStr;
        if (seconds < 10)
        {
            secondsStr = "0" + seconds;
        } else
        {
            secondsStr = seconds.ToString();
        }
        underwaterTimeStr = Math.Floor(underwaterTime / 60) + ":" + secondsStr;
    }

    void GameOverOxygen()
    {
        SceneManager.LoadScene("GameOverOxygen", LoadSceneMode.Single);
    }

    void GameOverBattery()
    {
        SceneManager.LoadScene("GameOverBattery", LoadSceneMode.Single);
    }
}
