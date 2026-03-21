using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerZone : MonoBehaviour
{

    public Transform newLookTarget;
    public CinemachineVirtualCamera virtualCamera;

    private void OnTriggerEnter(Collider other)
       
    {     
        Debug.Log("On TriggerEnter");
       // CinemachineVirtualCamera virtualCamera = other.GetComponentInChildren<CinemachineVirtualCamera>();

        
        if (newLookTarget != null)
        {
            Debug.Log("...");
            virtualCamera.LookAt = newLookTarget;
        }
    }

    //public CinemachineVirtualCamera virtualCamera; 
    //public Transform newLookAtTarget;
    //public GameObject dollyCart;

    //void OnTriggerEnter(Collider other)
    //{

    //    if (other.gameObject == dollyCart)
    //    {
    //        Debug.Log("worked");
    //        virtualCamera.LookAt = newLookAtTarget;
    //    }
    //}
}
