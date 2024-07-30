using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchG : MonoBehaviour
{
    public CharacterController characterController;
    public float gravity;
    private Vector3 velocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        velocity = Vector3.zero;

        if (SerialManager.instance != null)
        {
            SerialManager.instance.OnDataReceived += OnDataReceived;
        }
    }

    void Update()
    {
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            gravity *= -1;
            velocity.y = 0;
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
        if (data == "Button 1 pressed")
        {
            gravity = -Mathf.Abs(gravity);
            velocity.y = 0;
        }
        else if (data == "Button 3 pressed")
        {
            gravity = Mathf.Abs(gravity);
            velocity.y = 0;
        }
    }
}
