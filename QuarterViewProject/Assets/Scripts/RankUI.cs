using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RankUI : MonoBehaviour
{
    [SerializeField]
    GameObject addUI;
    [SerializeField]
    GameObject searchUI;
    [SerializeField]
    GameObject addComplete;
    [SerializeField]
    GameObject searchComplete;
    [SerializeField]
    DatabaseManager databaseManager;
    [SerializeField]
    TextMeshProUGUI[] nameList;
    [SerializeField]
    TextMeshProUGUI[] timeList;
    [SerializeField]
    TextMeshProUGUI[] enemyList;
    [SerializeField]
    TextMeshProUGUI[] scoreList;
    [SerializeField]
    TextMeshProUGUI[] myList;

    public GameObject loadingText;
    [SerializeField]
    TMP_InputField searchInput;

    public bool isLoading;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenAddUI()
    {
        if(!isLoading)
        {
            addUI.SetActive(true);
        }
    }

    public void CloseAddUI()
    {
        addComplete.SetActive(false);
        addUI.SetActive(false);
        SetLeaderBoard();
    }

    public void OpenSearchUI()
    {
        if(!isLoading)
        {
            searchUI.SetActive(true);
        }
    }

    public void CloseSearchUI()
    {
        searchComplete.SetActive(false);
        searchUI.SetActive(false);
    }

    public void SetLeaderBoard()
    {
        loadingText.SetActive(true);
        isLoading= true;
        StartCoroutine(databaseManager.LoadData());
        StartCoroutine(CreatingLeaderBoard());
    }

    IEnumerator CreatingLeaderBoard()
    {
        yield return new WaitForSeconds(3f);
        loadingText.SetActive(false);
        isLoading= false;
        if (databaseManager.nameList.Count < 5)
        {
            int count = databaseManager.nameList.Count;
            for (int i = 0; i < count; i++)
            {
                nameList[i].text = databaseManager.nameList[i];
                timeList[i].text = string.Format("{0:F2}", databaseManager.timeList[i]);
                enemyList[i].text = databaseManager.enemyKillList[i].ToString();
                scoreList[i].text = databaseManager.scoreList[i].ToString();
            }

            for (int i = count; i < 5; i++)
            {
                nameList[i].text = "";
                timeList[i].text = "";
                enemyList[i].text = "";
                scoreList[i].text = "";
            }

        }

        else
        {
            for (int i = 0; i < 5; i++)
            {
                nameList[i].text = databaseManager.nameList[i];
                timeList[i].text = string.Format("{0:F2}", databaseManager.timeList[i]);
                enemyList[i].text = databaseManager.enemyKillList[i].ToString();
                scoreList[i].text = databaseManager.scoreList[i].ToString();
            }
        }
    }

    public void SearchScore()
    {
        string name = searchInput.text;
        if(databaseManager.nameList.Exists(item => item.Equals(name)))
        {
            int index = databaseManager.nameList.IndexOf(name);
            float percent = (float) (index + 1) / (float)databaseManager.nameList.Count * 100;
            myList[0].text = string.Format("{0:F2}", percent) + "%";
            myList[1].text = databaseManager.nameList[index];
            myList[2].text = string.Format("{0:F2}", databaseManager.timeList[index]);
            myList[3].text = databaseManager.enemyKillList[index].ToString();
            myList[4].text = databaseManager.scoreList[index].ToString();
            searchComplete.SetActive(true);
        }

        else
        {
            searchInput.text = "No Results.";
            searchComplete.SetActive(false);
        }
    }

}
