 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParticleTest : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] float resetTime;
    [SerializeField] bool randomRota;
    Vector3 resetPos;
    Quaternion resetRota;

    void Start()
    {
        StartCoroutine(PosReset());
        resetPos = transform.position;
        resetRota = transform.rotation;
    }

    void FixedUpdate()
    {
        transform.position += transform.forward * moveSpeed;
        transform.Rotate(0, rotateSpeed, 0);
        transform.Rotate(0, rotateSpeed, 0);
    }

    IEnumerator PosReset()
    {
        while (true)
        {
            yield return new WaitForSeconds(resetTime);
            transform.position = resetPos;
            transform.rotation = resetRota;

            if (randomRota)
            {
                transform.Rotate(0, Random.Range(0, 360), 0);
            }
        }
    }
}
