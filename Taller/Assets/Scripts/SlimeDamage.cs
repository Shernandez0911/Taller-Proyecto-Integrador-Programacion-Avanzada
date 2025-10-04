using UnityEngine;

public class SlimeDamage : MonoBehaviour
{
    public float health = 3;
    private SlimeGeneral slimeGeneral;

    void Start()
    {

        slimeGeneral = GetComponentInParent<SlimeGeneral>();

        if (slimeGeneral == null)
        {
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            health -= 1;

            if (health <= 0)
            {
                slimeGeneral.OnDefeated();
            }
            else
            {
                slimeGeneral.OnHit();
            }
        }
    }
}
