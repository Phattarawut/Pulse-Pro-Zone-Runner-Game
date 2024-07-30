using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoInput : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM3", 9600);

    public CharacterController characterController; // ใช้ private แทน public
    public float gravity;
    private Vector3 velocity;

    void Start()
    {
        sp.Open();
        sp.ReadTimeout = 1;

        characterController = GetComponent<CharacterController>(); // ดึง Component CharacterController
        velocity = Vector3.zero;
    }

    void Update()
    {
        velocity.y += gravity * Time.deltaTime;

        if (characterController != null)
        {
            characterController.Move(velocity * Time.deltaTime);
        }
        else
        {
            Debug.LogError("CharacterController not found on the GameObject");
        }

        if (sp.IsOpen)
        {
            try
            {
                if (sp.ReadByte() == 1)
                {
                    gravity *= -1;
                    // Reset vertical velocity when gravity is switched
                    velocity.y = 0;
                }
            }
            catch (System.Exception)
            {
                // Ignore timeout exceptions
            }
        }
    }

    void OnApplicationQuit()
    {
        sp.Close();
    }
}
