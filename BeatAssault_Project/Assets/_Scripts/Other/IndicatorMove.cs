using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class IndicatorMove : MonoBehaviour
{
    public float angle = -22.5f;
    public void Rotate()
    {
        transform.Rotate(Vector3.forward, angle);
    }

    public void ResetIndicator()
    {
        
    }
}
