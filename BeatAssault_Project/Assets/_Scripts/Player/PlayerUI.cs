using System;
using Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class PlayerUI : MonoBehaviour
{
    // public CurInstrument _curInstrument;
    public PlayerVariables _variables;
    public PlayerController player;
    public PlayInstrument playInstrument;

    [SerializeField] GameObject voilinPattern;
    [SerializeField] GameObject snarePattern;
    [SerializeField] GameObject saxophonePattern;
    [SerializeField] GameObject pianoPattern;
    [SerializeField] GameObject kickPattern;
    [SerializeField] GameObject bassPattern;
    [SerializeField] GameObject[] allPatterns;

    [Header("Indicator")]
    public GameObject indicator;
    public Image redOverlay;
    public float angle = -22.5f;
    public float redIndicationDuration = .25f;
    public float redTargetAlpha = 160f;

    public GameObject deadOverlay;
    public List<GameObject> deadOverlayDisables = new List<GameObject>();
    
    public GameObject healthBar;
    public GameObject easeBar;

    public int tempIndex;
    
    
    public void AssignPlayer(int index)
    {
        StartCoroutine(AssignPlayerDelay(index));
    }
    IEnumerator AssignPlayerDelay(int index)
    {
        yield return new WaitForSeconds(.01f);
        player = GameManager.instance.playerList[index].GetComponent<PlayerInputHandler>()._playerController;
        playInstrument = player.gameObject.GetComponent<PlayInstrument>();
        _variables = player.gameObject.GetComponent<PlayerVariables>();
        // _variables.playerUI = this;
        
        _variables.playerEvents?.AddEvent("PlaysInstrumentIndicator", UpdateUIPattern);
        _variables.playerEvents?.AddEvent("PlayerMisses", NoHitFeedback);
        
        // player.GetComponent<PlayerEvents>().AddNegativeFeedback(NoHitFeedback);
        // player.gameObject.GetComponent<PlayerEvents>().AddHealthEvent(HealthDisplay);
    }

    public void RotateIndicator()
    {
        indicator.transform.Rotate(Vector3.forward, angle);

    }
    public void ResetIndicator()
    {
        indicator.transform.rotation = Quaternion.Euler(0, 0, 22.5f);

    }

    // public void ResetUI()
    // {
    //     _variables.playerUI.deadOverlay.SetActive(false);
    //     foreach (var overlay in _variables.playerUI.deadOverlayDisables)
    //     {
    //         overlay.SetActive(true);
    //     }
    //     _variables.playerUI.healthBar.GetComponent<Image>().fillAmount = 1f;
    //     _variables.playerUI.easeBar.GetComponent<Image>().fillAmount = 1f;
    //     ResetIndicator();
    // }
    private void OnDestroy()
    {
        if (_variables is not null)
        {
            _variables.playerEvents?.RemoveEvent("PlayerMisses", NoHitFeedback);
            _variables.playerEvents?.RemoveEvent("PlaysInstrumentIndicator", UpdateUIPattern);
            
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameObject.SetActive(true);
        for (int i = 0; i < GameManager.instance.playerList.Count; i++)
        {
            if (tempIndex == GameManager.instance.playerList[i].playerIndex)
            {
                player = GameManager.instance.playerList[i].GetComponent<PlayerInputHandler>()._playerController;
                _variables = player.gameObject.GetComponent<PlayerVariables>();
                playInstrument = player.gameObject.GetComponent<PlayInstrument>();
                
                // UIManager.instance.joinMessages[player.playerIndex].SetActive(false);
            }
            
        }
        for (int i = 0; i < allPatterns.Length; i++)
        {
            allPatterns[i].SetActive(false);
        }
        // if (tempIndex <= GameManager.instance.playerList.Count)
        // {
        //     player = GameManager.instance.playerList[tempIndex].GetComponent<PlayerInputHandler>()._playerController;
        //     _variables = player.gameObject.GetComponent<PlayerVariables>();
        //     playInstrument = player.gameObject.GetComponent<PlayInstrument>();
        //     
        //     UIManager.instance.joinMessages[player.playerIndex].SetActive(false);
        // }
            
        // player = GameManager.instance.playerList[tempIndex].gameObject.transform.parent.GetComponent<PlayerController>();
        // print("yo " + GameManager.instance.playerList[tempIndex].gameObject.transform.parent.GetComponent<PlayerController>());
        // AssignPlayer(player.playerIndex);
        
    }

    public void UpdateUIPattern()
    {
        for (int i = 0; i < allPatterns.Length; i++)
        {
            foreach (Transform child in allPatterns[i].transform)
            {
                child.gameObject.GetComponent<Image>().color = Color.white;
            }
        }
        // foreach (Transform child in _variables.curPatternUI.transform)
        // {
        //     child.gameObject.GetComponent<Image>().color = Color.white;
        // }
    }

    
    // public void HealthDisplay(float health)
    // {
    //     int healthInt = (int)health;
    //     this.health.SetText(healthInt.ToString());
    // }
    public void ChangePatternUI()
    {
        for (int i = 0; i < allPatterns.Length; i++)
        {
            foreach (Transform child in allPatterns[i].transform)
            {
                child.gameObject.GetComponent<Image>().color = Color.black;
            }
        }
        if (playInstrument is null) return;
        switch (playInstrument.curInstrumentEnum)
        {
            case CurInstrument.None:
                for (int i = 0; i < allPatterns.Length; i++) {allPatterns[i].SetActive(false); }
                break;
            case CurInstrument.HasViolin:
                for (int i = 0; i < allPatterns.Length; i++) {allPatterns[i].SetActive(false); }
                voilinPattern.SetActive(true);
                _variables.curPatternUI = voilinPattern;
                break;
            case CurInstrument.HasSnare:
                for (int i = 0; i < allPatterns.Length; i++) {allPatterns[i].SetActive(false); }
                snarePattern.SetActive(true);
                _variables.curPatternUI = snarePattern;
                break;
            case CurInstrument.HasSaxophone:
                for (int i = 0; i < allPatterns.Length; i++) {allPatterns[i].SetActive(false); }
                saxophonePattern.SetActive(true);
                _variables.curPatternUI = saxophonePattern;
                break;
            case CurInstrument.HasPiano:
                for (int i = 0; i < allPatterns.Length; i++) {allPatterns[i].SetActive(false); }
                pianoPattern.SetActive(true);
                _variables.curPatternUI = pianoPattern;
                _variables._pianoPatternBalls.transform.localPosition = new Vector3(0, 1, 0);
                break;
            case CurInstrument.HasKick:
                for (int i = 0; i < allPatterns.Length; i++) {allPatterns[i].SetActive(false); }
                kickPattern.SetActive(true);
                _variables.curPatternUI = kickPattern;
                _variables._kickPatternBalls.transform.localPosition = new Vector3(0, 1, 0);
                break;
            case CurInstrument.HasBass:
                for (int i = 0; i < allPatterns.Length; i++) {allPatterns[i].SetActive(false); }
                bassPattern.SetActive(true);
                _variables.curPatternUI = bassPattern;
                break;
        }
    }

    private void Awake()
    {
        // _variables = GetComponent<PlayerVariables>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        

    }
    // private void OnDisable()
    // {
    //     player.GetComponent<PlayerEvents>().Remove(NoHitFeedback);
    // }
    public void NoHitFeedback()
    {
        // if (player is not null && player.misses)
        // {
        //     StartCoroutine(RedFeedback());
        // }
        // if (player is not null && player.misses)
        // {
            StartCoroutine(RedFeedback());
        //}
    }

    IEnumerator RedFeedback()
    {
        redOverlay.gameObject.SetActive(true);
        yield return new WaitForSeconds(redIndicationDuration);
        redOverlay.gameObject.SetActive(false);
        _variables.misses = false;
    }
}
