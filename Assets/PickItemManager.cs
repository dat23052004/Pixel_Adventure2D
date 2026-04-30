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
        //todo 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Constant.TAG_PICKABLE))
        {
            Instantiate(VFXCollected, collision.gameObject.transform.position, Quaternion.identity, null);
            collision.gameObject.SetActive(false);
            GameManager.Instance.FruitCollected();
        }
    }
}
