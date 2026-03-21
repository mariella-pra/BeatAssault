using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentRespawn : MonoBehaviour
{
    public GameObject instrumentsPrefab;
    public bool canSpawn = false;
    public bool activator = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (canSpawn && !activator) RespawnInstruments();
            if(activator) GameObject.FindGameObjectWithTag("RESPAWNER").GetComponent<InstrumentRespawn>().canSpawn = true;
            else canSpawn = false;
        }
    }

    public void RespawnInstruments()
    {
        canSpawn = false;
        GameObject[] cur =GameObject.FindGameObjectsWithTag("INSTRUMENTS");
        if(cur.Length > 0)
        {
            foreach (var item in cur)
            {
                Destroy(item);
            }
        }
        Instantiate(instrumentsPrefab, Vector3.zero, Quaternion.identity);
        
    }
}
