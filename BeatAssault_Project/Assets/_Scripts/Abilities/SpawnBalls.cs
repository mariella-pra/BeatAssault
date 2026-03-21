using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SpawnBalls", menuName = "Abilities/SpawnBalls")]
public class SpawnBalls : Abilities
{
    [SerializeField] private GameObject ballGroup;
    //public bool spawned = false;

    public override void Use(GameObject _player, PlayerController _playerController, int _nextBall)
    {
        GameObject newBall;
        // if (spawned == false)
        // {
            newBall = Instantiate(ballGroup, _player.gameObject.transform.GetChild(0).transform.position, _player.gameObject.transform.GetChild(0).transform.rotation);
            PlayerVariables _variables = _player.GetComponent<PlayerVariables>();
            for (int i = 0; i <  newBall.transform.childCount; i++)
            {
                newBall.transform.GetChild(i).GetComponent<BallBehaviour>().AssignPlayerShooting(_playerController);
                // newBall.transform.GetChild(i).GetComponent<BallBehaviour>().AssignPlayerShooting(_playerController);
            }

            newBall.GetComponent<BallGroup>().AssignPlayerShooting(_playerController);
            // newBall.GetComponent<BallGroup>().AddToList();
            _variables.selectedAbilityIndex += 1;
    }
}