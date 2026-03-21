using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameAssets : MonoBehaviour
{
    public static GameAssets instance = null;
    public Camera _gameCam;
    
    [Header("Feedback")]
    public Transform damagePopup;
    public GameObject damageParticles;

    [Header("Instruments")]
    public List<GameObject> instruments;

    public float playerDeathLength = 10f;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance is null) instance = this;
        else if (instance is not null) Destroy(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _gameCam = Camera.main;
    }
    
}
