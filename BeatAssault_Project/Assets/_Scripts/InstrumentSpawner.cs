using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentSpawner : MonoBehaviour
{
    public GameObject[] randomSpawner;
    public int randomNumInArray;
  
    //eine funktion wo ein gewähltes Instrument an einem random Ort spawnr
    public void RandomInstrument(GameObject randomI)
    {
        randomNumInArray = Random.Range(0, randomSpawner.Length);
        Instantiate(randomI, randomSpawner[randomNumInArray].transform.position, Quaternion.identity);
    }
}
