using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingPlatform : MonoBehaviour
{
    [Header("Puntos de movimiento")]
    public Transform puntoA;
    public Transform puntoB;

    [Header("Configuración")]
    public float velocidad = 2f;
    public float tiempoEspera = 1f;

    private Rigidbody2D rb;
    private bool yendoAB = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate; // suaviza el movimiento

        StartCoroutine(Mover());
    }

    private IEnumerator Mover()
    {
        while (true)
        {
            Vector3 inicio = yendoAB ? puntoA.position : puntoB.position;
            Vector3 fin = yendoAB ? puntoB.position : puntoA.position;

            float distancia = Vector3.Distance(inicio, fin);
            float duracion = distancia / velocidad;
            float t = 0f;

            // Movimiento suave
            while (t < 1f)
            {
                t += Time.fixedDeltaTime / duracion;
                Vector3 nuevaPos = Vector3.Lerp(inicio, fin, Mathf.SmoothStep(0f, 1f, t));
                rb.MovePosition(nuevaPos);
                yield return new WaitForFixedUpdate();
            }

            // Esperar arriba o abajo
            yield return new WaitForSeconds(tiempoEspera);

            yendoAB = !yendoAB;
        }
    }

    // Permitir que el jugador viaje con la plataforma
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.SetParent(null);
    }
}
