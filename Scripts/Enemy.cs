using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int score;
    public GameManager manager;
    public GameObject collectEffect;
    private bool isPlayerNearby = false;

    void Start()
    {
        manager = GameObject.FindObjectOfType<GameManager>();

        if (SerialManager.instance != null)
        {
            SerialManager.instance.OnDataReceived += OnDataReceived;
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKey(KeyCode.E))
        {
            Collect();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerNearby = false;
        }
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
        if (data == "Button 2 pressed" && isPlayerNearby)
        {
            Collect();
        }
    }

    private void Collect()
    {
        manager.playerScore += score;
        if (collectEffect)
        {
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
