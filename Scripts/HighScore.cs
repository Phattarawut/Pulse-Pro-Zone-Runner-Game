using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    public int highScoreZ1;
    public int highScoreZ2;
    public int highScoreZ3;

    public TMP_Text highScoreText;
    private int currentHighScore;

    private string currentScenePrefix;

    void Start()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (sceneName.StartsWith("Zone1"))
        {
            currentScenePrefix = "Zone1";
            highScoreZ1 = PlayerPrefs.GetInt("HighScoreZ1");
            currentHighScore = highScoreZ1;
        }
        else if (sceneName.StartsWith("Zone2"))
        {
            currentScenePrefix = "Zone2";
            highScoreZ2 = PlayerPrefs.GetInt("HighScoreZ2");
            currentHighScore = highScoreZ2;
        }
        else if (sceneName.StartsWith("Zone3"))
        {
            currentScenePrefix = "Zone3";
            highScoreZ3 = PlayerPrefs.GetInt("HighScoreZ3");
            currentHighScore = highScoreZ3;
        }

        UpdateHighScoreText();
    }

    void Update()
    {
        GameManager manager = GameObject.FindObjectOfType<GameManager>();
        if (manager != null)
        {
            int playerScore = manager.playerScore;

            if (playerScore > currentHighScore)
            {
                currentHighScore = playerScore;

                if (currentScenePrefix == "Zone1")
                {
                    highScoreZ1 = playerScore;
                    PlayerPrefs.SetInt("HighScoreZ1", highScoreZ1);
                }
                else if (currentScenePrefix == "Zone2")
                {
                    highScoreZ2 = playerScore;
                    PlayerPrefs.SetInt("HighScoreZ2", highScoreZ2);
                }
                else if (currentScenePrefix == "Zone3")
                {
                    highScoreZ3 = playerScore;
                    PlayerPrefs.SetInt("HighScoreZ3", highScoreZ3);
                }

                PlayerPrefs.Save();
                UpdateHighScoreText();
            }
        }
    }

    void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            highScoreText.text = currentHighScore.ToString();
        }
    }

    public void Reset()
    {
        highScoreZ1 = 0;
        highScoreZ2 = 0;
        highScoreZ3 = 0;

        PlayerPrefs.SetInt("HighScoreZ1", highScoreZ1);
        PlayerPrefs.SetInt("HighScoreZ2", highScoreZ2);
        PlayerPrefs.SetInt("HighScoreZ3", highScoreZ3);

        PlayerPrefs.Save();

        // Reset the current high score and update the UI
        currentHighScore = 0;
        UpdateHighScoreText();
    }
}