using UnityEngine;
using TMPro;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;
using UnityEngine.SceneManagement;

public class InstantiateHistoryEntry : MonoBehaviour
{
    public GameObject historyEntryPrefab;
    public Transform parentTransform;

    void Start()
    {

    }

    public void InstantiateHistoryEntryWithDate()
    {
        
        GameObject historyEntryInstance = Instantiate(historyEntryPrefab, parentTransform);

        // Find TextMeshPro - Text Child Prefab
        TMP_Text dateText = historyEntryInstance.transform.Find("Date").GetComponent<TMP_Text>();
        TMP_Text gameModeText = historyEntryInstance.transform.Find("GameMode").GetComponent<TMP_Text>();
        TMP_Text calBurnText = historyEntryInstance.transform.Find("CaloriesBurned").GetComponent<TMP_Text>();
        TMP_Text avgHeartText = historyEntryInstance.transform.Find("AverageHeartRate").GetComponent<TMP_Text>();

        // SetDate
        System.DateTime now = System.DateTime.Now;
        int buddhistYear = now.Year + 543;
        string formattedDate = now.ToString($"dd/MM/{buddhistYear}");

        // SetGameModeText
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

        // SetCalBurnText
        string formattedCal = "Test 100 Kcal";

        // SetAvgHeartText
        string formattedAvg = "Test 120 Avg";

        // Display TextMeshPro
        dateText.text = formattedDate;

        gameModeText.text = formattedMode;

        calBurnText.text = formattedCal;

        avgHeartText.text = formattedAvg;
    }
}