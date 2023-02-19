using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    PlayerController playerController;
    [SerializeField]
    Vector3 offset;
    [SerializeField]
    GameObject[] aura;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = playerTransform.position + offset;
    }

    private void Update()
    {
        if(playerController.isGodMode)
        {
            aura[0].SetActive(true);
        }

        else
        {
            aura[0].SetActive(false);
        }

        if(playerController.isSpeedUp)
        {
            aura[1].SetActive(true);
        }

        else
        {
            aura[1].SetActive(false);
        }
    }
}
