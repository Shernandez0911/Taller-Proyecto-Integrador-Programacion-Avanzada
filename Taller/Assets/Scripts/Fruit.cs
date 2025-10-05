using System.Collections;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private float cantPuntos;
    [SerializeField] private Score score;
    private bool alreadyCollected = false;
    public AudioClip sonidoFruta;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!alreadyCollected && collision.CompareTag("Player"))
        {
            alreadyCollected = true;

            // 🍎 Reproducir sonido de fruta (aunque el objeto se desactive)
            if (sonidoFruta != null)
            {
                AudioSource.PlayClipAtPoint(sonidoFruta, transform.position,1.5f);
            }

            // 🧮 Actualizar puntaje
            score.UpdateScore(cantPuntos);

            // 💨 Desactivar la fruta del escenario
            gameObject.SetActive(false);
        }
    }
}
