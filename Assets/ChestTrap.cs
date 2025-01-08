using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrap : MonoBehaviour
{
    public GameObject enemy;

    private void CreateEnemy()
    {
        Instantiate(enemy,this.transform.position,Quaternion.identity,null);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            CreateEnemy();
        }
    }
}
