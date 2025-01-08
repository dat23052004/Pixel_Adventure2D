using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapOnCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Debug.Log("collision");
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.PlayerHealthPointUpdate(-1);
            
        }
    }

}
