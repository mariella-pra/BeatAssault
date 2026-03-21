using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public int gamePlayerCount;
    
    public int minimumPlayers;
    public List<GameObject> spawnPoints = new List<GameObject>();
    public static GameManager instance = null;
    public List<PlayerInput> playerList = new List<PlayerInput>();
    public List<PlayerInput> playerListControllers = new List<PlayerInput>();
    public List<GameObject> activePlayerList = new List<GameObject>();
    public List<GameObject> inactivePlayerList = new List<GameObject>();
    public event Action<PlayerInput> PlayerJoinedGame;
    public event Action<PlayerInput> PlayerLeftGame;
    [SerializeField] InputAction joinAction;
    [SerializeField] InputAction leaveAction;

    public PlayerEvents _events;
    public GameObject _winScreenSpawn;
    public GameObject deadPlayer;

    private PlayerInputManager _inputManager;
    public bool gameScene;

    public GameObject winner;
    
    // List<GameObject> players
    public int winnerIndex;
    public string winnerSound;
    public GameObject[] _player;
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

        _inputManager = GetComponent<PlayerInputManager>();
        
        // joinAction.Enable();
        // joinAction.started += context => JoinAction(context);
        // leaveAction.Enable();
        
        // leaveAction.started += context => LeaveAction(context);
        _events = GetComponent<PlayerEvents>();
    }

    private void OnSceneUnloaded(Scene scene)
    {
        // if(scene.name == "Level1")
        // {
        //     inactivePlayerList.Clear();
        // }
    }
    

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        
        spawnPoints.Clear();
        for (int i = 0; i < spawnPoints.Count; i++) spawnPoints.Remove(spawnPoints[i]);
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("SpawnPoint");
        for (int i = 0; i < spawns.Length; i++) spawnPoints.Add(spawns[i].gameObject);
        spawnPoints.Sort((a, b) =>
        {
            int numA = int.Parse(a.name);
            int numB = int.Parse(b.name);
            return numA.CompareTo(numB);
        });
        
        joinAction.Enable();
        joinAction.started += context => JoinAction(context);
        leaveAction.Enable();
        leaveAction.started += context => LeaveAction(context);
        
        
        if(scene.name != "CharacterSelection")
        {
            joinAction.Disable();
            leaveAction.Disable();
            inactivePlayerList.Clear();
        }

        if (scene.name == "Level1")
        {
            PlayerController[] players = FindObjectsOfType<PlayerController>();
            for (int i = 0; i < players.Length; i++)
            {
                activePlayerList.Add(players[i].gameObject);
            }
            gamePlayerCount = activePlayerList.Count;
            
            // gameScene = true;
        }
        // if(scene.name == "WinScene") inactivePlayerList.Clear();
        // else gameScene = false;

        // else
        // {
        //     joinAction.Disable();
        //     leaveAction.Disable();
        // }

        // PlayerInput[] players = GameObject.FindObjectsByType<PlayerInput>();
        // activePlayerList.Add();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;

        _events?.AddEvent("checkPlayersLeft", CheckPlayersLeft);
        _events?.AddEvent("killAll", KillPlayers);
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
        
        _events?.RemoveEvent("killAll", KillPlayers);
    }
    // private void OnDisable()
    // {
    //     // SceneManager.sceneLoaded -= OnSceneLoaded;
    //     _events.RemoveEvent("killAll", KillPlayers);
    // }

    public void ResetAll()
    {
        for (int i = 0; i < activePlayerList.Count; i++)
        {
            activePlayerList[i].GetComponent<PlayInstrument>().ResetInstrument();
        }
        for (int i = 0; i < UIManager.instance.playerUIs.Count; i++ )
        {
            // UIManager.instance.playerUIs[i].GetComponent<PlayerUI>().ResetUI();
            UIManager.instance.ResetUI();
            
            MonoBehaviour[] allBehaviours = FindObjectsOfType<MonoBehaviour>(true);

            foreach (var behaviour in allBehaviours)
            {
                if (behaviour is IResetable resetable)
                {
                    resetable.Reset();
                }
            }

        }
    }
    private void Update()
    {
        
        // if (SceneManager.GetActiveScene().name == "Level1") _events?.PublishEvent("checkPlayersLeft");
        
        
        // Debug.Log(activePlayerList.Count);
        // if (gameScene)
        // {
        //     if (activePlayerList.Count <= 1)
        //     {
        //         CheckPlayersLeft();
        //     }
        // }
        
        //
        
        // Scene currentScene = SceneManager.GetActiveScene ();
        // string scene = currentScene.name;
        // if(scene == "CurrentGame")
        // {
        //     // _events?.PublishEvent("checkPlayersLeft", CheckPlayersLeft);
        //     CheckPlayersLeft();
        // }
    }

    public void CheckPlayersLeft()
    {
        // List<PlayerController> players = new List<PlayerController>();
        // Debug.Log(players.Count);
        
        if(activePlayerList.Count == 1)
        {
            winner = activePlayerList[0].gameObject;
            winnerIndex = winner.gameObject.GetComponent<PlayerController>().playerIndex;
            WinnerSound();
            
            activePlayerList[0].gameObject.GetComponent<PlayerVariables>().playerEvents.PublishEvent("Win");
            Invoke("WinScene", 1f);
        }
        
        // PlayerController[] playersFound = FindObjectsOfType<PlayerController>();
        
        // for (int i = 0; i < playersFound.Length; i++)
        // {
            // players.Add(players[i]);
        // }
        // int playerCount = players.Length - 1;
        // for (int i = 0; i < players.Count; i++)
        // {
        //     if (players[i].gameObject == deadPlayer)
        //     {
        //         players.Remove(players[i]);
        //     }
        // }
        // int playerCount = players.Count - 1;
        // if(players.Count <= 1)
        // {
        //     //gameover
        //     WinScene();
        //     
        // }
    }
    public void WinScene()
    {
        // AudioManager.instance._gameStop.Post(AudioManager.instance.gameObject);
        StopGame();
        
        // WinnerSound();
        
        for (int i = 0; i < activePlayerList.Count; i++)
        {
            activePlayerList[i].GetComponent<PlayerVariables>()._curPattern = null;
            activePlayerList[i].GetComponent<PlayerVariables>()._playsInstrument = false;
            activePlayerList[i].GetComponent<PlayerVariables>()._hasInstrument = false;

        }
        // for (int i = 0; i < activePlayerList.Count; i++)
        // {
        //     Destroy(activePlayerList[i]);
        // }
        UnregisterAllPlayers();
        SceneManager.LoadScene("WinScreen");
        AkSoundEngine.SetState("WinInstrument", winnerSound);
        AudioManager.instance.winSfx.Post(AudioManager.instance.gameObject);
        
        // _winScreenSpawn = GameObject.FindGameObjectWithTag("WinScreenSpawn");
        _winScreenSpawn.transform.GetChild(winnerIndex).gameObject.SetActive(true);
        // Instantiate( _player[winnerIndex], _winScreenSpawn.transform.position, Quaternion.identity);
        
        // GameObject player = FindObjectOfType<PlayerController>().gameObject;

        // player.transform.position = _winScreenSpawn.transform.position;
    }

    public void WinnerSound()
    {
        PlayerVariables winnerVar = winner.GetComponent<PlayerVariables>();
        if(winnerVar._curPattern == AudioManager.instance._snarePattern) winnerSound = "Snare";
        if(winnerVar._curPattern == AudioManager.instance._kickPattern) winnerSound = "Kick";
        if(winnerVar._curPattern == AudioManager.instance._bassPattern) winnerSound = "Bass";
        if(winnerVar._curPattern == AudioManager.instance._pianoPattern) winnerSound = "Piano";
        if(winnerVar._curPattern == AudioManager.instance._saxophonePattern) winnerSound = "Sax";
        if(winnerVar._curPattern == AudioManager.instance._violinPattern) winnerSound = "Violin";
        if(winnerVar._curPattern is null) winnerSound = "Snare";
    }

    public void EscapeWinScene()
    {
        for (int i = 0; i < UIManager.instance.playerUIs.Count; i++ )
        {
            // UIManager.instance.playerUIs[i].GetComponent<PlayerUI>().ResetUI();
            UIManager.instance.ResetUI();

        }

        for (int i = 0; i < _winScreenSpawn.transform.childCount; i++)
        {
            _winScreenSpawn.transform.GetChild(i).gameObject.SetActive(false);
        }
        // _winScreenSpawn.transform.GetChild(winnerIndex).gameObject.SetActive(false);
        // AudioManager.instance.StopLobbyMusic();
        UnregisterAllPlayers();
    }

    public void StopGame()
    {
        AudioManager.instance.StopGameMusic();
        // for (int i = 0; i < playerList.Count; i++)
        // {
        //     playerList[i].GetComponent<PlayerVariables>()._curPattern = null;
        //     playerList[i].GetComponent<PlayerVariables>()._playsInstrument = false;
        //     playerList[i].GetComponent<PlayerVariables>()._hasInstrument = false;
        //     
        // }
    }
    public void KillPlayers()
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            Destroy(playerList[i].gameObject);
            playerList.Remove(playerList[i]);
            Destroy(playerList[i].transform.parent.gameObject);
            // _inputManager.pla
        }
    }
    
    
    #region PlayerJoin
    public void JoinAction(InputAction.CallbackContext context)
    {
        PlayerInputManager.instance.JoinPlayerFromActionIfNotAlreadyJoined(context);
    }
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        playerList.Add(playerInput);
        playerListControllers.Add(playerInput);
        if (PlayerJoinedGame is not null) PlayerJoinedGame(playerInput);
        // AudioManager.instance.joinSfx.Post(AudioManager.instance.gameObject);
        AudioManager.instance.JoinSFX();
    }
    #endregion
    #region PlayerLeave
    public void OnPlayerLeft(PlayerInput playerInput)
    {
        playerList.Remove(playerInput);
        if (PlayerLeftGame is not null) PlayerLeftGame(playerInput);
    }
    public void LeaveAction(InputAction.CallbackContext context)
    {
        if (playerList.Count > 1)
        {
            foreach (var player in playerList)
            {
                foreach (var device in player.devices)
                {
                    if (device is not null && context.control.device == device)
                    {
                        UnregisterPlayer(player);
                        return;
                    }   
                }
            }
        }
    }

    void UnregisterPlayer(PlayerInput playerInput)
    {
        //added
        playerInput.DeactivateInput();

        playerList.Remove(playerInput);
        if (PlayerLeftGame is not null)
        {
            PlayerLeftGame(playerInput);
        }
    
        Destroy(playerInput.transform.parent.gameObject);
        AkSoundEngine.SetState("PlayerCount", "_" + GameManager.instance.playerList.Count);
    }

    public void UnregisterAllPlayers()
    {
        for (int i = playerList.Count - 1; i >= 0; i--)
        {
            playerList[i].DeactivateInput();
            UnregisterPlayer(playerList[i]);
        }
        playerList.Clear();
        activePlayerList.Clear();
        inactivePlayerList.Clear();
        playerListControllers.Clear();
        AkSoundEngine.SetState("PlayerCount", "_" + GameManager.instance.playerList.Count);

        // if (playerList.Count >= 1)
        // {
        //     foreach (var player in playerList)
        //     {
        //         foreach (var device in player.devices)
        //         {
        //             if (device is not null)
        //             {
        //                 playerList.Clear();
        //                 if (PlayerLeftGame is not null)
        //                 {
        //                     foreach (var playerInput in playerList)
        //                     {
        //                         PlayerLeftGame(playerInput);
        //                         Destroy(playerInput.transform.parent.gameObject);
        //                     }
        //                 }
        //                 return;
        //             }   
        //         }
        //     }
        // }
    }

    #endregion
}