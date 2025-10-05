using UnityEngine;

public class SlimeDamage : MonoBehaviour
{
    public float health = 3;
    private SlimeGeneral slimeGeneral;
    public AudioClip sonidoDaño;
    public AudioClip sonidoDaño2;
    private AudioSource audioSource;

    void Start()
    {

        slimeGeneral = GetComponentInParent<SlimeGeneral>();
        audioSource = GetComponent<AudioSource>();

        if (slimeGeneral == null)
        {
        }
    }
    public void ReproducirDaño1()
    {
        if (sonidoDaño != null)
        {
            audioSource.PlayOneShot(sonidoDaño);
        }
    }
    public void ReproducirDaño2()
    {
        if (sonidoDaño2 != null)
        {
            audioSource.PlayOneShot(sonidoDaño2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            health -= 1;

            if (health <= 0)
            {
                ReproducirDaño2();
                slimeGeneral.OnDefeated();

            }
            else
            {
                ReproducirDaño1();
                slimeGeneral.OnHit();
            }
        }
    }
}
