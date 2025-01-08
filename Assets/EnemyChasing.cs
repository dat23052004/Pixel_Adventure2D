using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasing : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    GameObject playerObject;
    public float moveSpeed = 1f;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        StartCoroutine(ChasePlayer());
    }

    IEnumerator ChasePlayer()
    {
        Vector2 directToPlayer = Direction2Points2D(this.transform.position, playerObject.transform.position);
        rigidbody2D.velocity = directToPlayer*moveSpeed;

        yield return new WaitForSeconds(1);
        StartCoroutine(ChasePlayer());
    }

    Vector2 Direction2Points2D(Vector2 point1, Vector2 point2)
    {
        var Direction = point2 - point1;
        return Direction.normalized;
    }
}
