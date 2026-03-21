using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    public int playerIndex = 0;
    private PlayerVariables _variables;
    private Gamepad gamepad;

    [Header("Movement")]
    public float _moveSpeed;
    public float _verticalVelocity = -15f;
    public bool isGrounded;
    public LayerMask _groundLM;
    public float _radius;
    public float _groundedOffset = 0.4f;
    
    private Vector2 dir;
    public Vector3 move;
    public CharacterController _controller;
    private Rigidbody _rb;

    public bool canMove = true;
    
    [Header("Dash")]
    public float _dashTime = 0.2f;
    public float _dashSpeed = 20f;
    public float _dashCooldown = 1f;
    private bool isDashing = false;
    private bool canDash = true;
    
    // public Camera playerCamera;
    
    public float rotationSpeed = 100f;
    private Vector2 rotationInput; 
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

        _controller = GetComponent<CharacterController>();
        _variables = GetComponent<PlayerVariables>();
        gamepad = Gamepad.current;
        
        canMove = true;
        _variables.playerEvents?.AddEvent("PatternCue", Vibrate);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {


        if (scene.name == "Level1")
        {
            // playerCamera = Camera.main;
            transform.position = GameManager.instance.spawnPoints.Find(obj => obj.name == playerIndex.ToString()).transform.position;
        }
        
        // print(playerList.Count);
        // playerList[i].enabled = false;
        // playerList[i].enabled = true;
        // playerList[i].actions.FindActionMap("UI").Disable();
        // playerList[i].actions.FindActionMap("Movement").Enable();
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; 
        StopVibrate();
    }
    private void OnDisable()
    {
        _variables.playerEvents?.RemoveEvent("PatternCue", Vibrate);
    }

    void FixedUpdate()
    {
        MovePlayer();
        GroundedCheck();
    }

    public void Vibrate()
    {
        // print(GameManager.instance.playerList[playerIndex].devices[0]);
        // if (GameManager.instance.playerList.Count > 0)
        // {
        //     gamepad = GameManager.instance.playerList[playerIndex].devices[0] as Gamepad;
        // }
        
        if (GameManager.instance.playerListControllers.Count > 0)
        {
            gamepad = GameManager.instance.playerListControllers[playerIndex].devices[0] as Gamepad;
        }
        // gamepad = Gamepad.current;
        if (gamepad != null)
        {
            gamepad.SetMotorSpeeds(.25f, 2f);
            Invoke("StopVibrate", 0.15f);
        }
    }
    public void OnVibrate(InputAction.CallbackContext context)
    {
        gamepad = GameManager.instance.playerList[playerIndex].devices[0] as Gamepad;
        // gamepad = Gamepad.current;
        if (gamepad != null)
        {
            gamepad.SetMotorSpeeds(.25f, 2f);
            Invoke("StopVibrate", 0.15f);
        }
    }
    public void StopVibrate()
    {
        if (gamepad != null) gamepad.SetMotorSpeeds(0, 0);
    }
    private void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, 0, transform.position.z);
        isGrounded = Physics.CheckSphere(spherePosition, _radius, _groundLM,
            QueryTriggerInteraction.Ignore);
    }
    void MovePlayer()
    {
        //rotate player
        if (rotationInput.x != 0)
        {
            float rotationY = rotationInput.x * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotationY, 0);
        }
        
        if(SceneManager.GetActiveScene().name != "Level1") return;
        Vector3 inputDirection = move;
        // Get the camera's forward and right vectors (ignoring vertical component)
        Vector3 cameraForward = GameAssets.instance._gameCam.transform.forward;
        Vector3 cameraRight = GameAssets.instance._gameCam.transform.right;
        // Project forward and right vectors onto the XZ plane (ignore Y axis)
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        if (canMove)
        {
            

        Vector3 worldMoveDirection = cameraForward * inputDirection.z + cameraRight * inputDirection.x;
        _controller.Move(worldMoveDirection * (_moveSpeed * Time.deltaTime) +
                         new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

        if (worldMoveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(worldMoveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        // Debug.Log($"Movement Input: {movement}");
        move = new Vector3(movement.x, 0,  movement.y);
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started && canDash) StartCoroutine(DashCoroutine());
    }
    public void OnUIRotate(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<Vector2>();
    }
    private IEnumerator DashCoroutine()
    {
        isDashing = true;
        canDash = false;

        float startTime = Time.time;

        // Use the current move direction, or default to forward if there's no movement input
        // Vector3 dashDirection = move != Vector3.zero ? move.normalized : transform.forward;
        Vector3 dashDirection = transform.forward;
        while (Time.time < startTime + _dashTime)
        {
            // _controller.Move(new Vector3(dashDirection.x, 0, dashDirection.z) * _dashSpeed * Time.deltaTime);
            _controller.Move(new Vector3(dashDirection.x, 0, dashDirection.z) * _dashSpeed * Time.deltaTime);
            yield return null;
        }
        isDashing = false;
        yield return new WaitForSeconds(_dashCooldown);
        canDash = true;
    }
}