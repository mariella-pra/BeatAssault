using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class testitest2 : MonoBehaviour
{
  
    Vector3 cameraDir;
    Transform newRotation;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraDir = Camera.main.transform.forward;
        transform.rotation = Quaternion.LookRotation(cameraDir);

        //newRotation.rotation = Quaternion.LookRotation(cameraDir, transform.forward);

        //transform.rotation = Quaternion.Euler(newRotation.rotation.x, 0, 0);


    }
}
