using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    
    // public GameObject[] playerUIs = new GameObject[4];
    public List<GameObject> playerUIs = new List<GameObject>();
    // public GameObject[] playerUIs = new GameObject[4];
    public GameObject[] joinMessages;

    //Escape
    public bool isOpen =false;
    public GameObject menu;
    // Escape 
    public GameObject ui;
    public Countdown countdown;
    
    
    public List<GameObject> deadOverlays = new List<GameObject>();
    public List<GameObject> deadOverlayDisables = new List<GameObject>();
    public List<GameObject> indicators = new List<GameObject>();
    
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
        ui = transform.GetChild(0).gameObject;

        countdown = ui.GetComponent<Countdown>();
        DontDestroyOnLoad(gameObject);

        //vor start dass 1 player recognized wird (kann spáter gechanged werden)
        // GameManager.instance.PlayerJoinedGame += PlayerJoinedGame;
        // GameManager.instance.PlayerLeftGame += PlayerLeftGame;

        // GameManager.instance.PlayerLeftGame += HideUI;
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        foreach (var playerUI in playerUIs)
        {
           PlayerUI ui = playerUI.GetComponent<PlayerUI>();
           deadOverlays.Add(ui.deadOverlay);
           for (int i = 0; i < ui.deadOverlayDisables.Count; i++)
           {
                deadOverlayDisables.Add(ui.deadOverlayDisables[i]);
           }
           indicators.Add(ui.indicator);
        }
    }

    public void ResetUI()
    {
        foreach (var overlay in deadOverlays)
        {
            overlay.SetActive(false);
        }
        foreach (var overlay in deadOverlayDisables)
        {
            overlay.SetActive(true);
        }
        // _variables.playerUI.healthBar.GetComponent<Image>().fillAmount = 1f;
        // _variables.playerUI.easeBar.GetComponent<Image>().fillAmount = 1f;
        countdown.gameObject.SetActive(true);
        countdown.Reset();
        ResetIndicator();
    }
    public void ResetIndicator()
    {
        foreach (var indicator in indicators)
        {
            indicator.transform.rotation = Quaternion.Euler(0, 0, 22.5f);
        }

    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level1")
        {
            ui.SetActive(true);
            countdown.StartCountdown();
            // for (int i = 0; i < 3; i++)
            // {
            //     playerUIs.Add(GameObject.FindGameObjectWithTag("UI" + (i + 1)));
            //
            //     if (playerUIs[i] == null)
            //     {
            //         Debug.LogError($"UI{i + 1} not found!");
            //     }
            // }
            // playerUIs[0] = GameObject.FindGameObjectWithTag("UI1");
            // playerUIs[1] = GameObject.FindGameObjectWithTag("UI2");
            // playerUIs[2] = GameObject.FindGameObjectWithTag("UI3");
            // playerUIs[3] = GameObject.FindGameObjectWithTag("UI4");
        }
        else
        {
            ui.SetActive(false);
            // Array.Clear(playerUIs, 0, playerUIs.Length);
            // for (int i = 0; i < playerUIs.Count; i++)
            // {
            //     // playerUIs[i] = null;
            //     playerUIs.Clear();
            // }
        }
    }


    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //
        //     if (isOpen == true)
        //     {
        //         Resume();
        //         // AudioManager.instance._gameContinue.Post(gameObject);
        //
        //         // if (!isOpen) OpenMenu();
        //         // else CloseMenu();
        //     }
        //
        //     else if (isOpen == false)
        //     {
        //         Pause();
        //         // AudioManager.instance._gamePause.Post(gameObject);
        //     }
        //
        //     //Debug.Log(isOpen);
        // }
        //Debug.Log(isOpen);
    }

    //public void OpenMenu()
    //{
    //    if (isOpen == true)
    //    {
    //        menu.SetActive(false);
    //        Time.timeScale = 1f;
    //        isOpen = false;
    //    }
    //    else if (isOpen == false)
    //    {       
    //        menu.SetActive(true);
    //        Time.timeScale = 0f;
    //        isOpen = true;
    //    }

    //}

    public void Resume()
    {
        AudioManager.instance._gameContinue.Post(AudioManager.instance.gameObject);

        menu.SetActive(false);
        Time.timeScale = 1f;
        isOpen = false;
        
    }

    public void Pause()
    {
        AudioManager.instance.pauseSfx.Post(AudioManager.instance.gameObject);

        menu.SetActive(true);
        Time.timeScale = 0f;
        isOpen = true;
    }

    //public void Restart()
    //{
    //    Time.timeScale = 1.0f;
    //}



    //escape ^
    
   
    void PlayerJoinedGame(PlayerInput playerInput)
    {
        ShowUI(playerInput);   
    }
    void PlayerLeftGame(PlayerInput playerInput)
    {
        HideUI(playerInput);
    }

    // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     ShowUI(playerInput);   
    // }
    

    void ShowUI(PlayerInput playerInput)
    {
        // playerUIs[playerInput.playerIndex].SetActive(true);
        // playerUIs[playerInput.playerIndex].GetComponent<PlayerUI>().AssignPlayer(playerInput.playerIndex);
        // joinMessages[playerInput.playerIndex].SetActive(false);
    }

    void HideUI(PlayerInput playerInput)
    {
        // playerUIs[playerInput.playerIndex].SetActive(false);
        // joinMessages[playerInput.playerIndex].SetActive(true);
    }
}
