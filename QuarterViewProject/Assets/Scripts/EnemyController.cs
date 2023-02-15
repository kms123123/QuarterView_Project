using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    

    GameObject player;
    Rigidbody enemyRb;
    Vector3 playerDirection;
    PlayerController playerController;
    Material enemyMat;
    Animator enemyAnim;

    bool isAlive;

    
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        isAlive = true;
        enemyRb= GetComponent<Rigidbody>();
        playerController = player.GetComponent<PlayerController>();
        enemyMat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        enemyAnim = GetComponent<Animator>();
    }

    public void SetDeath()
    {
        isAlive = false;
        gameObject.layer = 7;
        enemyMat.color = Color.gray;
        enemyAnim.enabled= false;
    }

    /// <summary>
    /// Moves Enemy
    /// </summary>
    // Update is called once per frame
    void FixedUpdate()
    {
        if(isAlive)
        {
            playerDirection = (player.transform.position - transform.position).normalized;
            playerDirection.y = 0;
            if (playerController.isMove && !playerController.isHit)
            {
                EnemyMove();
            }
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
