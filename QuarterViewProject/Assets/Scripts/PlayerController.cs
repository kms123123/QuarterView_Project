using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    public float knockbackForce;
    public int HP;



    public int timeStop;
    [SerializeField]
    float timeStopBlinkDuration;
    [SerializeField]
    float timeStopBlinkIntensity;
    [SerializeField]
    float godModeTime;
    [SerializeField]
    float speedUpSpeed;
    [SerializeField]
    float speedUpTime;
    [SerializeField]
    float laserTime;



    [SerializeField]
    HealthBar healthBar;
    [SerializeField]
    SkillBar skillBar;


    [SerializeField]
    GameObject AOEAura;
    public float AOERemainTime;


    [SerializeField]
    AudioClip timeStopAudio;
    [SerializeField]
    AudioClip hitAudio;
    [SerializeField]
    AudioClip laserGetAudio;
    [SerializeField]
    AudioClip godModeAudio;
    [SerializeField]
    AudioClip speedUpAudio;
    [SerializeField]
    AudioClip timeStopGetAudio;
    [SerializeField]
    AudioClip aoeAudio;


    Rigidbody playerRb;
    Animator playerAnim;
    Material playerMat;
    AudioSource playerAudioSource;
    LaserMode laserMode;
    
    Vector3 moveDirection;
    float horizontalInput, verticalInput;
    float timeStopBlinkTimer;
    float originalSpeed;

    [HideInInspector]
    public float GodModeInTime;
    [HideInInspector]
    public float TimeStopInTime;
    [HideInInspector]
    public float SpeedUpInTime;
    [HideInInspector]
    public float LaserInTime;

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
    [HideInInspector]
    public bool isSpeedUp;
    [HideInInspector]
    public bool isLaser;
    [HideInInspector]
    public bool isDead;


    // Start is called before the first frame update
    void Start()
    {
        isHit= false;
        isMove = false;
        isGodMode = false;
        isTimeStop = false;
        isSpeedUp= false;
        isLaser = false;
        isDead= false;
        originalSpeed = moveSpeed;
        GodModeInTime = godModeTime;
        TimeStopInTime = timeStopBlinkDuration;
        LaserInTime = laserTime;
        playerRb= GetComponent<Rigidbody>();
        playerAnim= GetComponent<Animator>();
        playerMat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        playerAudioSource= GetComponent<AudioSource>();
        laserMode = GetComponentInChildren<LaserMode>();
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
            isDead = true;
            playerAnim.SetTrigger("isDead");
            gameObject.layer = 11;
        }

        //Time Stop Skill
        if(Input.GetKeyDown(KeyCode.Space) && timeStop > 0 && !isTimeStop && !isDead && !isHit)
        {
            TimeStopInTime = timeStopBlinkDuration;
            playerAudioSource.PlayOneShot(timeStopAudio, 1.0f);
            skillBar.UseTimeStop();
            timeStop--;
            isTimeStop = true;
            if(isGodMode)
            {
                gameObject.layer = 15;
            }
            else
            {
                gameObject.layer = 14;
            }
            
        }
        
        if(isMove)
        {
            ReduceSkillTime(); 
        }

    }

    /// <summary>
    /// Reduce the duration of skills in-time. When the duration is up, restore the player's state.
    /// </summary>
    private void ReduceSkillTime()
    {
        if(isGodMode)
        {
            GodModeInTime -= Time.deltaTime;
            if (GodModeInTime <= 0)
            {
                isGodMode = false;
                if(isTimeStop)
                {
                    gameObject.layer = 14;
                }
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

        if(isSpeedUp)
        {
            SpeedUpInTime -= Time.deltaTime;
            if(SpeedUpInTime <= 0)
            {
                isSpeedUp = false;
                moveSpeed = originalSpeed;
            }
        }

        if(isLaser)
        {
            LaserInTime -= Time.deltaTime;
            if(LaserInTime <= 0)
            {
                isLaser = false;
                laserMode.ShootLaser();
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


    private void StopMove()
    {
        isWall = Physics.Raycast(transform.position, moveDirection, 1, LayerMask.GetMask("Wall"));
    }


    private void FixedUpdate()
    {
        if(!isDead)
        {
            MovePlayer();
            StopMove();
        }
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
                playerAudioSource.PlayOneShot(hitAudio, 1f);
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
            playerAudioSource.PlayOneShot(godModeAudio, 1f);
            Destroy(other.gameObject);
            skillBar.UseGodMode();
            isGodMode = true;
            if(isTimeStop)
            {
                gameObject.layer = 15;
            }
        }

        if(other.gameObject.CompareTag("TimeStop"))
        {
            playerAudioSource.PlayOneShot(timeStopGetAudio, 1f);
            Destroy(other.gameObject);
            timeStop++;
        }

        if(other.gameObject.CompareTag("AOE"))
        {
            Instantiate(AOEAura, other.gameObject.transform.position - new Vector3(0, 0.8f, 0), Quaternion.identity);
            playerAudioSource.PlayOneShot(aoeAudio, 1f);
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("SpeedUp"))
        {
            SpeedUpInTime = speedUpTime;
            playerAudioSource.PlayOneShot(speedUpAudio, 1f);
            Destroy(other.gameObject);
            skillBar.UseSpeedUp();
            isSpeedUp = true;
            moveSpeed = speedUpSpeed;
        }

        if(other.gameObject.CompareTag("Laser"))
        {
            LaserInTime = laserTime;
            playerAudioSource.PlayOneShot(laserGetAudio, 1f);
            skillBar.UseLaser();
            isLaser= true;
            Destroy(other.gameObject);
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
