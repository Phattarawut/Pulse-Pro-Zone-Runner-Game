/*using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class HistoryManager : MonoBehaviour
{
    public GameObject historyEntryPrefab;
    public Transform historyContent;
    public int maxEntries = 10;

    private Queue<GameObject> historyEntries = new Queue<GameObject>();
    private const string historyKey = "historyEntries";

    private void Start()
    {
        LoadHistory();
        // Load history data if available
        if (HistoryInfo.historyData.Count > 0)
        {
            foreach (var data in HistoryInfo.historyData)
            {
                AddHistoryEntry(data.date, data.gameMode, data.calories, data.heartRate);
            }
            HistoryInfo.historyData.Clear();
        }
    }

    public void AddHistoryEntry(string date, string gameMode, string calories, string heartRate)
    {
        if (historyEntries.Count >= maxEntries)
        {
            Destroy(historyEntries.Dequeue());
        }

        GameObject newEntry = Instantiate(historyEntryPrefab, historyContent);
        TMP_Text[] textComponents = newEntry.GetComponentsInChildren<TMP_Text>();
        textComponents[0].text = date;
        textComponents[1].text = gameMode;
        textComponents[2].text = calories;
        textComponents[3].text = heartRate;

        // Add newEntry to the top of the historyContent
        newEntry.transform.SetSiblingIndex(0);

        historyEntries.Enqueue(newEntry);
        SaveHistory();
    }

    private void SaveHistory()
    {
        List<string> entries = historyEntries.Select(entry =>
        {
            TMP_Text[] textComponents = entry.GetComponentsInChildren<TMP_Text>();
            return $"{textComponents[0].text}|{textComponents[1].text}|{textComponents[2].text}|{textComponents[3].text}";
        }).ToList();

        PlayerPrefs.SetString(historyKey, string.Join("#", entries));
        PlayerPrefs.Save();
    }

    private void LoadHistory()
    {
        if (PlayerPrefs.HasKey(historyKey))
        {
            string savedData = PlayerPrefs.GetString(historyKey);
            string[] entries = savedData.Split('#');
            foreach (string entry in entries)
            {
                string[] data = entry.Split('|');
                if (data.Length == 4)
                {
                    AddHistoryEntry(data[0], data[1], data[2], data[3]);
                }
            }
        }
    }

    public void ClearHistory()
    {
        // Clear PlayerPrefs
        PlayerPrefs.DeleteKey(historyKey);
        PlayerPrefs.Save();

        // Destroy all current history entries in the UI
        while (historyEntries.Count > 0)
        {
            Destroy(historyEntries.Dequeue());
        }
    }
}*/

using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class HistoryManager : MonoBehaviour
{
    public GameObject historyEntryPrefab;
    public Transform historyContent;
    public int maxEntries = 10;

    private Queue<GameObject> historyEntries = new Queue<GameObject>();
    private const string historyKey = "historyEntries";

    private void Start()
    {
        LoadHistory();
        // Load history data if available
        if (HistoryInfo.historyData.Count > 0)
        {
            foreach (var data in HistoryInfo.historyData)
            {
                AddHistoryEntry(data.date, data.gameMode, data.calories, data.heartRate);
            }
            HistoryInfo.historyData.Clear();
        }
    }

    public void AddHistoryEntry(string date, string gameMode, string calories, string heartRate)
    {
        if (historyEntries.Count >= maxEntries)
        {
            Destroy(historyEntries.Dequeue());
        }

        GameObject newEntry = Instantiate(historyEntryPrefab, historyContent);
        TMP_Text[] textComponents = newEntry.GetComponentsInChildren<TMP_Text>();
        textComponents[0].text = date;
        textComponents[1].text = gameMode;
        textComponents[2].text = calories;
        textComponents[3].text = heartRate;

        // Add newEntry to the top of the historyContent
        newEntry.transform.SetSiblingIndex(0);

        historyEntries.Enqueue(newEntry);
        SaveHistory();
    }

    private void SaveHistory()
    {
        List<string> entries = historyEntries.Select(entry =>
        {
            TMP_Text[] textComponents = entry.GetComponentsInChildren<TMP_Text>();
            return $"{textComponents[0].text}|{textComponents[1].text}|{textComponents[2].text}|{textComponents[3].text}";
        }).ToList();

        PlayerPrefs.SetString(historyKey, string.Join("#", entries));
        PlayerPrefs.Save();
    }

    private void LoadHistory()
    {
        if (PlayerPrefs.HasKey(historyKey))
        {
            string savedData = PlayerPrefs.GetString(historyKey);
            string[] entries = savedData.Split('#');
            foreach (string entry in entries)
            {
                string[] data = entry.Split('|');
                if (data.Length == 4)
                {
                    AddHistoryEntry(data[0], data[1], data[2], data[3]);
                }
            }
        }
    }

    public void ClearHistory()
    {
        // Clear PlayerPrefs
        PlayerPrefs.DeleteKey(historyKey);
        PlayerPrefs.Save();

        // Destroy all current history entries in the UI
        while (historyEntries.Count > 0)
        {
            Destroy(historyEntries.Dequeue());
        }
    }
}