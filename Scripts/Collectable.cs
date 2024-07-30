using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public GameManager manager;
    public Health heart;
    public int score = 100;
    public int trash = 75;
    public int coin = 1;

    private void Start()
    {
        manager = GameObject.FindObjectOfType<GameManager>();
        heart = GameObject.FindObjectOfType<Health>();
    }

    private void Update()
    {
        transform.Rotate(0f, 50 * Time.deltaTime, 0f, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (gameObject.tag == "Score")
            {
                manager.playerScore += score;
            }
            else if (gameObject.tag == "Coin")
            {
                manager.playerCoin += coin;
            }
            else if (gameObject.tag == "Trash")
            {
                if (manager.playerScore - trash < 0)
                {
                    manager.playerScore = 0;
                }
                else
                {
                    manager.playerScore -= trash;
                }
            }
            else if (gameObject.tag == "Toxic")
            {
                heart.currentHealth--;
            }
            else if (gameObject.tag == "Heal")
            {
                if (heart.currentHealth < 3)
                {
                    heart.currentHealth++;
                }
                else if (heart.currentHealth == 3)
                {
                    manager.playerScore += score;
                }
            }
            else if (gameObject.tag == "BonusTime")
            {
                manager.StartBonusTime();
            }
            else if (gameObject.tag == "BonusTimeItem") // Add this condition
            {
                // Handle bonus time item collection
                manager.playerScore += score; // Or some other bonus score calculation
                manager.playerCoin += coin;
            }
            Destroy(gameObject);
        }
    }
}
