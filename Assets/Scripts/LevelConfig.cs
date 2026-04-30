using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelConfig
{
    public Transform spawnPoint;
    public Vector3 cameraPosition;      // vị trí cố định của camera cho map này
    public float cameraSize = 5f;       // orthographic size để vừa cả màn
    public List<GameObject> fruits;
}
