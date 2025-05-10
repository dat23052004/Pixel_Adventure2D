using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScroll : MonoBehaviour
{
    public float scrollSpeed = 2f;
    public float backgroundHeight = 10f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position += Vector3.down * scrollSpeed * Time.deltaTime;

        if (transform.position.y <= -backgroundHeight)
        {
            transform.position += new Vector3(0, backgroundHeight * 2f, 0);
        }
    }
}
