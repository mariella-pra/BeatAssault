using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] GameObject pointA;
    [SerializeField] GameObject pointB;
    [SerializeField] float speed = 10f;
    [SerializeField] float delay = 1f;
    [SerializeField] GameObject platform;

    private Vector3 targetPosition;


    private void Start()
    {
        platform.transform.position = pointA.transform.position;
        targetPosition = pointB.transform.position;
        StartCoroutine(MovePlatform());
    }

    IEnumerator MovePlatform()
    {
        while (true)
        {
            while((targetPosition - platform.transform.position).sqrMagnitude > 0.01f)
            {
                platform.transform.position = Vector3.MoveTowards(platform.transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }

            targetPosition = targetPosition == pointA.transform.position ? pointB.transform.position : pointA.transform.position;
            yield return new WaitForSeconds(delay);

        }
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         other.transform.parent = transform;
    //     }
    // }
    //
    // private void OnCollisionExit(Collision other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         other.transform.parent = null;
    //     }
    // }
}