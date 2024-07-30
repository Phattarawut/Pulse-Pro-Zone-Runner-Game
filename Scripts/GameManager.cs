using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private loop_section loopSection;
    public int playerScore;
    public int playerCoin;
    public TMP_Text textScore;
    public TMP_Text textCoin;

    public TMP_Text scoreEndGameWIN;
    public TMP_Text coinEndGameWIN;
    public TMP_Text scoreEndGameLOSE;
    public TMP_Text coinEndGameLOSE;

    void Start()
    {
        loopSection = GameObject.FindObjectOfType<loop_section>();

        if (SceneManager.GetActiveScene().name.StartsWith("Zone3_MiniGame"))
        {
            playerScore = PlayerPrefs.GetInt("Score");
        }
    }

    void Update()
    {
        textScore.text = playerScore.ToString();
        PlayerPrefs.SetInt("Score", playerScore);
        scoreEndGameWIN.text = PlayerPrefs.GetInt("Score").ToString();
        scoreEndGameLOSE.text = PlayerPrefs.GetInt("Score").ToString();
        textCoin.text = playerCoin.ToString();
        PlayerPrefs.SetInt("Coin", playerCoin);
        coinEndGameWIN.text = PlayerPrefs.GetInt("Coin").ToString();
        coinEndGameLOSE.text = PlayerPrefs.GetInt("Coin").ToString();
        PlayerPrefs.Save();
    }

    public void StartBonusTime()
    {
        if (loopSection != null)
        {
            loopSection.StartBonusTime();
        }
    }
}
