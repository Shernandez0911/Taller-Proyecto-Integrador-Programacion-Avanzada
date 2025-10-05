using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : MonoBehaviour
{
    public int damage = 1;

    private bool hasDealtDamage = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasDealtDamage) return;

        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                hasDealtDamage = true;
                StartCoroutine(ResetHit());
            }
        }
    }

    private IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(0.5f); // vuelve a poder dañar después de medio segundo
        hasDealtDamage = false;
    }

}
