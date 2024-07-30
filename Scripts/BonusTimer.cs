using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonusTimer : MonoBehaviour
{
    public GameObject BgStar;
    public TMP_Text timeStar;
    private loop_section loopSection;

    // Start is called before the first frame update
    void Start()
    {
        BgStar.SetActive(false);
        loopSection = FindObjectOfType<loop_section>();

        if (loopSection == null)
        {
            Debug.LogError("loop_section script not found in the scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (loopSection != null)
        {
            if (loopSection.isBonusTime)
            {
                BgStar.SetActive(true);

                // Calculate the remaining time
                float remainingTime = loopSection.bonusTimeEnd - Time.time;
                if (remainingTime < 0)
                {
                    remainingTime = 0;
                }

                // Update the timeStar text to show the remaining time
                timeStar.text = remainingTime.ToString("F1");
            }
            else
            {
                BgStar.SetActive(false);
                timeStar.text = ""; // Clear the text when not in bonus time
            }
        }
    }
}