using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{

    private static string previousSceneKey;
    public static SceneController Instance;
    private static string previousScene;

    private void Awake()
    {
        //// Pr�fen, ob es bereits eine Instanz gibt
        //if (Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

        //SceneManager.sceneLoaded += OnSceneLoaded;
        
    }


    public  void Start()
    {
        //string currentSceneName = SceneManager.GetActiveScene().name;
        //PlayerPrefs.SetString(previousSceneKey, currentSceneName);
        //PlayerPrefs.Save();

        //previousScene = SceneManager.GetActiveScene().name;
    }


    //public void GoBackPreviousScene()
    //{
    //    if (!string.IsNullOrEmpty(previousScene))
    //    {
    //        SceneManager.LoadScene(previousScene);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Keine vorherige Szene gefunden!");
    //    }
    //}

    public void ChangeScene(string sceneName)
    {
        if(sceneName == "MainMenu" || sceneName == "Credits"|| sceneName == "Controls")
        {
            GameManager.instance.EscapeWinScene();
            
        }
        if (sceneName == "Level1")
        {
            if(GameManager.instance.playerList.Count < GameManager.instance.minimumPlayers) return;
            // GameManager.instance.OnSceneLoad();
            

            // gameObject.transform.position =
            //     GameManager.instance
            //         .spawnPoints[
            //             _player[GetComponent<PlayerInput>().playerIndex].GetComponent<PlayerController>().playerIndex]
            //         .transform.position;
        }
        SceneManager.LoadScene(sceneName);
        
       
        // if (sceneName == "CharacterSelection")
        // {
        //     
        // }
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{

    //    GameObject buttonObj = GameObject.FindGameObjectWithTag("BackButton");

    //    if (buttonObj != null)
    //    {
    //        Button backButton = buttonObj.GetComponent<Button>();
            

    //        if (backButton != null)
    //        {
    //            //backButton.onClick.AddListener(() => ChangeScene("MainMenu"));
    //            GoBackPreviousScene();              
    //        }
    //    }

        //GameObject buttonObj2 = GameObject.FindGameObjectWithTag("PlayButton");

        //if (buttonObj2 != null)
        //{
        //    Button playButton = buttonObj.GetComponent<Button>();
        //    if (playButton != null)
        //    {
        //        playButton.onClick.AddListener(() => ChangeScene("Ella_Playground_Play"));
               
        //    }
        //}
    //}   
}