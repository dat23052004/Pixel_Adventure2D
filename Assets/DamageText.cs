using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed = .5f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = new Vector2(0, 1)*moveSpeed;
        Destroy(this.gameObject,2);
    }
}
