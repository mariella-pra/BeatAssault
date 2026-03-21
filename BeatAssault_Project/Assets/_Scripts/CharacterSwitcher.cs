
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSwitcher : MonoBehaviour
{
    private PlayerInputHandler _inputHandler;
    private int index = 0;
    [SerializeField] private List<GameObject> _players = new List<GameObject>();
    void Start()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
        index = Random.Range(0, _players.Count);
        // _inputHandler._player = _players[index];
    }   

    public void SwitchPlayerPrefab(PlayerInput input)
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
        index = Random.Range(0, _players.Count);
        // _inputHandler._player = _players[index];
    }
}
