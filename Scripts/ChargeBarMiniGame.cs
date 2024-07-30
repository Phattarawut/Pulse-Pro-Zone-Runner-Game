using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBarMiniGame : MonoBehaviour
{
    public Image chargeBar;
    public Gradient gradient;
    public float charge, maxCharge;
    public float dischargeRate = 1f; // Rate at which charge decreases
    public GameObject winnerMenu;

    void Start()
    {
        winnerMenu.SetActive(false);
        charge = PlayerPrefs.GetFloat("SavedCharge", charge);
        UpdateChargeBar();
    }

    void Update()
    {
        // Gradually decrease the charge
        charge = Mathf.Clamp(charge - dischargeRate * Time.deltaTime, 0, maxCharge);
        UpdateChargeBar();

        if (charge == 0)
        {
            EndGame();
        }
    }

    private void UpdateChargeBar()
    {
        if (chargeBar != null)
        {
            chargeBar.fillAmount = charge / maxCharge;
            chargeBar.color = gradient.Evaluate(charge / maxCharge);
        }
    }

    public void EndGame()
    {
        Time.timeScale = 0f;
        winnerMenu.SetActive(true);
    }
}
