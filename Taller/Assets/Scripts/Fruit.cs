using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float cantPuntos;
    [SerializeField] private Score score;
    private bool alreadyCollected = false;

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public static bool IsObtained;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!alreadyCollected && collision.CompareTag("Player"))
        {
            alreadyCollected = true;
            score.UpdateScore(cantPuntos);
            gameObject.SetActive(false);
        }
    }
}