using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRotaion : MonoBehaviour
{
    private Vector3 direction;
    private Vector3 previousPosition;
    //private ParticleSystem particleSystem;

    void Awake()
    {
        //particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        direction = (transform.position - previousPosition).normalized;
        previousPosition = transform.position;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        //particleSystem.startRotation3D = new Vector3(Mathf.Deg2Rad * 90, 0, Mathf.Deg2Rad * 234.7f);
    }



}
