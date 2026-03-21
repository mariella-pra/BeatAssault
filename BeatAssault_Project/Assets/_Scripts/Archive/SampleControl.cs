using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class SampleControl : MonoBehaviour
{
    public AudioClip kickSound;
    public AudioSource audioSource;
    
    // Variables for beat timing
    public float beatInterval = 1f; // Time between beats in seconds
    private float nextBeatTime; // Time of the next beat
    public bool waitingForBeat = false; // Flag to indicate if waiting for player input on beat

    // Variables for timing accuracy
    public float perfectTimingThreshold = 0.1f; // Threshold for perfect timing in seconds

    private PlayerController player;
    private void Start()
    {
        // Initialize next beat time
        nextBeatTime = Time.time + beatInterval;
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        // Check if it's time for the next beat
        if (Time.time >= nextBeatTime)
        {
            waitingForBeat = true; // Set flag to indicate waiting for player input
            // Schedule next beat
            nextBeatTime += beatInterval;
        }

        // _sampleAction = _playerInput.actions.FindAction("Kick");
        // if (_sampleAction.performed)
        // {
        //     
        // }
    }

    public void OnKick(InputAction.CallbackContext context)
    {
        if (/*waitingForBeat && */context.started)
        {
            
            float timingDifference = Mathf.Abs(Time.time - nextBeatTime); // Calculate timing difference
            // float timingDifference = Time.time - nextBeatTime; // Calculate timing difference
            if (timingDifference <= perfectTimingThreshold)
            {
                Debug.Log("Perfect Timing!"); // Display feedback for perfect timing
                // StartCoroutine(player.ChangeColorCoroutine(player.perfectHitColor));
            }
            else
            {
                Debug.Log("Timing Off!"); // Display feedback for timing off
                // StartCoroutine(player.ChangeColorCoroutine(player.offHitColor));
            }
            // waitingForBeat = false; // Reset flag
            
            // if (kickSound != null)
            // {
            //     audioSource.PlayOneShot(kickSound);
            // }
        }
    }
    // // Variables for beat timing
    // [SerializeField] float beatInterval = 1f; // Time between beats in seconds -- depending on bpm
    // private float nextBeatTime; // Time of the next beat
    // private bool waitingForBeat = false; // Flag to indicate if waiting for player input on beat
    //
    // // Variables for timing accuracy
    // public float perfectTimingThreshold = 0.1f; // Threshold for perfect timing in seconds
    //
    // void Start()
    // {
    //     // Initialize next beat time
    //     nextBeatTime = Time.time + beatInterval;
    // }
    //
    // void Update()
    // {
    //     // Check if it's time for the next beat
    //     if (Time.time >= nextBeatTime)
    //     {
    //         waitingForBeat = true; // Set flag to indicate waiting for player input
    //         // Schedule next beat
    //         nextBeatTime += beatInterval;
    //     }
    //
    //     // Check for player input
    //     if (waitingForBeat && Input.GetKeyDown(KeyCode.Space)) // Assuming space key for kick
    //     {
    //         float timingDifference = Mathf.Abs(Time.time - nextBeatTime); // Calculate timing difference
    //         if (timingDifference <= perfectTimingThreshold)
    //         {
    //             Debug.Log("Perfect Timing!"); // Display feedback for perfect timing
    //         }
    //         else
    //         {
    //             Debug.Log("Timing Off!"); // Display feedback for timing off
    //         }
    //         waitingForBeat = false; // Reset flag
    //     }
    // }
}
