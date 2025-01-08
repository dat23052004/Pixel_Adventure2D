using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static MusicManager Singleton { get; private set; }

    private void Awake()
    {
        if(Singleton != null &&  Singleton != null)
        {
            Destroy(Singleton.gameObject);
        }
        else
        { 
            Singleton = this;
        }
    }
}
