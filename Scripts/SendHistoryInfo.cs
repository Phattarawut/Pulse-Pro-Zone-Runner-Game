using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SendHistoryInfo : MonoBehaviour
{
    [SerializeField] TMP_Text date;
    [SerializeField] TMP_Text gameMode;
    [SerializeField] TMP_Text caloriesBurned;
    [SerializeField] TMP_Text averageHeartRate;
    void Start()
    {
        DisplayCurrentDate();
        DisplayCurrentMode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisplayCurrentDate()
    {
        DateTime now = DateTime.Now;
        int buddhistYear = now.Year + 543;
        string formattedDate = now.ToString($"dd/MM/{buddhistYear}");
        date.text = formattedDate;
    }

    void DisplayCurrentMode()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string formattedMode = "";

        if (sceneName.StartsWith("Zone1") || sceneName.StartsWith("Zone2") || sceneName.StartsWith("Zone3"))
        {
            if (sceneName.Length > 5)
            {
                string zoneNumber = sceneName.Substring(0, 5); // Gets "Zone1", "Zone2" or "Zone3"
                char lastChar = sceneName[sceneName.Length - 1]; // Gets the last character of the scene name

                if (char.IsDigit(lastChar))
                {
                    formattedMode = $"{zoneNumber}/{lastChar}";
                }
                else
                {
                    formattedMode = zoneNumber; // If the last character is not a digit, just show the zone
                }
            }
            else
            {
                formattedMode = sceneName; // If the scene name length is not as expected, just show the whole name
            }
        }

        gameMode.text = formattedMode;
    }

    void DisplayBurnKcal() 
    {
        
    }
    void DisplayRateOfBPM()
    {

    }
}