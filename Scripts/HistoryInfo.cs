using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HistoryInfo : MonoBehaviour
{
    [System.Serializable]
    public struct HistoryData
    {
        public string date;
        public string gameMode;
        public string calories;
        public string heartRate;

        public HistoryData(string date, string gameMode, string calories, string heartRate)
        {
            this.date = date;
            this.gameMode = gameMode;
            this.calories = calories;
            this.heartRate = heartRate;
        }
    }

    public static List<HistoryData> historyData = new List<HistoryData>();
    private const string historyKey = "historyEntries";

    public void SendHistoryData(string date, string gameMode, string calories, string heartRate)
    {
        historyData.Add(new HistoryData(date, gameMode, calories, heartRate));
        SaveHistory();
    }

    public void SendNewHistory()
    {
        // SetDate
        System.DateTime now = System.DateTime.Now;
        int buddhistYear = now.Year + 543;
        string formattedDate = now.ToString($"dd/MM/{buddhistYear}");

        // SetGameMode
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
            }
        }

        string calories = "3.44 (testSend)";
        string heartRate = "120 (testSend)";

        SendHistoryData(formattedDate, formattedMode, calories, heartRate);
    }

    private void SaveHistory()
    {
        List<string> entries = historyData.Select(data =>
            $"{data.date}|{data.gameMode}|{data.calories}|{data.heartRate}").ToList();

        PlayerPrefs.SetString(historyKey, string.Join("#", entries));
        PlayerPrefs.Save();
    }
}
