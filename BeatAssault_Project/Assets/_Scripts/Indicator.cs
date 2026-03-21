using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public BoxCollider hitCollider;
    private PlayerVariables _variables;

    // private void Awake()
    // {
    //     _variables = transform.parent.transform.parent.GetComponent<PlayerVariables>();
    // }
    //
    // private void OnTriggerEnter(Collider other)
    // {
    //     if(other == hitCollider)
    //     {
    //         _variables.playerEvents.PublishEvent("IndicatorRemove");
    //     }
    // }
}
