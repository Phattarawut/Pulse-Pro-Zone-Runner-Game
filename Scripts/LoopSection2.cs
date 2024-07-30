using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoopSection2 : MonoBehaviour
{
    public GameObject[] section;
    public float zPosS;
    public bool createSection = false;
    public float sNext;

    void Start()
    {
        // Initialize your variables if needed
    }

    void Update()
    {
        if (!createSection)
        {
            createSection = true;
            StartCoroutine(GenerateSection());
        }
    }

    IEnumerator GenerateSection()
    {
        int randomIndex = Random.Range(0, section.Length);
        Instantiate(section[randomIndex], new Vector3(0, 0, zPosS), Quaternion.identity);
        zPosS += sNext;
        yield return new WaitForSeconds(3);
        createSection = false;
    }
}