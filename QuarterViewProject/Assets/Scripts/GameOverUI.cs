using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI timeText;
    [SerializeField]
    TextMeshProUGUI enemyText;
    [SerializeField]
    TextMeshProUGUI rankText;


    public void GameOverUISetting(float time, int enemy)
    {
        timeText.text = string.Format("{0:F2}", time) + "S";
        enemyText.text = enemy.ToString() + " ENEMIES";

        if(time > 60)
        {
            rankText.text = "Well Done!!";
            rankText.color = Color.HSVToRGB(180f / 360f, 1f, 1f);
        }

        else if(time > 40)
        {
            rankText.text = "Good Job!";
            rankText.color = Color.HSVToRGB(110f / 360f, 1f, 1f);
        }

        else if(time > 20)
        {
            rankText.text = "SO-SO";
            rankText.color = Color.HSVToRGB(60f / 360f, 1f, 1f);
        }

        else
        {
            rankText.text = "Cheer Up...";
            rankText.color = Color.HSVToRGB(0f, 0f, 0.5f);
        }
    }

}
