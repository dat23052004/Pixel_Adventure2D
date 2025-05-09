using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public ParticleSystem ParticleSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Thêm điều kiện cho phím Space
        {
            ParticleSystem.Stop();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            ParticleSystem.Play();
        }
        

    }
}
