using System;
using UnityEngine;
using DG.Tweening;

public class ProgressiveWallScript : MonoBehaviour
{
    [SerializeField] private Vector3 originScale;
    [SerializeField] private float maxScale;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private bool isShrinking;
    [SerializeField] private bool scale = true;

    [SerializeField] private float waitFor = 1f;
    private float waitTime;

    public PlayerController _playerShooting;
    public float damage = 0.5f;

    public GameObject particleSystem;

    public void AssignPlayerShooting(PlayerController _player)
    {
        _playerShooting = _player;
    }

    private void Start()
    {
        originScale = transform.localScale;
        transform.rotation = Quaternion.Euler(0, _playerShooting.transform.eulerAngles.y, 0);
        
    }

    private void FixedUpdate()
    {
        if (scale)
        {
            transform.localPosition += transform.forward * (0.1f * (speed * Time.fixedDeltaTime * 10f));

            if (transform.localScale.z <= maxScale && !isShrinking)
            {
                transform.localScale += new Vector3(0, 0, 0.2f) * (speed * Time.fixedDeltaTime * 10f);
            }
            else
            {
                isShrinking = true;
                transform.localScale += new Vector3(0, 0, -0.2f) * (speed * Time.fixedDeltaTime * 10f);
                if (transform.localScale.z <= originScale.z) scale = false;
            }
        }
        else
        {
            waitTime += Time.deltaTime;
            if (waitTime >= waitFor)
            {
                Destroy(gameObject);
                Destroy(particleSystem.gameObject);
            }
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") &&
            other.gameObject.GetComponent<PlayerController>() != _playerShooting)
        {
            other.gameObject.GetComponent<PlayerController>()
                .GetComponent<PlayerHealthControl>().DamagePlayer(damage);
        }
    }
}


// using System;
// using System.Collections;
// using UnityEngine;
// using DG.Tweening;
// using UnityEngine.InputSystem.LowLevel;
//
// public class ProgressiveWallScript : MonoBehaviour
// {
//     // [SerializeField] float maxLength = 10f; // The maximum length the wall can grow to along the x-axis
//     // [SerializeField] float growSpeed = 2f; // Duration in seconds to grow
//     // [SerializeField] float shrinkSpeed = 2f; // Duration in seconds to shrink
//     // [SerializeField] float travelDistance = 20f; // The distance to "travel" through growth and shrinking
//     // [SerializeField] Ease growEase = Ease.OutSine; // Ease function for growing
//     // [SerializeField] Ease shrinkEase = Ease.InSine; // Ease function for shrinking
//     //
//     // private Vector3 initialScale; // The original scale of the wall
//     // private Vector3 initialPosition; // The original position of the wall
//     // private float distanceCovered = 0f; // Track how much distance the wall has "traveled"
//     //
//     // private float distance;
//
//     [SerializeField] Vector3 originScale;
//     [SerializeField] float maxScale;
//     [SerializeField] private float speed = .5f;
//     [SerializeField] bool isShrinking;
//     [SerializeField] bool scale = true;
//     
//
//     [SerializeField] float waitFor = 1f;
//     private float waitTime;
//
//     public PlayerController _playerShooting;
//
//     public float damage = .5f;
//     public void AssignPlayerShooting(PlayerController _player)
//     {
//         _playerShooting = _player;
//     }
//     private void Start()
//     {
//         originScale = transform.localScale;
//         transform.Rotate(new Vector3(0, _playerShooting.gameObject.transform.rotation.y, 0));
//
//     }
//     void FixedUpdate()
//     {
//         if (scale)
//         {
//             transform.localPosition += transform.forward * (0.1f * (speed * Time.fixedDeltaTime * 10f));
//             
//             if (transform.localScale.z <= maxScale && !isShrinking)
//             {
//                 transform.localScale += new Vector3(0, 0, 0.2f) * (speed * Time.fixedDeltaTime * 10f);
//             }
//             else
//             {
//                 isShrinking = true;
//                 transform.localScale += new Vector3(0, 0, -0.2f) * (speed * Time.fixedDeltaTime * 10f);
//                 if (transform.localScale.z <= originScale.z)scale = false;
//             }
//         }
//         else
//         {
//             waitTime += Time.deltaTime;
//             if (waitTime >= waitFor) Destroy(gameObject);
//         }
//     }
//
//     private void OnTriggerStay(Collider other)
//     {
//         if (other.gameObject.CompareTag("Player") &&
//             other.gameObject.GetComponent<PlayerController>() != _playerShooting)
//         {
//             other.gameObject.GetComponent<PlayerController>().GetComponent<PlayerHealthControl>().DamagePlayer(damage);
//             // Debug.Log("wall stay");
//         }
//         
//             // Debug.Log("player gets hit");
//     } 
// }
