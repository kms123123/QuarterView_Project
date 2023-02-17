using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float knockbackForce;
    
    public int HP;
    [SerializeField]
    int timeStop;
    [SerializeField]
    float timeStopBlinkDuration;
    [SerializeField]
    float timeStopBlinkIntensity;
    [SerializeField]
    float godModeTime;
    [SerializeField]
    HealthBar healthBar;
    [SerializeField]
    SkillBar skillBar;

    Rigidbody playerRb;
    Animator playerAnim;
    Material playerMat;
    
    Vector3 moveDirection;
    float horizontalInput, verticalInput;
    float timeStopBlinkTimer;

    [HideInInspector]
    public float GodModeInTime;
    [HideInInspector]
    public float TimeStopInTime;

    Ray ray;
    bool isWall;

    /// <summary>
    /// The player's condition determines whether the enemy moves or not
    /// </summary>
    [HideInInspector]
    public bool isMove;
    [HideInInspector]
    public bool isGodMode;
    [HideInInspector]
    public bool isHit;
    [HideInInspector]
    public bool isTimeStop;


    // Start is called before the first frame update
    void Start()
    {
        isHit= false;
        isMove = false;
        isGodMode = false;
        isTimeStop = false;
        GodModeInTime = godModeTime;
        TimeStopInTime = timeStopBlinkDuration;
        playerRb= GetComponent<Rigidbody>();
        playerAnim= GetComponent<Animator>();
        playerMat = GetComponentInChildren<SkinnedMeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        CheckMove();

        //Player Death
        if(HP <= 0 )
        {
            Debug.Log("Game Over!");
        }

        //Time Stop Skill
        if(Input.GetKeyDown(KeyCode.Space) && timeStop > 0 && !isTimeStop)
        {
            TimeStopInTime = timeStopBlinkDuration;
            skillBar.UseTimeStop();
            timeStop--;
            isTimeStop = true;
            gameObject.layer = 11;
        }
        
        if(isMove)
        {
            ReduceSkillTime(); 
        }


    }

    private void ReduceSkillTime()
    {
        if(isGodMode)
        {
            GodModeInTime -= Time.deltaTime;
            if (GodModeInTime <= 0)
            {
                isGodMode = false;
            }
        }
        
        if(isTimeStop)
        {
            TimeStopInTime -= Time.deltaTime;
            TimeStopBlink();
            if (TimeStopInTime <= 0)
            {
                isTimeStop = false;
                gameObject.layer = 9;
            }
        }
    }

    private void CheckMove()
    {
        //Getting Boolean(Player is Moving)
        if (horizontalInput != 0 || verticalInput != 0)
        {
            isMove = true;
        }

        else
        {
            isMove = false;
        }
    }

    private void TimeStopBlink()
    {
        float lerp = Mathf.Clamp01(TimeStopInTime / timeStopBlinkDuration);
        float intensity = lerp * timeStopBlinkIntensity + 1.0f;
        if(!isHit)
        {
            playerMat.color = Color.white * intensity;
        }
        
    }

    IEnumerator EndTimeStop()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Stop Time Stop");
        isTimeStop = false;
        gameObject.layer = 9;
    }

    private void StopMove()
    {
        Debug.DrawRay(transform.position, moveDirection, Color.green);
        isWall = Physics.Raycast(transform.position, moveDirection, 1, LayerMask.GetMask("Wall"));
    }


    private void FixedUpdate()
    {
        MovePlayer();
        StopMove();
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
        if(!isWall) 
        {
            playerRb.MovePosition(transform.position + (moveDirection * moveSpeed * Time.deltaTime));
        }
        
    }

    /// <summary>
    /// If a player hits an enemy, it gives the enemy a huge knockback and HP decreases by one.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 forceDirection = (collision.transform.position - transform.position).normalized;
            forceDirection.y = 0;

            //If GodMode power-up is obtained, it will eliminate all conflicting enemies.
            if (isGodMode)
            {
                forceDirection += Vector3.up * 0.3f;
                collision.gameObject.GetComponent<Rigidbody>().freezeRotation = false;
                collision.gameObject.GetComponent<EnemyController>().SetDeath();
                Destroy(collision.gameObject, 4);
            }

            //If it collides with an enemy, it suffers damage and is briefly invincible and fluidized.
            //At this time, the enemy does not follow.
            else
            {
                StartCoroutine(GracePeriod());
                isHit = true;
                playerMat.color = Color.yellow;
                gameObject.layer = 11;
                HP--;
                healthBar.DamageinUI();
            }

            collision.rigidbody.AddForce(knockbackForce * forceDirection, ForceMode.Impulse);
        }
    }



    /// <summary>
    /// If the player acquires a power-up, the skill is used.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("GodMode"))
        {
            GodModeInTime = godModeTime;
            Destroy(other.gameObject);
            skillBar.UseGodMode();
            isGodMode = true;
        }

        if(other.gameObject.CompareTag("TimeStop"))
        {
            Destroy(other.gameObject);
            timeStop++;
        }
    }

    /// <summary>
    /// When attacked by an enemy, it becomes invincible for a while and ignores the conflict.
    /// </summary>
    /// <returns></returns>
    IEnumerator GracePeriod()
    {
        yield return new WaitForSeconds(1f);
        isHit= false;
        playerMat.color = Color.white;
        gameObject.layer = 9;
    }



}
