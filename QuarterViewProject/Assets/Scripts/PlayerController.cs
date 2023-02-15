using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float knockbackForce;
    [SerializeField]
    int HP;

    Rigidbody playerRb;
    Animator playerAnim;

    Vector3 moveDirection;
    float horizontalInput, verticalInput;

    [HideInInspector]
    public bool isMove;

    // Start is called before the first frame update
    void Start()
    {
        isMove = false;
        playerRb= GetComponent<Rigidbody>();
        playerAnim= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Getting Boolean(Player is Moving)
        if(horizontalInput != 0 || verticalInput != 0)
        {
            isMove = true;
        }

        else
        {
            isMove = false;
        }

        //Player Death
        if(HP <= 0 )
        {
            Debug.Log("Game Over!");
        }
    }


    private void FixedUpdate()
    {
        MovePlayer();
    }

    /// <summary>
    /// Player Moves by Getting Horizontal, Vertical Input
    /// </summary>
    private void MovePlayer()
    {
        //Player Moves
        moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
        playerAnim.SetFloat("speed", moveDirection.magnitude);
        transform.LookAt(transform.position + moveDirection);
        playerRb.MovePosition(transform.position + (moveDirection * moveSpeed * Time.deltaTime));
    }

    /// <summary>
    /// If a player hits an enemy, it gives the enemy a huge knockback and HP decreases by one.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            HP--;
            Vector3 forceDirection = (collision.transform.position - transform.position).normalized;
            forceDirection.y = 0;
            collision.rigidbody.AddForce(knockbackForce * forceDirection, ForceMode.Impulse);
        }
    }

}
