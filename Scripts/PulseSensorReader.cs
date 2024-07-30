using TMPro;
using UnityEngine;

public class PulseSensorReader : MonoBehaviour
{
    public TextMeshProUGUI pulse;
    private int pulseValue = 0;
    private int bpm = 0;

    private float timer = 0f;
    private float updateInterval = 0.5f;

    void Start()
    {
        if (SerialManager.instance != null)
        {
            SerialManager.instance.OnDataReceived += OnDataReceived;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= updateInterval)
        {
            timer = 0f;
            // อัปเดตข้อความบน TextMeshProUGUI
            if (pulse != null)
            {
                pulse.text = bpm.ToString();
            }
        }
    }

    void OnDestroy()
    {
        if (SerialManager.instance != null)
        {
            SerialManager.instance.OnDataReceived -= OnDataReceived;
        }
    }

    void OnDataReceived(string data)
    {
        if (int.TryParse(data, out pulseValue))
        {
            bpm = CalculateBPM(pulseValue);
            Debug.Log("BPM: " + bpm);
        }
    }

    int CalculateBPM(int pulseValue)
    {
        return (int)Mathf.Round((float)pulseValue / 10f);
    }
}
