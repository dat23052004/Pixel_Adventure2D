using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject panelIngame;
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject panelLose;

    [SerializeField] private TMP_Text textTimer;
    [SerializeField] private TMP_Text textFruitCount;

    [SerializeField] private Button btnNextLevel;
    [SerializeField] private Button btnRetry;

    // 3 star GameObjects trong Panel_Win (index 0=sao1, 1=sao2, 2=sao3)
    [SerializeField] private GameObject[] stars;

    void Awake()
    {
        GameManager.OnPlayerDied    += ShowLose;
        GameManager.OnLevelComplete += ShowWin;
        GameManager.OnFruitChanged  += UpdateFruits;
        GameManager.OnTimerTick     += UpdateTimer;

        btnNextLevel.onClick.AddListener(GameManager.Instance.NextLevel);
        btnRetry.onClick.AddListener(GameManager.Instance.RetryLevel);
    }

    void OnDestroy()
    {
        GameManager.OnPlayerDied    -= ShowLose;
        GameManager.OnLevelComplete -= ShowWin;
        GameManager.OnFruitChanged  -= UpdateFruits;
        GameManager.OnTimerTick     -= UpdateTimer;
    }

    private void ShowIngame()
    {
        panelIngame.SetActive(true);
        panelWin.SetActive(false);
        panelLose.SetActive(false);
    }

    private void ShowWin(int starCount)
    {
        panelIngame.SetActive(false);
        panelWin.SetActive(true);
        panelLose.SetActive(false);

        for (int i = 0; i < stars.Length; i++)
            stars[i].SetActive(i < starCount);
    }

    private void ShowLose()
    {
        panelIngame.SetActive(false);
        panelLose.SetActive(true);
        panelWin.SetActive(false);
    }

    private void UpdateTimer(float seconds)
    {
        int m = Mathf.FloorToInt(seconds / 60f);
        int s = Mathf.FloorToInt(seconds % 60f);
        textTimer.text = string.Format("{0:00}:{1:00}", m, s);

        if (seconds == 0f) ShowIngame();
    }

    private void UpdateFruits(int remaining, int total)
    {
        textFruitCount.text = remaining.ToString();
    }
}
