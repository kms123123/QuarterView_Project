using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;
    [SerializeField]
    GameObject heartImage;
    int playerHP;
    GameObject[] heartinGame;
    
    // Start is called before the first frame update
    void Start()
    {
        playerHP = playerController.HP;
        heartinGame = new GameObject[playerHP];
        for(int i = 0; i < playerHP; i++)
        {
            GameObject heart = Instantiate(heartImage);
            heart.transform.SetParent(transform);
            heartinGame[i] = heart;
        }
    }

    public void DamageinUI()
    {
        int currentHP = playerController.HP;
        heartinGame[currentHP].SetActive(false);
    }
}
