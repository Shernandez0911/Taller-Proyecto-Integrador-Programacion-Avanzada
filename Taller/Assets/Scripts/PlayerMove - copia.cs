using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove1 : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public float Velocidad = 5f;
    public float FuerzaSalto = 8f;
    public float gravedadExtra = 1.2f;
    public bool estaSaltando;

    private int contadorSaltos = 0; // 👈 Contador de saltos
    public int maxSaltos = 2;       // 👈 Cuántos saltos permitidos (2 = doble salto)

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Reiniciamos el contador al tocar el suelo
        if (CheckGround.IsGround)
        {
            contadorSaltos = 0;
            estaSaltando = false;
        }
    }

    private void FixedUpdate()
    {
        // --- Movimiento lateral ---
        if (Input.GetKey("right"))
        {
            animator.SetBool("isWalking", true);
            rb.velocity = new Vector2(Velocidad, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        else if (Input.GetKey("left"))
        {
            animator.SetBool("isWalking", true);
            rb.velocity = new Vector2(-Velocidad, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            animator.SetBool("isWalking", false);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // --- Salto y doble salto ---
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z))
            && contadorSaltos < maxSaltos)
        {
            rb.velocity = new Vector2(rb.velocity.x, FuerzaSalto);
            estaSaltando = true;
            contadorSaltos++; // 👈 Aumenta el número de saltos usados
        }

        // --- Control del salto corto ---
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.UpArrow)) && estaSaltando)
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }

        // --- Caída más rápida ---
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (gravedadExtra - 1) * Time.deltaTime;
        }

        // --- Muerte por caída ---
        if (BottomlessPit.IsBottomlessPit)
        {
            gameObject.SetActive(false);
        }
    }
}
