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
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    int timeWeight;
    [SerializeField]
    int enemyWeight;

    
    public void GameOverUISetting(float time, int enemy)
    {
        float score = time * timeWeight + enemy * enemyWeight;
        timeText.text = string.Format("{0:F2}", time) + "S";
        enemyText.text = enemy.ToString() + " ENEMIES";
        int resultScore = (int)score;
        scoreText.text = string.Format("{0:#,###}", resultScore);

        if(resultScore > 60000)
        {
            rankText.text = "Well Done!!";
            rankText.color = Color.HSVToRGB(180f / 360f, 1f, 1f);
        }

        else if(resultScore > 40000)
        {
            rankText.text = "Good Job!";
            rankText.color = Color.HSVToRGB(110f / 360f, 1f, 1f);
        }

        else if(resultScore > 20000)
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

    public int GetScore(float time, int enemy) 
    {
        float score = time * timeWeight + enemy * enemyWeight;
        int resultScore = (int)score;
        return resultScore;
    }

}
