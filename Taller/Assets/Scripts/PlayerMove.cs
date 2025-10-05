using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public float Velocidad = 5f;
    public float FuerzaSalto = 8f;
    public float gravedadExtra = 1.2f;
    public bool estaSaltando;

    [HideInInspector] public bool puedeMoverse = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 🔒 Si no puede moverse, forzar que quede inmóvil y salir
        if (!puedeMoverse)
        {
            animator.SetBool("isWalking", false);
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            return;
        }
        // --- Movimiento horizontal ---
        if (Input.GetKey("right"))
        {
            animator.SetBool("isWalking", true);
            transform.localScale = new Vector2(1, 1);
        }
        else if (Input.GetKey("left"))
        {
            animator.SetBool("isWalking", true);
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // --- Saltos (solo si está en suelo) ---
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z)) && CheckGround.IsGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, FuerzaSalto);
            estaSaltando = true;
        }

        // --- Salto corto ---
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.UpArrow)) && estaSaltando)
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
            estaSaltando = false;
        }

        // --- Caída rápida manual (opcional) ---
        if (Input.GetKeyDown(KeyCode.DownArrow) && !CheckGround.IsGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - 3f);
        }
    }

    private void FixedUpdate()
    {
        // 🔒 Evitar que se mueva si está muerto o congelado
        if (!puedeMoverse)
        {
            animator.SetBool("isWalking", false);
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            return;
        }


        // --- Movimiento físico (velocidad constante) ---
        if (Input.GetKey("right"))
        {
            rb.velocity = new Vector2(Velocidad, rb.velocity.y);
        }
        else if (Input.GetKey("left"))
        {
            rb.velocity = new Vector2(-Velocidad, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // --- Gravedad extra ---
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (gravedadExtra - 1) * Time.deltaTime;
        }

        // --- Caída infinita ---
        if (BottomlessPit.IsBottomlessPit)
        {
            gameObject.SetActive(false);
        }
    }
}
