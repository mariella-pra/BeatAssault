using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoWallIndicator : MonoBehaviour
{
    PlayerController _playerShooting;
    private PlayerVariables _variables;
    
    public void AssignPlayerShooting(PlayerController _player)
    {
        _playerShooting = _player;
        _variables = _player.gameObject.GetComponent<PlayerVariables>();
        
        gameObject.transform.parent = _player.transform;
    }

    public void SetGameObject()
    {
        _playerShooting.gameObject.GetComponent<PlayerVariables>()._currentPianoWall = gameObject;
    }
    public void DestroyWall()
    {
        _variables._pianoWallIndicatorActive = false;
        Destroy(gameObject);
    }

    private void Update()
    {
        if (_playerShooting.GetComponent<PlayerVariables>()._currentPianoWall == null) DestroyWall();
    }
}