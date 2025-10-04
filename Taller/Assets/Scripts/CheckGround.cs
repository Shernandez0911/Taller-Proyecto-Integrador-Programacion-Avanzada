using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public static bool IsGround;
    public static bool IsEnemy;

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        IsGround = true;
        if (collision.CompareTag("Enemy"))
        {
            IsEnemy = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IsGround = false;
    }

}
