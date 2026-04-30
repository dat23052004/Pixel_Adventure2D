using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static event Action OnPlayerDied;
    public static event Action<int> OnLevelComplete;   // tham số: số sao (1-3)
    public static event Action<int, int> OnFruitChanged;
    public static event Action<float> OnTimerTick;

    [SerializeField] private LevelConfig[] levels;
    [SerializeField] private Player_Controller player;
    [SerializeField] private Camera mainCamera;

    private int currentLevel = 0;
    private float timer = 0f;
    private int totalFruits = 0;
    private int fruitsCollected = 0;
    private bool isActive = false;
    private bool isDead = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        StartLevel(0);
    }

    void Update()
    {
        if (!isActive || isDead) return;
        timer += Time.deltaTime;
        OnTimerTick?.Invoke(timer);
    }

    public void StartLevel(int index)
    {
        currentLevel = Mathf.Clamp(index, 0, levels.Length - 1);
        isDead = false;
        timer = 0f;
        fruitsCollected = 0;

        LevelConfig cfg = levels[currentLevel];
        Vector3 spawnPoint = cfg.spawnPoint.position + new Vector3(.8f,0f,0);
        player.Teleport(spawnPoint);
        player.inputEnabled = true;

        foreach (GameObject fruit in cfg.fruits)
            if (fruit != null) fruit.SetActive(true);

        totalFruits = cfg.fruits.Count;
        isActive = true;

        if (mainCamera != null)
        {
            mainCamera.transform.position = cfg.cameraPosition;
            mainCamera.orthographicSize = cfg.cameraSize;
        }

        OnFruitChanged?.Invoke(totalFruits, totalFruits);
        OnTimerTick?.Invoke(0f);
    }

    public void FruitCollected()
    {
        if (!isActive) return;
        fruitsCollected++;
        int remaining = totalFruits - fruitsCollected;
        OnFruitChanged?.Invoke(remaining, totalFruits);
    }

    public void PlayerDied()
    {
        if (isDead || !isActive) return;
        isDead = true;
        isActive = false;
        player.inputEnabled = false;
        OnPlayerDied?.Invoke();
    }

    // Gọi từ EndPoint khi player chạm vào điểm đích
    public void ReachEndPoint()
    {
        if (!isActive || isDead) return;
        isActive = false;
        OnLevelComplete?.Invoke(CalculateStars());
    }

    public void RetryLevel()
    {
        fruitsCollected = 0;
        StartLevel(currentLevel);
    }

    public void NextLevel()
    {
        StartLevel(currentLevel + 1);
    }

    private int CalculateStars()
    {
        if (totalFruits == 0) return 3;
        float ratio = (float)fruitsCollected / totalFruits;
        if (ratio >= 1f) return 3;
        if (ratio >= 0.3f) return 2;
        return 1;
    }
}
