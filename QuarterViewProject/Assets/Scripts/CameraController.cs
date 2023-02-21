using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    Vector3 cameraPos;
    [SerializeField]
    GameObject player;
    [SerializeField]
    float offSet;
    public Vector3 pos;

    PlayerController playerController;
    float deadOffset = 2f;
    bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!playerController.isDead)
        {
            transform.position = player.transform.position + cameraPos * offSet;
        }
         

        else
        {
            if(!isDead)
            {
                transform.position = player.transform.position + cameraPos * deadOffset;
                isDead = true;
            }
            transform.RotateAround(player.transform.position, new Vector3(0, 1, 0), 60f * Time.deltaTime);

        }
    }



}
