using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida del jugador")]
    public int maxHealth = 5;
    private int currentHealth;
    private Animator animator;
    private Rigidbody2D rb;
    private PlayerMove move;

    private bool isInvulnerable = false;
    public float invulnerabilityTime = 0.5f;


    private bool isDead = false;
    public AudioClip sonidoDaño;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (animator == null)
            Debug.LogError("❌ Animator no encontrado en PlayerHealth");
        else
            Debug.Log("✅ Animator encontrado correctamente");

        // Fuerza los valores iniciales aunque el prefab tenga datos viejos
        maxHealth = Mathf.Max(maxHealth, 1);
        currentHealth = maxHealth;
        isDead = false;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        move = GetComponent<PlayerMove>();


        Debug.Log($"🏁 Vida inicial configurada: {currentHealth}/{maxHealth}");
    }
    public void ReproducirDaño()
    {
        if (sonidoDaño != null)
        {
            audioSource.PlayOneShot(sonidoDaño);
        }
    }
    void Awake()
    {
        void Awake()
        {
            Debug.Log("🧠 PlayerHealth Awake() ejecutado en " + gameObject.name);
        }

        currentHealth = maxHealth;
        Debug.Log($"💙 Vida inicial configurada en Awake(): {currentHealth}/{maxHealth}");
    }
    void Update() {
        if (currentHealth < 0 || currentHealth > maxHealth)
        {
            Debug.LogWarning($"⚠️ Valor extraño en currentHealth: {currentHealth}");
        }
    }


    public void TakeDamage(int damage)
    {
        if (isDead || isInvulnerable)
        {
            Debug.Log("⚠️ Intento de daño pero el jugador ya está muerto o invulnerable");
            return;
        }

        if (currentHealth <= 0)
        {
            Debug.LogWarning("⚠️ Intento de daño con vida <= 0, ignorado");
            return;
        }

        Debug.Log($"💥 Daño recibido: {damage} | Vida actual antes del golpe: {currentHealth}");

        currentHealth -= damage;

        Debug.Log($"❤️ Vida después del golpe: {currentHealth}");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("☠️ Vida llegó a 0 → Ejecutando Die()");
            Die();
        }
        else
        {
            Debug.Log("🤕 Ejecutando animación Got_Hit");
            animator.SetTrigger("Got_Hit");
            ReproducirDaño();
            Debug.Log($"Trigger Got_Hit activado en animator: {animator.isInitialized}");
            StartCoroutine(InvulnerabilityFrames());
        }
    }

    private IEnumerator InvulnerabilityFrames()
    {
        isInvulnerable = true;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (sr != null)
        {
            float elapsed = 0f;
            float blinkSpeed = 0.1f; // velocidad del parpadeo

            while (elapsed < invulnerabilityTime)
            {
                sr.enabled = !sr.enabled; // alterna visibilidad
                yield return new WaitForSeconds(blinkSpeed);
                elapsed += blinkSpeed;
            }

            sr.enabled = true; // asegúrate de dejarlo visible al final
        }

        isInvulnerable = false;
    }


    private void Die()
    {
        if (isDead) return;
        isDead = true;
        ReproducirDaño();
        ReproducirDaño();

        Debug.Log("💀 El jugador ha muerto");

        // Bloquear movimiento inmediatamente
        if (move != null)
        {
            move.puedeMoverse = false;
        }

        // Congelar físicas
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        // Animación de muerte
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
    }

}

