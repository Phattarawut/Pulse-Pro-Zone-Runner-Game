using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    public GameObject enemyBall;
    public Transform player;
    public float ballSpeed = 10f;
    private Health health; // Assuming you have a Health class with currentHealth
    private int previousHealth;

    // Start is called before the first frame update
    void Start()
    {
        health = player.GetComponent<Health>(); // Adjust based on where Health component is
        previousHealth = health.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if health has decreased
        if (health.currentHealth < previousHealth)
        {
            Shoot();
        }

        // Update previousHealth for the next frame
        previousHealth = health.currentHealth;
    }

    void Shoot()
    {
        // Create the ball
        GameObject ball = Instantiate(enemyBall, transform.position, transform.rotation);
        Rigidbody rb = ball.GetComponent<Rigidbody>();

        // Shoot the ball using AddForce
        Vector3 direction = (player.position - transform.position).normalized;
        rb.AddForce(direction * ballSpeed, ForceMode.VelocityChange);

        // Add collision detection and destruction logic
        ball.AddComponent<BallCollisionHandler>();
    }
}

public class BallCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); // Destroy the ball when it collides with the player
        }
    }
}
