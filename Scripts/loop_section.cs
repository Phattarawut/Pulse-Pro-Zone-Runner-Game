using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class loop_section : MonoBehaviour
{
    public GameObject[] section;
    public GameObject[] collection;
    public int zPosS;
    public int zPosC;
    public bool createSection = false;
    public bool createCollectable = false;
    public int secNum;
    public int collNum;

    private float lastCollectionSpawnTime;
    private float spawnInterval;
    public bool isBonusTime = false;
    public float bonusTimeEnd = 0f;
    private PlayerMove playerMove;

    void Start()
    {
        lastCollectionSpawnTime = Time.time;
        spawnInterval = Time.time;
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    void Update()
    {
        if (!createCollectable)
        {
            createCollectable = true;
            StartCoroutine(GenerateCollection());
        }

        if (!createSection)
        {
            createSection = true;
            StartCoroutine(GenerateSection());
        }

        if (createCollectable && Time.time - lastCollectionSpawnTime >= Random.Range(6, 15))
        {
            StartCoroutine(GenerateCollection());
        }

        // Check if bonus time has ended
        if (isBonusTime && Time.time >= bonusTimeEnd)
        {
            isBonusTime = false;
            DestroyAllBonusItems();
        }
    }

    IEnumerator GenerateSection()
    {
        int newSecNum;

        do
        {
            newSecNum = Random.Range(0, 3);
        } while (newSecNum == secNum);

        secNum = newSecNum;

        Instantiate(section[secNum], new Vector3(0, 0, zPosS), Quaternion.identity);
        zPosS += 25;
        yield return new WaitForSeconds(3);
        createSection = false;
    }

    IEnumerator GenerateCollection()
    {
        GameObject player = GameObject.Find("Player");

        if (player != null)
        {
            if (playerMove != null)
            {
                if (!playerMove.isMoving)
                {
                    yield break;
                }

                float playerZ = player.transform.position.z + 10;
                Vector3 itemPosition = new Vector3(2.4f, 1, playerZ);
                float destroyTime = Random.Range(3, 5);

                if (playerMove.slow == true && Time.time - lastCollectionSpawnTime >= spawnInterval)
                {
                    collNum = Random.Range(0, 3);
                    Quaternion objectRotation = collection[collNum].transform.rotation;

                    GameObject instantiatedObject = Instantiate(collection[collNum], itemPosition, objectRotation);
                    Destroy(instantiatedObject, destroyTime);

                    lastCollectionSpawnTime = Time.time;
                    spawnInterval = Random.Range(4, 6);
                }

                if (playerMove.maximumSpeed >= 5 && (isBonusTime || Time.time - lastCollectionSpawnTime >= Random.Range(6, 15)))
                {
                    if (isBonusTime)
                    {
                        collNum = Random.Range(0, 3);
                        for (int i = 0; i < 3; i++)
                        {
                            Quaternion objectRotation = collection[collNum].transform.rotation;

                            GameObject bonusItem = Instantiate(collection[collNum], itemPosition, objectRotation);
                            bonusItem.tag = "BonusTimeItem";
                            itemPosition.z += 1.5f;
                        }
                        lastCollectionSpawnTime = Time.time + Random.Range(5, 7);
                    }
                    else
                    {
                        collNum = Random.Range(0, 16);
                        Quaternion objectRotation = collection[collNum].transform.rotation;
                        Instantiate(collection[collNum], itemPosition, objectRotation);
                        zPosC += 25;
                        lastCollectionSpawnTime = Time.time;
                    }
                }

                yield return null;
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


    public void StartBonusTime()
    {
        isBonusTime = true;
        bonusTimeEnd = Time.time + 20f; // Bonus time lasts for 20 seconds
        StartCoroutine(SpawnBonusItems());
    }

    private IEnumerator SpawnBonusItems()
    {
        while (isBonusTime)
        {
            if (playerMove != null && playerMove.isMoving)
            {
                GameObject player = GameObject.Find("Player");
                float playerZ = player.transform.position.z + 10;
                float randomY = Random.Range(1, 3);
                Vector3 itemPosition = new Vector3(2.4f, randomY, playerZ);

                collNum = Random.Range(0, 3);
                for (int i = 0; i < 3; i++)
                {
                    Quaternion objectRotation = collection[collNum].transform.rotation;

                    GameObject bonusItem = Instantiate(collection[collNum], itemPosition, objectRotation);
                    bonusItem.tag = "BonusTimeItem"; // Add this line
                    itemPosition.z += 1.5f; // Adjust the z position for each subsequent item
                }
            }
            yield return new WaitForSeconds(3);
        }
    }

    private void DestroyAllBonusItems()
    {
        GameObject[] bonusItems = GameObject.FindGameObjectsWithTag("BonusTimeItem");
        foreach (GameObject item in bonusItems)
        {
            Destroy(item);
        }
    }
}