using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AOEAura : MonoBehaviour
{
    PlayerController playerController;
    ParticleSystem particle;
    Image skillImage;
    float remainTime;
    float AuraTime;
    float knockbackForce;

    private void Awake()
    {
        skillImage = GetComponentInChildren<Image>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        particle = GetComponent<ParticleSystem>();
        remainTime = playerController.AOERemainTime;
        AuraTime= remainTime;
        knockbackForce = playerController.knockbackForce;
    }

    private void Update()
    {
        skillImage.fillAmount = remainTime / AuraTime;

        if(!playerController.isMove)
        {
            particle.Pause();
        }

        else
        {
            particle.Play();
            remainTime -= Time.deltaTime;
        }

        if(remainTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Vector3 forceDirection = (other.gameObject.transform.position - transform.position).normalized;
            forceDirection.y = 0;
            forceDirection += Vector3.up * 0.3f;

            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();

            enemyRb.freezeRotation = false;
            other.gameObject.GetComponent<EnemyController>().SetDeath();
            enemyRb.AddForce(knockbackForce * forceDirection, ForceMode.Impulse);
            Destroy(other.gameObject, 4);
        }
    }
}
