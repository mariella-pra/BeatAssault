using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoWall : MonoBehaviour
{
    PlayerController _playerShooting;
    public float damage = .5f;
    public float lifeTime = 1.6f;
    public void AssignPlayerShooting(PlayerController _player)
    {
        _playerShooting = _player;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") &&
            other.gameObject.GetComponent<PlayerController>() != _playerShooting)
        {
            other.gameObject.GetComponent<PlayerController>().GetComponent<PlayerHealthControl>().DamagePlayer(damage);
        }
    } 
    public void DestroyWall()
    {
        
        Destroy(gameObject);
    }

    private void Awake()
    {
        Invoke("DestroyWall", lifeTime);
    }
}