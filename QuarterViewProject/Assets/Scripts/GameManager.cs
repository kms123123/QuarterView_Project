using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;
    [SerializeField]
    TextMeshProUGUI timer;
    [SerializeField]
    GameObject[] enemyList;
    [SerializeField]
    GameObject[] spawnPoints;
    [SerializeField]
    float firstLevelTime;
    [SerializeField]
    float levelWeight;
    [SerializeField]
    float enemyCoolTime;


    float time;
    [SerializeField]
    int level;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        level = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Setting Timer UI
        timer.text = "Time: " + string.Format("{0:F3}", time) + "s";

        if(playerController.isMove) 
        {
            time += Time.deltaTime;
            enemyCoolTime -= Time.deltaTime;
            if(time > 10 * level)
            {
                level++;
            }
        }

        if(time > 10)
        {
            Debug.Log("Win!");
        }

        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        if(enemyCoolTime < 0)
        {
            int enemyIndex = Random.Range(0, enemyList.Length);
            int spawnIndex = Random.Range(0, spawnPoints.Length);

            Instantiate(enemyList[enemyIndex], spawnPoints[spawnIndex].transform.position, Quaternion.identity);
            enemyCoolTime = firstLevelTime - (level * levelWeight);
            if(enemyCoolTime < 0.5f)
            {
                enemyCoolTime = 0.5f;
            }
        }
    }

    
}
