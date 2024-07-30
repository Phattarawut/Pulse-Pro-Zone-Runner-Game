using System;
using System.Collections.Concurrent;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class SerialManager : MonoBehaviour
{
    public static SerialManager instance;

    public string portName = "COM3";
    public int baudRate = 9600;

    private SerialPort serialPort;
    private Thread serialThread;
    private ConcurrentQueue<string> messageQueue = new ConcurrentQueue<string>();
    private bool isRunning = true;

    public event Action<string> OnDataReceived;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        serialPort = new SerialPort(portName, baudRate);
        try
        {
            serialPort.Open();
            serialThread = new Thread(ReadSerialPort);
            serialThread.Start();
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error opening serial port: " + ex.Message);
        }
    }

    void Update()
    {
        while (messageQueue.TryDequeue(out string message))
        {
            OnDataReceived?.Invoke(message);
        }
    }

    private void ReadSerialPort()
    {
        while (isRunning)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    string value = serialPort.ReadLine().Trim();
                    messageQueue.Enqueue(value);
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error reading from serial port: " + ex.Message);
            }
        }
    }

    private void OnDestroy()
    {
        isRunning = false;
        if (serialThread != null && serialThread.IsAlive)
        {
            serialThread.Join();
        }

        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
