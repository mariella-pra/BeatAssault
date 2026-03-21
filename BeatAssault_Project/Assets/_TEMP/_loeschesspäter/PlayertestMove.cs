using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayertestMove : MonoBehaviour
{
    public float speed = 5f; 
    public float rotationSpeed = 720f; 

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
       
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;

       
        if (movement.magnitude > 0f)
        {
            
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

           
            rb.MovePosition(transform.position + movement * speed * Time.deltaTime);
        }
    }
}