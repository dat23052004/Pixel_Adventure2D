using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeetBox : MonoBehaviour
{
    public Player_Controller controller;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            controller.onGround = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            controller.onGround = false;

        }
    }

}
