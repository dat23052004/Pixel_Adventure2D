using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidbody2DComponent;
    public List<GameObject> paths;
    public float moveSpeed = 2f;
    int currentIndex = 0;
    GameObject currentPath;
    public float waitTimes = 1;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2DComponent = GetComponent<Rigidbody2D>();
        currentPath = paths[currentIndex];
    }

    private void Start()
    {
        StartCoroutine(MoveTo());
    }

    IEnumerator MoveTo()
    {
        rigidbody2DComponent.velocity = Vector2.zero;
        animator.Play("Enemy Idle");
        yield return new WaitForSeconds(waitTimes);
        var directionToPath = Direction2point2D(transform.position, currentPath.transform.position);
        rigidbody2DComponent.velocity = directionToPath * moveSpeed;

        RotateToPath(directionToPath);
        animator.Play("Enemy Walk");

    }

    private void RotateToPath(Vector2 directionToPath)
    {
        if(directionToPath.x >= 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    Vector2 Direction2point2D(Vector2 point1, Vector2 point2)
    {
        var direction2D = point2 - point1;
        return direction2D.normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == currentPath)
        {
            ChangPlatformDirectionMovement();
        }
    }

    private void ChangPlatformDirectionMovement()
    {
        currentIndex++;
        if (currentIndex >= paths.Count)
        {
            currentIndex = 0;
        }
        currentPath = paths[currentIndex];
        StartCoroutine(MoveTo());
    }
}
