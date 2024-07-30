using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarningTime : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI timerText1;
    public GameObject BgWaringPulse;
    public GameObject BgWarningNoMove;
    private float countdownTime = 10f; // Countdown time in seconds
    public float timer; // Timer to track the countdown
    public float timer1;

    private Health health; // Reference to the Health class

    // Start is called before the first frame update
    void Start()
    {
        timer = countdownTime; // Initialize the timer
        timer1 = countdownTime;
        health = GameObject.FindObjectOfType<Health>();
        if (health == null)
        {
            Debug.LogError("Health component is not attached to the GameObject or WarningTime script is not attached to a GameObject with Health component.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (BgWaringPulse.activeSelf) // Check if BgWaringPulse is active
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime; // Decrease the timer
                if (timer <= 0)
                {
                    timer = countdownTime; // Reset the timer
                    health.currentHealth--; // Decrease currentHealth by 1
                    Debug.Log("Countdown finished. Current Health: " + health.currentHealth);
                }
            }
        }
        else
        {
            timer = countdownTime; // Reset the timer
        }

        // Update the UI text with the current countdown timer in seconds
        timerText.text = Mathf.CeilToInt(timer).ToString(); // Display timer as an integer (seconds)
        
        if (BgWarningNoMove.activeSelf)
        {
            if (timer1 > 0)
            {
                timer1 -= Time.deltaTime;
                if (timer1 <= 0)
                {
                    timer1 = countdownTime;
                    health.currentHealth--;
                    Debug.Log("Countdown finished. Current Health: " + health.currentHealth);
                }
            }
        }
        else
        {
            timer1 = countdownTime;
        }
        timerText1.text = Mathf.CeilToInt(timer1).ToString();
    }
}