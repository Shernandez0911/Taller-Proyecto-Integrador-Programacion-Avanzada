using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private float cantPuntos;
    [SerializeField] private Score score;

    void Start()
    {
        
    }

    public static bool IsObtained;

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        score.UpdateScore(cantPuntos);
        gameObject.SetActive(false);
    }



}
