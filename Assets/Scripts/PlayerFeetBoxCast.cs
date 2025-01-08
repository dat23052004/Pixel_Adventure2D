using UnityEngine;

public class PlayerFeetBoxCast : MonoBehaviour
{
    public PlayerController controller;

    private void OnTriggerStay2D(Collider2D collision)
    {
        controller.onGround = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        controller.onGround = false;
    }
}
