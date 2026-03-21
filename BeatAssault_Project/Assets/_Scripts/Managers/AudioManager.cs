using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    #region Variables
    public static AudioManager instance = null;

    [Header("Values")]
    public float hitInterval;
    public float bpm = 90;
    public float hitMoveTime;
    public float patternTime;
    
    [Header("Bank")]
    [SerializeField] private Bank _bank;
    
    [Header("Callbacks")]
    public CallbackFlags callbackType;
    
    [Header("MusicEvents")]
    public AK.Wwise.Event _lobbyMusicPlay;
    public AK.Wwise.Event _lobbyMusicStop;
    public AK.Wwise.Event _gameStart;
    public AK.Wwise.Event _gamePause;
    public AK.Wwise.Event _gameContinue;
    public AK.Wwise.Event _gameStop;
    
    [Header("SoundEvents")]
    public AK.Wwise.Event _violinSound;
    public AK.Wwise.Event _snareSound;
    public AK.Wwise.Event _saxophoneSound;
    public AK.Wwise.Event _pianoSound;
    public AK.Wwise.Event _kickSound;
    public AK.Wwise.Event _bassSound;
    
    [Header("Patterns")]
    public AK.Wwise.Event _violinPattern;
    public AK.Wwise.Event _snarePattern;
    public AK.Wwise.Event _saxophonePattern;
    public AK.Wwise.Event _pianoPattern;
    public AK.Wwise.Event _pianoPatternAudible;
    public AK.Wwise.Event _kickPattern;
    public AK.Wwise.Event _bassPattern;
    
    [Header("Stop Patterns")]
    public AK.Wwise.Event _stopViolinPattern;
    public AK.Wwise.Event _stopSnarePattern;
    public AK.Wwise.Event _stopSaxophonePattern;
    public AK.Wwise.Event _stopPianoPattern;
    public AK.Wwise.Event _stopKickPattern;
    public AK.Wwise.Event _stopBassPattern;
    
    [Header("DeathSounds")]
    public AK.Wwise.Event _deathSound;
    public AK.Wwise.Event _violinDeathSound;
    public AK.Wwise.Event _snareDeathSound;
    public AK.Wwise.Event _saxDeathSound;
    public AK.Wwise.Event _pianoDeathSound;
    public AK.Wwise.Event _kickDeathSound;
    public AK.Wwise.Event _bassDeathSound;

    [Header("SFX")]
    public AK.Wwise.Event joinSfx;
    public AK.Wwise.Event pauseSfx;
    public AK.Wwise.Event buttonHoverSfx;
    public AK.Wwise.Event buttonClickSfx;
    public AK.Wwise.Event winSfx;

    
    private uint patternIndicator;
    private PlayerVariables[] _variables;

    #endregion
    private void Awake()
    {
        if (instance is null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        bpm = 90;
        hitMoveTime = (60f / bpm) / 2f;
        patternTime = (60f / bpm) * 8f;
        
        _variables = FindObjectsOfType<PlayerVariables>();
        AkSoundEngine.SetState("PlayerCount", "_0");


        // StartCoroutine(DelayedLobbyMusicStart());
        

        // if (_lobbyMusicPlay != null)
        // {
        //     StartCoroutine(DelayedLobbyMusicStart());
        // }
    }

    private void Start()
    {
        StartLobbyMusic();
    }

    // IEnumerator DelayedLobbyMusicStart()
    // {
    //     yield return new WaitForSeconds(0.1f);
    //     StartLobbyMusic();
    //     Debug.Log("Delayed lobby music started.");
    // }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (instance == null) return;
        if (scene.name == "MainMenu")
        {
            // _bank.Load();
        }

        if (scene.name == "Level1")
        {
            StopLobbyMusic();
            // _gameStart.Post(gameObject, callbackType, NewBeat);

            StartCoroutine(GameStartDelay());
        }
        //come back
        if (scene.name == "WinScreen")
        {
            StopGameMusic();
            StartLobbyMusic();
        }
            
            
        // if(scene.name == "CurrentGame") _metronomeEvent.Post(gameObject, callbackType, NewBeat);
    }

    IEnumerator GameStartDelay()
    {
        yield return new WaitForSeconds(1f);
        _gameStart.Post(gameObject, callbackType, NewBeat);
    }
    public void StartLobbyMusic()
    {
        _lobbyMusicPlay.Post(instance.gameObject);
    }
    public void StopLobbyMusic()
    {
        _lobbyMusicStop.Post(instance.gameObject);
    }
    public void JoinSFX()
    {
        joinSfx.Post(instance.gameObject);
        // int count = GameManager.instance.playerList.Count -1 ;
        // foreach (var playerInput in GameManager.instance.playerList)
        // {
        //     if (playerInput is null) count--;
        // }
        // if(count < 0) count = 0;
        AkSoundEngine.SetState("PlayerCount", "_" + GameManager.instance.playerList.Count);
    }
    public void StopGameMusic()
    {
        _gameStop.Post(instance.gameObject);
    }
    void NewBeat(object in_cookie, AkCallbackType in_type,  AkCallbackInfo info)
    {
         if (in_type == AkCallbackType.AK_MusicSyncUserCue)
         {
            var markerInfo = info as AkMusicSyncCallbackInfo;

            if (markerInfo is not null)
            {
                //EVENT
                if (markerInfo.userCueName == "Indicator")
                {
                    _variables = FindObjectsOfType<PlayerVariables>();
                    for (int i = 0; i < _variables.Length; i++)
                    {
                        if (_variables[i]._hasInstrument && _variables[i]._playsInstrument) _variables[i].playerEvents.PublishEvent("Indicator");
                        if (_variables[i]._hasInstrument && _variables[i]._playsInstrument) _variables[i].playerEvents.PublishEvent("IndicatorRemove");
                    }

                    // if (!UIManager.instance.isOpen)
                    // {
                    //     
                    // }
                    for (int i = 0; i < UIManager.instance.playerUIs.Count; i++ )
                    {
                        UIManager.instance.playerUIs[i].GetComponent<PlayerUI>().RotateIndicator();
                    }
                   
                }
                if (markerInfo.userCueName == "NewBeat")
                {
                    PlayInstrument[] _playInstruments = FindObjectsOfType<PlayInstrument>();
                    for (int i = 0; i < _playInstruments.Length; i++)
                    {
                        _playInstruments[i].HasInstrument();
                    }
                }
            }
         }
    }
    #region Comments
        
    // void CheckInput(object in_cookie, AkCallbackType in_type, object in_callbackInfo)
    // {
    //     Debug.Log("yay kick jz");
    // }
    public void CheckNoteHit()
    {
        // float currentTime = Time.time;
        // Debug.Log(currentTime);
        // currentTime = resetTime;
        // foreach (float noteTime in patternKick1)
        // {
        //     // if (Mathf.Abs(currentTime - noteTime) < hitWindow)
        //     if (Mathf.Abs(currentTime - noteTime) < hitWindow)
        //     {
        //         // Note hit!
        //         Debug.Log("Note hit!");
        //         // Provide visual/audio feedback, increase score, etc.
        //         break; // Exit the loop after one note hit
        //     }
        // }
        // StartCoroutine(CheckHit());
    }

    // public IEnumerator CheckHit()
    // {
    //     foreach (float noteTime in patternKick1)
    //     {
    //         float currentTime = 0;
    //         currentTime += Time.deltaTime;
    //         
    //         Debug.Log(currentTime);
    //         yield return new WaitUntil(() => currentTime > 5f);
    //         if (Mathf.Abs(currentTime - noteTime) < hitWindow)
    //         {
    //             // Note hit!
    //             Debug.Log("Note hit!");
    //             // Provide visual/audio feedback, increase score, etc.
    //             break; // Exit the loop after one note hit
    //         }
    //     }
    //     yield break;
    // }
    // private void Update()
    // {
    //     if (_checkInput)
    //     {
    //         CheckInput();
    //     }
    // }
    //
    // public void CheckInput()
    // {
    //     for (int i = 0; i < patternKick1.Length; i++)
    //     {
    //         float timer = 0f;
    //
    //         timer += patternKick1[i];
    //         if (timer == patternKick1[i]) _canHitNote = true;
    //         else if (timer != patternKick1[i]) _canHitNote = false;
    //     }
    // }
    #endregion
}
