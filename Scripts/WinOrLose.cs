using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinOrLose : MonoBehaviour
{
    public Timer timer;
    public Health health;
    public GameManager manager;
    public HistoryInfo historyInfo;

    public GameObject winnerMenu;
    public GameObject deadMenu;
    public GameObject hintMenu;

    public bool win;
    public bool lose;
    private bool hasLost = false; // Flag to ensure Lose() is only called once

    // Start is called before the first frame update
    void Start()
    {
        win = false;
        winnerMenu.SetActive(false);
        lose = false;
        deadMenu.SetActive(false);
        timer = GameObject.FindObjectOfType<Timer>();
        health = GameObject.FindObjectOfType<Health>();
        manager = GameObject.FindObjectOfType<GameManager>();
        historyInfo = GameObject.FindObjectOfType<HistoryInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.remainingTime < 0)
        {
            win = true;
            CheckSceneAndHandleWinner();
        }
        if (health.currentHealth == 0 && !hasLost)
        {
            lose = true;
            Lose();
        }
    }

    public void CheckSceneAndHandleWinner()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName.StartsWith("Zone3"))
        {
            HandleZone3Winner();
        }
        else
        {
            Winner();
        }
    }

    public void Winner()
    {
        Time.timeScale = 0f;
        winnerMenu.SetActive(true);
        historyInfo.SendNewHistory();
    }

    public void Lose()
    {
        Time.timeScale = 0f;
        manager.playerScore = 0;
        deadMenu.SetActive(true);
        historyInfo.SendNewHistory();
        hasLost = true; // Ensure Lose() is only called once
    }

    public void HandleZone3Winner()
    {
        Time.timeScale = 0f;
        hintMenu.SetActive(true);
    }
}
