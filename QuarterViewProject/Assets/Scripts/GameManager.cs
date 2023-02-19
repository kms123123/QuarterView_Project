using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.UIElements;
using Unity.VisualScripting;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;
    [SerializeField]
    TextMeshProUGUI timer;
    [SerializeField]
    GameObject[] enemyList;
    [SerializeField]
    GameObject[] enemySpawnPoints;
    [SerializeField]
    GameObject[] powerUpList;


    [SerializeField]
    float firstLevelTime;
    [SerializeField]
    float levelWeight;
    [SerializeField]
    float enemyCoolTime;
    [SerializeField]
    float powerUpCoolTime;

    float powerUpPosX, powerUpPosZ;
    Image[] heartUIList;
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
        timer.text = "Time: " + string.Format("{0:F2}", time) + "s";

        if(playerController.isMove) 
        {
            time += Time.deltaTime;

            if(!playerController.isTimeStop)
            {
                enemyCoolTime -= Time.deltaTime;
            }
            
            powerUpCoolTime -= Time.deltaTime;
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
        SpawnPowerUp();
    }

    void SpawnEnemy()
    {
        if(enemyCoolTime < 0)
        {
            int enemyIndex = UnityEngine.Random.Range(0, enemyList.Length);
            int spawnIndex = UnityEngine.Random.Range(0, enemySpawnPoints.Length);

            Instantiate(enemyList[enemyIndex], enemySpawnPoints[spawnIndex].transform.position, Quaternion.identity);
            enemyCoolTime = firstLevelTime - (level * levelWeight);
            if(enemyCoolTime < 0.5f)
            {
                enemyCoolTime = 0.5f;
            }
        }
    }

    void SpawnPowerUp()
    {
        if (powerUpCoolTime < 0)
        {
            int powerUpIndex = UnityEngine.Random.Range(0, powerUpList.Length);
            powerUpPosX = UnityEngine.Random.Range(-9f, 9f);
            float temp = Math.Abs(powerUpPosX);
            temp = 9f - temp;
            powerUpPosZ = UnityEngine.Random.Range(-temp, temp);

            Instantiate(powerUpList[powerUpIndex], new Vector3(powerUpPosX, 1.0f, powerUpPosZ), Quaternion.Euler(270f, 0f, 0f));
            powerUpCoolTime = 10f;
        }
    }


}
