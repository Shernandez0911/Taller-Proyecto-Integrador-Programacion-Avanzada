using UnityEngine;

public class CheckGroundDobleSalto1 : MonoBehaviour
{
    [HideInInspector] public bool isGround;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.CompareTag("Ground"))
            isGround = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.CompareTag("Ground"))
            isGround = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.CompareTag("Ground"))
            isGround = false;
    }
}
