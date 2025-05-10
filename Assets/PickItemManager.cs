using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItemManager : MonoBehaviour
{
    public GameObject VFXCollected;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pickable"))
        {
            Instantiate(VFXCollected,collision.gameObject.transform.position,Quaternion.identity,null);
            Destroy(collision.gameObject);
        }
    }
}
