using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;
    [SerializeField]
    TextMeshProUGUI timer;

    float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Setting Timer UI
        timer.text = "Time: " + string.Format("{0:F3}", time) + "s";

        if(playerController.isMove) 
        {
            time += Time.deltaTime;
        }

        if(time > 10)
        {
            Debug.Log("Win!");
        }
    }
}
