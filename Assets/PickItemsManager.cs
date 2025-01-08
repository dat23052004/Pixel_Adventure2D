using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickItemsManager : MonoBehaviour
{
    public GameObject VFXCollected;
    public TextMeshProUGUI scoreNumber;
     int scoreTotal = 0;
    public int scoreOfFruit = 1;
    private void Start()
    {
        UpdateScore(0);
    }
    private void Update()
    {
        
    }
    public void UpdateScore(int addValue)
    {
        scoreTotal += addValue;
        scoreNumber.text = scoreTotal.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Pickable")
        {
            scoreTotal = scoreTotal + scoreOfFruit;
            UpdateScore(scoreOfFruit);
            Instantiate(VFXCollected, collision.gameObject.transform.position, Quaternion.identity,null);
            Destroy(collision.gameObject);
        }
    }
}
