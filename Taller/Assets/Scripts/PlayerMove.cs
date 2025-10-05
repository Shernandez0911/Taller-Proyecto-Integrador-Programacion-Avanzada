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





    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate() {
        if (Input.GetKey("right"))
        {
            animator.SetBool("isWalking", true);
            rb.velocity = new Vector2(Velocidad,rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        else if (Input.GetKey("left"))
        {
            animator.SetBool("isWalking", true);
            rb.velocity = new Vector2(-Velocidad, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        else {
            animator.SetBool("isWalking", false);
            transform.localScale = new Vector2(1, 1);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (Input.GetKey("space") && CheckGround.IsGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, FuerzaSalto);
            estaSaltando = true;
        }
        if (Input.GetKey("up") && CheckGround.IsGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, FuerzaSalto);
            estaSaltando = true;
        }
        if (Input.GetKey("z") && CheckGround.IsGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, FuerzaSalto);
            estaSaltando = true;
        }
        if ((Input.GetKeyUp("space") || Input.GetKeyUp("z") || Input.GetKeyUp("up")) && estaSaltando)
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
            estaSaltando = false;
        }
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (gravedadExtra - 1) * Time.deltaTime;
        }
        if (Input.GetKeyDown("down") && !CheckGround.IsGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - 3f); // impulso inicial
        }

        if (Input.GetKey("down") && !CheckGround.IsGround)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (gravedadExtra * 2f) * Time.deltaTime;
        }

        if (BottomlessPit.IsBottomlessPit == true)
        {
            gameObject.SetActive(false);
        }
    }

    }
