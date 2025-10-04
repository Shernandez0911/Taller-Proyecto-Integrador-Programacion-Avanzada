using System.Collections;
using UnityEngine;

public class SlimeGeneral : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private bool isDying = false;
    private bool isHit = false;
    private bool isInvulnerable = false;


    public float Velocidad = 2f;
    public bool isWalking = true;

    public float invulnerabilityDuration = 1.0f;
    public float blinkInterval = 0.1f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator.SetBool("IsWalking", isWalking);
    }

    void Update()
    {

        if (isWalking && !isDying && !isHit)
        {
            rb.velocity = new Vector2(Velocidad, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }


    public void OnHit()
    {
        if (isDying || isHit || isInvulnerable) return;

        isHit = true;
        animator.SetBool("IsWalking", false);
        animator.SetTrigger("Got_Hit");
        StartCoroutine(RecoverFromHit());
    }


    public void OnDefeated()
    {
        if (isDying) return;

        isDying = true;
        animator.SetBool("IsWalking", false);
        animator.SetTrigger("Got_Defeated");
        StartCoroutine(Die());
    }

    private IEnumerator RecoverFromHit()
    {

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        isHit = false;
        StartCoroutine(BecomeInvulnerable());
        animator.SetBool("IsWalking", true); 
    }

    private IEnumerator Die()
    {

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }
    private IEnumerator BecomeInvulnerable()
    {
        isInvulnerable = true;
        float elapsed = 0f;

        while (elapsed < invulnerabilityDuration)
        {

            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }


        spriteRenderer.enabled = true;
        isInvulnerable = false;
    }
}
