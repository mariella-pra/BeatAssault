using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public string playScene;
    //UIManager uiManager;

    public void Update()
    {
        //GetComponent<UIManager>();
        //uiManager = FindObjectOfType<UIManager>();
    }
    
    
    public void OnPress()
    {
        Time.timeScale = 1.0f;
        //ella muss hier kurz ran sorry
        if(playScene == "CharacterSelection" || playScene == "MainMenu"|| playScene == "Credits")
        {
            for (int i = 0; i < UIManager.instance.playerUIs.Count; i++ )
            {
                // UIManager.instance.playerUIs[i].GetComponent<PlayerUI>().ResetUI();
                UIManager.instance.ResetUI();
            }
            
            AudioManager.instance.StartLobbyMusic();
            AudioManager.instance.StopGameMusic();
        }

        if (playScene == "CharacterSelection")
        {
            GameManager.instance.ResetAll();
            GameManager.instance.UnregisterAllPlayers();
        }

        UIManager.instance.Resume();
        SceneManager.LoadScene(playScene);
        
        
    }
    public void OnQuit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(playScene);
    }

    public void Resume()
    {
        UIManager.instance.Resume();
    }

}
