using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovedoblesalto : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private CheckGroundDobleSalto1 checkGround;

    public float Velocidad = 5f;
    public float FuerzaSalto = 8f;
    public float gravedadExtra = 1.2f;
    public bool estaSaltando;

    private int contadorSaltos = 0;
    public int maxSaltos = 2;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        checkGround = GetComponentInChildren<CheckGroundDobleSalto1>();
    }

    void Update()
    {
        // --- Movimiento lateral ---
        float move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(move * Velocidad, rb.velocity.y);

        if (move != 0)
        {
            animator.SetBool("isWalking", true);
            transform.localScale = new Vector2(Mathf.Sign(move), 1);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // --- Reinicio de saltos ---
        if (checkGround != null && checkGround.isGround)
        {
            contadorSaltos = 0;
            estaSaltando = false;
        }

        // --- Salto / Doble salto ---
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z))
            && contadorSaltos < maxSaltos)
        {
            rb.velocity = new Vector2(rb.velocity.x, FuerzaSalto);
            estaSaltando = true;
            contadorSaltos++;
        }

        // --- Salto corto ---
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.UpArrow)) && estaSaltando)
        {
            if (rb.velocity.y > 0)
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        // --- Caída rápida ---
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (gravedadExtra - 1) * Time.deltaTime;
        }

        // --- Caída en pozo ---
        if (BottomlessPit.IsBottomlessPit)
        {
            gameObject.SetActive(false);
        }
    }
}
