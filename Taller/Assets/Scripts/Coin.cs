using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private float cantPuntos;
    [SerializeField] private Score score;
    private bool alreadyCollected = false;

    void Start()
    {

    }

    public static bool IsObtained;

    // Update is called once per frame
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