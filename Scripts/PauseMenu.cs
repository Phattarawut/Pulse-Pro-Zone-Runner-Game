using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public TextMeshProUGUI countdownText;
    public bool isPaused;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    IEnumerator CountdownCoroutine()
    {
        pauseMenu.SetActive(false);
        countdownText.gameObject.SetActive(true);
        countdownText.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        countdownText.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        countdownText.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        countdownText.text = "Go!";
        yield return new WaitForSecondsRealtime(1f);
        countdownText.gameObject.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        if (isPaused)
        {
            StartCoroutine(CountdownCoroutine());
        }
    }

    public void RestartZ1S1()
    {
        SceneManager.LoadScene("Zone1_set1");
        Time.timeScale = 1f;
    }

    public void RestartZ1S2()
    {
        SceneManager.LoadScene("Zone1_set2");
        Time.timeScale = 1f;
    }

    public void RestartZ1S3()
    {
        SceneManager.LoadScene("Zone1_set3");
        Time.timeScale = 1f;
    }

    public void RestartZ2S1()
    {
        SceneManager.LoadScene("Zone2_set1");
        Time.timeScale = 1f;
    }

    public void RestartZ2S2()
    {
        SceneManager.LoadScene("Zone2_set2");
        Time.timeScale = 1f;
    }

    public void RestartZ2S3()
    {
        SceneManager.LoadScene("Zone2_set3");
        Time.timeScale = 1f;
    }

    public void RestartZ3S1()
    {
        SceneManager.LoadScene("Zone3_set1");
        Time.timeScale = 1f;
    }

    public void RestartZ3S2()
    {
        SceneManager.LoadScene("Zone3_set2");
        Time.timeScale = 1f;
    }

    public void RestartZ3S3()
    {
        SceneManager.LoadScene("Zone3_set3");
        Time.timeScale = 1f;
    }

    public void GoMiniGame1()
    {
        SceneManager.LoadScene("Zone3_MiniGame1");
        Time.timeScale = 1f;
    }

    public void GoMiniGame2()
    {
        SceneManager.LoadScene("Zone3_MiniGame2");
        Time.timeScale = 1f;
    }

    public void GoMiniGame3()
    {
        SceneManager.LoadScene("Zone3_MiniGame3");
        Time.timeScale = 1f;
    }

    public void History()
    {
        SceneManager.LoadScene("History");
    }
}
