using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public Animator animator;
    private PlayerMove playerMove;
    public Health health; // Assuming you have a Health class with currentHealth
    private int previousHealth;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMove = GameObject.FindObjectOfType<PlayerMove>();
        health = GameObject.FindObjectOfType<Health>(); // Adjust based on your actual implementation
        previousHealth = health.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMove.warningIcon.activeInHierarchy)
        {
            // warningIcon is active
            animator.SetBool("idle", false);
            animator.SetBool("holding", true);
        }
        else
        {
            // warningIcon is not active
            animator.SetBool("idle", true);
            animator.SetBool("holding", false);
        }

        // Check if health has decreased
        if (health.currentHealth < previousHealth)
        {
            animator.SetBool("punching", true);
        }
        else
        {
            animator.SetBool("punching", false);
        }

        // Update previousHealth for the next frame
        previousHealth = health.currentHealth;
    }
}
