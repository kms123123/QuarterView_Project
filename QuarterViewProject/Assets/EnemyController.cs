using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    GameObject player;

    Rigidbody enemyRb;
    Vector3 playerDirection;
    PlayerController playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyRb= GetComponent<Rigidbody>();
        playerController = player.GetComponent<PlayerController>();
    }


    /// <summary>
    /// Moves Enemy
    /// </summary>
    // Update is called once per frame
    void FixedUpdate()
    {
        playerDirection = (player.transform.position - transform.position).normalized;
        playerDirection.y = 0;
        if(playerController.isMove)
        {
            EnemyMove();
        }
    }

    private void EnemyMove()
    {
        transform.LookAt(transform.position + playerDirection);
        enemyRb.MovePosition(transform.position + (playerDirection * moveSpeed * Time.deltaTime));
    }

    private void Update()
    {

    }
}
