using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHPBar : MonoBehaviour
{
    public RectTransform RectTransform;
    int HPBarWidthMax = 1000;
    int PlayerHPMax = 100;

    public TextMeshProUGUI hpAmount;
    public TextMeshPro playerHPAmount;
    public void updatePlayerHPBar(int playerHP)
    {
        float newHPBarWidth = playerHP * HPBarWidthMax/PlayerHPMax;
        RectTransform.sizeDelta = new Vector2(newHPBarWidth, RectTransform.sizeDelta.y);
        hpAmount.text = playerHP + "/" + PlayerHPMax;
        playerHPAmount.text = playerHP + "/" + PlayerHPMax;
    }
}
