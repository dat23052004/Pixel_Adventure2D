using UnityEngine;

public class BouncingBulletManager : MonoBehaviour
{
    int touchCount = 0;
    public GameObject hitEffect;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        touchCount++;   
        if(touchCount >= 3)
        {
            DestroyAndEffect();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag== "Enemy")
        {
            DestroyAndEffect();
        }
    }
    void DestroyAndEffect()
    {
        Instantiate(hitEffect, transform.position, Quaternion.identity, null);
        Destroy(this.gameObject);
    }
}
