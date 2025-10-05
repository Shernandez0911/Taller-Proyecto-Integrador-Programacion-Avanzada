using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private float puntos;

    private TextMeshProUGUI text;
    
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        text.text = puntos.ToString("0");
    }

    public void UpdateScore(float puntajeIncial)
    {
        puntos += puntajeIncial;
        
    }

<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
}
