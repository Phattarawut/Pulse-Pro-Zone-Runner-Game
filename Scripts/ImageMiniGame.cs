using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageMiniGame : MonoBehaviour
{
    public Image canvasImage;
    public Sprite[] colors;
    public int coin = 1;
    private int lastIndex = -1; // ตัวแปรเก็บ index ล่าสุดที่ถูกสุ่มไว้

    public GameManager manager;
    private Coroutine countdownCoroutine;

    public AudioSource sfx_fail;
    public AudioSource sfx_getCoin;

    void Start()
    {
        manager = GameObject.FindObjectOfType<GameManager>();
        DisplayRandomColorImage();

        if (SerialManager.instance != null)
        {
            SerialManager.instance.OnDataReceived += OnDataReceived;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CheckColorMatch(0);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            CheckColorMatch(1);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            CheckColorMatch(2);
        }
    }

    void CheckColorMatch(int colorIndex)
    {
        if (canvasImage.sprite == colors[colorIndex])
        {
            manager.playerCoin += coin;
            sfx_getCoin.Play();

            DisplayRandomColorImage();
        }
    }

    void DisplayRandomColorImage()
    {
        if (colors.Length > 0)
        {
            int randomIndex = GetRandomIndex();

            // Assign the selected image to the canvasImage's sprite
            if (canvasImage != null)
            {
                canvasImage.sprite = colors[randomIndex];
                lastIndex = randomIndex; // เก็บ index ล่าสุดที่ถูกสุ่มไว้

                // Start the countdown coroutine
                if (countdownCoroutine != null)
                {
                    StopCoroutine(countdownCoroutine);
                }
                countdownCoroutine = StartCoroutine(CountdownCoroutine());
            }
            else
            {
                Debug.LogError("canvasImage is null!");
            }
        }
        else
        {
            Debug.LogError("No images found in the 'colors' array!");
        }
    }

    int GetRandomIndex()
    {
        int randomIndex = Random.Range(0, colors.Length);

        while (randomIndex == lastIndex)
        {
            randomIndex = Random.Range(0, colors.Length);
        }

        return randomIndex;
    }

    IEnumerator CountdownCoroutine()
    {
        yield return new WaitForSeconds(3);
        sfx_fail.Play();
        // If no correct button was pressed within 3 seconds, change the image
        DisplayRandomColorImage();
    }

    private void OnDestroy()
    {
        if (SerialManager.instance != null)
        {
            SerialManager.instance.OnDataReceived -= OnDataReceived;
        }
    }

    private void OnDataReceived(string data)
    {
        if (data == "Button 1 pressed")
        {
            CheckColorMatch(0);
        }
        else if (data == "Button 2 pressed")
        {
            CheckColorMatch(1);
        }
        else if (data == "Button 3 pressed")
        {
            CheckColorMatch(2);
        }
    }
}
