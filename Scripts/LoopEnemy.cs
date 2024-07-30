using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopEnemy : MonoBehaviour
{
    public GameObject[] enemySlow;
    public GameObject[] enemyNormal;
    public bool createEnemy = false;
    private PlayerMove playerMove;
    public int enemyNumSlow;
    public int enemyNumNor;

    void Start()
    {
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    void Update()
    {
        if (!createEnemy)
        {
            createEnemy = true;
            StartCoroutine(GenerateEnemy());
        }
    }

    IEnumerator GenerateEnemy()
    {
        GameObject player = GameObject.Find("Player");

        if (player != null)
        {
            if (playerMove != null)
            {
                while (true)
                {
                    if (!playerMove.isMoving)
                    {
                        yield return null; // Wait for the next frame and check again
                        continue;
                    }

                    // Calculate player's current position in the world space
                    Vector3 playerPosition = player.transform.position;

                    // Calculate the desired position Z for enemy
                    float playerZ = playerPosition.z + 15; // Add 15 to create enemy ahead of player

                    float delay = 0f;

                    // Check conditions for enemy creation
                    if (playerMove.slow)
                    {
                        enemyNumSlow = Random.Range(0, enemySlow.Length);
                        Quaternion objectRotation = enemySlow[enemyNumSlow].transform.rotation;

                        // Calculate itemPosition using player's exact X and the calculated playerZ
                        Vector3 itemPosition = new Vector3(playerPosition.x, enemySlow[enemyNumSlow].transform.position.y, playerZ);

                        delay = Random.Range(6f, 9f);

                        GameObject instantiatedObject = Instantiate(enemySlow[enemyNumSlow], itemPosition, objectRotation);
                        Destroy(instantiatedObject, Random.Range(3, 5));

                        yield return new WaitForSeconds(delay); // Wait for the calculated delay
                    }
                    else if (playerMove.maximumSpeed >= 5)
                    {
                        enemyNumNor = Random.Range(0, enemyNormal.Length);
                        Quaternion objectRotation = enemyNormal[enemyNumNor].transform.rotation;

                        // Calculate itemPosition using player's exact X and the calculated playerZ
                        Vector3 itemPosition = new Vector3(playerPosition.x, enemyNormal[enemyNumNor].transform.position.y, playerZ);

                        delay = Random.Range(4f, 6f); // Random delay between 5 to 8 seconds

                        Instantiate(enemyNormal[enemyNumNor], itemPosition, objectRotation);

                        yield return new WaitForSeconds(delay); // Wait for the calculated delay
                    }
                }
            }
            else
            {
                Debug.LogError("PlayerMove component not found on Player GameObject.");
            }
        }
        else
        {
            Debug.LogError("Player GameObject not found!");
        }
    }

}