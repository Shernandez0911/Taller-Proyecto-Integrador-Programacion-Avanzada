using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomlessPit : MonoBehaviour
{
    public static bool IsBottomlessPit = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BottomlessPit"))
        {
            IsBottomlessPit = true;
        }
    }
}
