using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMiniGame : MonoBehaviour
{
    public Animator animator;
    public GameManager manager;
    private int previousCoin;
    public ParticleSystem getCoin;

    void Start()
    {
        animator = GetComponent<Animator>();
        manager = GameObject.FindObjectOfType<GameManager>();
        previousCoin = manager.playerCoin;
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.playerCoin > previousCoin)
        {
            animator.SetTrigger("punch");
            getCoin.Play();
        }
        else
        {
            animator.SetTrigger("toIdel");
        }

        previousCoin = manager.playerCoin;
    }
}
