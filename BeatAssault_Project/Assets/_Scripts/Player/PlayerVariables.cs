using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerVariables : MonoBehaviour
{
    public GameObject ray;
    [Header("Values")]
    public bool _lowHealth;
    public float maxHealth = 100f;
    public float playerHealth = 100f;
    public float outOfBoundsDamage = 1f;
    public float lerpVal = 5f;
    public float healthBeforeDamage = 0f;
    public float lowHealthIndicator = 20f;
    public bool dead = false;

    [HideInInspector]
    // public List<PlayerUI> playerUIs = new List<PlayerUI>();
    public PlayerUI playerUI;
    public GameObject curPatternUI;
    [Space]
    public GameObject _snarePatternBalls;
    public GameObject _pianoPatternBalls;
    public GameObject _violinPatternBalls;
    public GameObject _saxPatternBalls;
    public GameObject _kickPatternBalls;
    [Space]
    public GameObject indicator;
    // [Header("Feedback")]
    // public Transform damagePopup;
    
    
    [Header("Instrument")]
    public int selectedAbilityIndex;
    public bool _hitIntervalActive;
    public bool _hasInstrument;
    public bool _playsInstrument;
    public bool misses;
    public int allowedHits = 0;
    public int round;
    public bool _hasBigHitPattern;
    
    [Header("Patterns")]
    public AK.Wwise.Event _curPattern;
    public AK.Wwise.Event _stopCurPattern;

    [Header("Abilities")]
    public GameObject _currentPianoWall;
    public bool _pianoWallIndicatorActive;
    public bool _playsPiano;
    
    [HideInInspector]
    public PlayerFeedback _playerFeedback;
    
    [HideInInspector]
    public PlayerEvents playerEvents;
    [HideInInspector]
    public PlayerHealthControl healthControl;
    private PlayInstrument playInstrument;
    public PlayerController controller;
    public PlayerAnimationController animationController;
    
    public AK.Wwise.Event _currentDeathSound;

    public GameObject feet;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
        _playerFeedback = GetComponent<PlayerFeedback>();
        playerEvents = GetComponent<PlayerEvents>();
        healthControl = GetComponent<PlayerHealthControl>();
        playInstrument = GetComponent<PlayInstrument>();
        controller = GetComponent<PlayerController>();
        animationController = GetComponent<PlayerAnimationController>();
        feet = gameObject.transform.GetChild(0).gameObject;
        
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }

    public void AssignUI()
    {
        _playerFeedback.easeBar = playerUI.easeBar.GetComponent<Image>();
        _playerFeedback.healthBar = playerUI.healthBar.GetComponent<Image>();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level1")
        {
            // var foundplayerUIs = FindObjectsOfType<PlayerUI>();
            // foreach (var playerUI in foundplayerUIs) playerUIs.Add(playerUI);

            for (int i = 0; i < UIManager.instance.playerUIs.Count; i++)
            {
                if (UIManager.instance.playerUIs[i].GetComponent<PlayerUI>().tempIndex == controller.playerIndex && !UIManager.instance.playerUIs[i].gameObject.CompareTag("joinMessage"))
                {
                    playerUI = UIManager.instance.playerUIs[i].GetComponent<PlayerUI>();
                    AssignUI();
                    playerUI.AssignPlayer(controller.playerIndex);
                }
            }
            
        // switch (gameObject.GetComponent<PlayerController>().playerIndex)
        // {
        //     case 0:
        //         playerUI = GameObject.FindGameObjectWithTag("UI1").GetComponent<PlayerUI>();
        //         break;
        //     case 1:
        //         playerUI = GameObject.FindGameObjectWithTag("UI2").GetComponent<PlayerUI>();
        //         break;
        //     case 2:
        //         playerUI = GameObject.FindGameObjectWithTag("UI3").GetComponent<PlayerUI>();
        //         break;
        //     case 3:
        //         playerUI = GameObject.FindGameObjectWithTag("UI4").GetComponent<PlayerUI>();
        //         break;
        // }
        
        }
    }

    private void OnDestroy()
    {
        _hitIntervalActive = false;
        _hasInstrument = false;
        _playsInstrument = false;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    // public void AbilityIndex(bool smallImpact)
    // {
    //     if (smallImpact)
    //     {
    //         
    //     }   
    // }
}