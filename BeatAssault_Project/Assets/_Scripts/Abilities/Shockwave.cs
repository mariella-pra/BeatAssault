using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class Shockwave : MonoBehaviour
{
    PlayerController _playerShooting;
    private PlayerVariables _variables;
    public float damage = .5f;
    public float expandSize = 3f;
    public float expandTime = .5f;
    
    public void AssignPlayerShooting(PlayerController _player)
    {
        _playerShooting = _player;
        _variables = _player.gameObject.GetComponent<PlayerVariables>();
    }
    public void Expand()
    {
        transform.DOScale(expandSize ,expandTime).SetEase(Ease.Flash);
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.position =  _playerShooting.GetComponent<PlayerVariables>().feet.transform.position;
    }
    private void Awake()
    {
        Expand();
        Invoke("Destroy", expandTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") &&
            other.gameObject.GetComponent<PlayerController>() != _playerShooting)
        {
            other.gameObject.GetComponent<PlayerController>().GetComponent<PlayerHealthControl>().DamagePlayer(damage);
        }
    } 
}