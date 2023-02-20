using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRemainTime : MonoBehaviour
{
    PlayerController playerController;

    [SerializeField]
    float remainTime;
    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (playerController.isMove)
        {
            remainTime -= Time.deltaTime;
        }

        if(remainTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
