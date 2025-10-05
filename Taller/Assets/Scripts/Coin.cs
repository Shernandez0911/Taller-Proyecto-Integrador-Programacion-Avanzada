using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float cantPuntos;
    [SerializeField] private Score score;
    private bool alreadyCollected = false;
    public AudioClip sonidoMoneda; // asignas el clip desde el inspector
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static bool IsObtained;

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!alreadyCollected && collision.CompareTag("Player"))
        {
            alreadyCollected = true;
            score.UpdateScore(cantPuntos);
            StartCoroutine(RecogerMoneda());
        }

    }
    private IEnumerator RecogerMoneda()
    {
        ReproducirMoneda();

        // ⏳ Esperar la duración del sonido antes de desactivar el objeto
        yield return new WaitForSeconds(sonidoMoneda.length);

        gameObject.SetActive(false);
    }
    public void ReproducirMoneda()
    {
        if (sonidoMoneda != null)
        {
            audioSource.PlayOneShot(sonidoMoneda);
        }
    }



}