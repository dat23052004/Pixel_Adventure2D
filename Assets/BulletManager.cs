using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{

    public GameObject hitEffect;
    public float damage;
    private void Start()
    {
        StartCoroutine(DestroyTime());
    }

    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
        CreateHitEffect();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealthBarManager>().UpdateHealth(-1 * damage);
        }
         Destroy(this.gameObject);
        CreateHitEffect();

    }

    void CreateHitEffect()
    {
        Instantiate(hitEffect, transform.position, Quaternion.identity, null);
    }
}
