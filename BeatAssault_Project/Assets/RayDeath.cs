using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayDeath : MonoBehaviour
{
    void Update()
    {
        Invoke("DestroyMe", 5f);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
