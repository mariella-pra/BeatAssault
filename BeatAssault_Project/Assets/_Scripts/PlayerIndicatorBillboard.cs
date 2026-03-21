using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIndicatorBillboard : MonoBehaviour
{
    [SerializeField] Transform cam;
    private void Awake()
    {
        cam = Camera.main.transform;
        // cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
    void LateUpdate()
    {
        // transform.LookAt(transform.position + cam.forward);
    }
}
