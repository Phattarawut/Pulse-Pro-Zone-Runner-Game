using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    }

    public void LoadHistory()
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

    private void AddHistoryEntry(string date, string gameMode, string calories, string heartRate)
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
