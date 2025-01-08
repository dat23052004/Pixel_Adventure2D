using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StopMusic : MonoBehaviour
{
    private void Awake()
    {
        MusicManager musicManagerComponent = FindAnyObjectByType<MusicManager>();
        if (musicManagerComponent != null)
        {
            Destroy(musicManagerComponent.gameObject);
        }
    }

}
