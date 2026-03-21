using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput _playerInput;
    public PlayerController _playerController;
    private PlayInstrument _playInstrument;
    public GameObject[] _player;
    private Vector3 startPos = new Vector3(0,0,0);
    
    Gamepad gamepad;
    
   // InputActionMap _actionMapMovement;
   // InputActionMap _actionMapUI;
   InputActionAsset inputActions;
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        _playerInput = GetComponent<PlayerInput>();
        // _actionMapMovement = _playerInput.actions.actionMaps[0];
        // _actionMapUI = _playerInput.actions.actionMaps[1];
        inputActions = _playerInput.actions;
        
        // print(_playerInput.devices[0].name);
        
        // inputActions = Resources.Load<InputActionAsset>($"Assets/unity/Inputs/PlayerInput"); 
        // inputActions.Enable();
        
        // index = _playerInput.playerIndex;
        // var movements = FindObjectsOfType<PlayerMovement>();
        // _movement = movements.FirstOrDefault(m => m.GetPlayerIndex() == index);
        
        // _player = transform.parent.gameObject;
        
        // Scene scene = SceneManager.GetActiveScene();
        // if (scene.name == "CurrentGame")
        // {
        //     GameManager.instance.spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint"); 
        //     gameObject.transform.position =
        //         GameManager.instance
        //             .spawnPoints[
        //                 _player[GetComponent<PlayerInput>().playerIndex].GetComponent<PlayerController>().playerIndex]
        //             .transform.position;
        // }
        
        
        if (_player != null && _player.Length > 0)
        {
            // GameObject temp = list.Where(obj => obj.name == "Sword").SingleOrDefault();
            // GameObject spawn = GameManager.instance.spawnPoints.Find(obj => obj.name ==  "LobbySpawn" + _player[GetComponent<PlayerInput>().playerIndex]);
            GameObject spawn = GameManager.instance.spawnPoints[GetComponent<PlayerInput>().playerIndex];
            _playerController = Instantiate(_player[GetComponent<PlayerInput>().playerIndex], spawn.transform.position, spawn.transform.rotation).GetComponent<PlayerController>();
            
            
            // _playerController = Instantiate(_player[GetComponent<PlayerInput>().playerIndex],
            //     GameManager.instance.spawnPoints[_player[GetComponent<PlayerInput>().playerIndex].GetComponent<PlayerController>().playerIndex].transform.position,
            //     GameManager.instance.spawnPoints[_player[GetComponent<PlayerInput>().playerIndex].GetComponent<PlayerController>().playerIndex].transform.rotation
            // ).GetComponent<PlayerController>();
            
            // _playerController = Instantiate(_player[GetComponent<PlayerInput>().playerIndex],
            //     GameManager.instance.spawnPoints[0].transform.position,
            //     transform.rotation).GetComponent<PlayerController>();
            transform.parent = _playerController.transform;
            transform.position = _playerController.transform.position;
        }
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe to avoid memory leaks
    }
    private void Start()
    {
        // InputSystem.ResetHaptics();
        // InputSystem.EnableDevice(InputSystem.devices[0]);

       
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Debug.Log($"Scene loaded: {scene.name}");
        inputActions.Enable();
        // print(_playerInput.devices[0].name);
        
        InputSystem.ResetHaptics();
        foreach (var device in InputSystem.devices)
        {
            InputSystem.RemoveDevice(device);
            InputSystem.AddDevice(device);
        }
        // Add any reinitialization logic here
    }
    private void OnEnable()
    {
        
        // _actionMapMovement.Enable();
        // _actionMapUI.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
        // _actionMapMovement.Disable();
        // _actionMapUI.Disable();
    }
    public void PositionPlayer()
    {
        gameObject.transform.position =
            GameManager.instance
                .spawnPoints[
                    _player[GetComponent<PlayerInput>().playerIndex].GetComponent<PlayerController>().playerIndex]
                .transform.position;
    }
    public void OnEscape(CallbackContext context)
    {
        //UIManager.instance.OpenMenu();
        if (context.performed)
        {
            if (UIManager.instance.isOpen)
            {
                // AudioManager.instance._gameContinue.Post(AudioManager.instance.gameObject);

                UIManager.instance.Resume();
            }

            else if (UIManager.instance.isOpen == false)
            {
                AudioManager.instance._gamePause.Post(AudioManager.instance.gameObject);

                UIManager.instance.Pause();
            }
        }
    }
    public void OnVibrate(CallbackContext context)
    {
        // _playerController.OnVibrate(context);
        
        // gamepad = Gamepad.current;
        // if (gamepad != null)
        // {
        //     gamepad.SetMotorSpeeds(.25f, 1f);
        //     Invoke("StopVibrate", 0.25f);
        // }
    }
    // public void StopVibrate()
    // {
    //     gamepad.SetMotorSpeeds(0, 0);
    // }
    public void OnMove(CallbackContext context)
    {
        _playerController.OnMove(context);
    }
    public void OnDash(CallbackContext context)
    {
        _playerController.OnDash(context);
    }

    public void OnPlayInstrument(CallbackContext context)
    {
        if(context.started) transform.parent.gameObject.GetComponent<PlayInstrument>().OnPlayInstrument(context);
    }
    public void OnUIRotate(CallbackContext context)
    {
        _playerController.OnUIRotate(context);
    }
}
