using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public float Velocidad = 5f;
    public float FuerzaSalto = 100f;

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
        }
        if (Input.GetKey("up") && CheckGround.IsGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, FuerzaSalto);
        }
        if (Input.GetKey("z") && CheckGround.IsGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, FuerzaSalto);
        }
    }
}
