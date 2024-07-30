using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChargeBar : MonoBehaviour
{
    public Image chargeBar;
    public Gradient gradient;
    public float charge, maxCharge;
    public float chargeRate;
    public float dischargeRate;
    public float slowDischargeRate;
    private PlayerMove playerMove;
    private GameManager manager;
    private Timer timer;
    public int score;
    public int extraScore;
    public float scoreIncreaseTimer; //timer
    public float scoreIncreaseInterval;
    public int countPlusScore;
    public GameObject objExtraX2;

    void Start()
    {
        timer = GameObject.FindObjectOfType<Timer>();
        playerMove = GameObject.FindObjectOfType<PlayerMove>();
        manager = GameObject.FindObjectOfType<GameManager>();
        charge = 0;
        objExtraX2.SetActive(false);
        UpdateChargeBar();
    }

    void Update()
    {
        if (playerMove.isMoving && !playerMove.slow)
        {
            charge = Mathf.Clamp(charge + chargeRate * Time.deltaTime, 0, maxCharge);
            HandleScoreIncrease();
        }
        else if (playerMove.isMoving && playerMove.slow)
        {
            charge = Mathf.Clamp(charge - slowDischargeRate * Time.deltaTime, 0, maxCharge);
            scoreIncreaseTimer = 0;
            countPlusScore = 0;
            objExtraX2.SetActive(false);
        }
        else if (!playerMove.isMoving && playerMove.slow || !playerMove.slow)
        {
            charge = Mathf.Clamp(charge - dischargeRate * Time.deltaTime, 0, maxCharge);
            scoreIncreaseTimer = 0;
            countPlusScore = 0;
            objExtraX2.SetActive(false);
        }

        UpdateChargeBar();

        if (countPlusScore >= 5)
        {
            objExtraX2.SetActive(true);
        }

        if (timer.remainingTime < 0)
        {
            SaveChargeValue();
        }
    }

    private void HandleScoreIncrease()
    {
        scoreIncreaseTimer += Time.deltaTime;

        if (scoreIncreaseTimer >= scoreIncreaseInterval)
        {   
            if (countPlusScore >= 5)
            {
                manager.playerScore += extraScore;
                scoreIncreaseTimer = 0;
            }
            else
            {
                manager.playerScore += score;
                countPlusScore++;
                scoreIncreaseTimer = 0;
            }
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

    public void SaveChargeValue()
    {
        PlayerPrefs.SetFloat("SavedCharge", charge);
    }
}
