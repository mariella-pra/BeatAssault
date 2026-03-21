using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWater : MonoBehaviour
{
    public float damage = 3f;

    public bool isWorldEnd = true;
    //private void OnTriggerEnter(Collider other)
    //{
    //    //if (collision.gameObject.CompareTag("Player"))
    //    //{
    //    //    Destroy(collision.gameObject);
    //    //}
    //    CharacterController characterController = other.gameObject.GetComponent<CharacterController>();

    //    if (characterController != null)
    //    {
    //        // Wenn das Objekt einen Character Controller hat, zerst�ren
    //        Destroy(other.gameObject);

    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isWorldEnd)
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            PlayerVariables _variables = other.gameObject.GetComponent<PlayerVariables>();
            PlayerHealthControl _healthControl = other.gameObject.GetComponent<PlayerHealthControl>();
            if (player is not null)
            {
                // StartCoroutine(_healthControl.Die());
                
                _variables.healthControl.DamagePlayer(damage);
                
                // _variables.playerEvents?.PublishEvent("playerDeath");
                // GameManager.instance._events.PublishEvent("checkPlayersLeft");

            }
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isWorldEnd)
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            PlayerVariables _variables = other.gameObject.GetComponent<PlayerVariables>();
            PlayerHealthControl _healthControl = other.gameObject.GetComponent<PlayerHealthControl>();
            if (player is not null)
            {
                StartCoroutine(_healthControl.Die());
                
                // _variables.healthControl.DamagePlayer(damage);
                
                // _variables.playerEvents?.PublishEvent("playerDeath");
                // GameManager.instance._events.PublishEvent("checkPlayersLeft");

            }
            
        }
    }

}