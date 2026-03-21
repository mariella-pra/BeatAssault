using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerHealthControl : MonoBehaviour, IHitable
{
    private PlayerVariables _variables;
    private GameAssets _gameAssets;
    private bool checkPlayers;
    
    public bool damagePopupActive = true;
    private void Awake()
    {
        _variables = GetComponent<PlayerVariables>();
        _gameAssets = FindObjectOfType<GameAssets>();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level1")
        {
            SetHealthBar();
            checkPlayers = true;
        }
        else checkPlayers = false;
    }
    public void SetHealthBar()
    {
        
       _variables._playerFeedback.easeBar = _variables.playerUI.easeBar.GetComponent<Image>();
       _variables._playerFeedback.healthBar = _variables.playerUI.healthBar.GetComponent<Image>();
       _variables._playerFeedback.healthbarSlider = _variables._playerFeedback.healthBar.transform.parent.gameObject.GetComponent<Slider>();
       _variables._playerFeedback.easeBarSlider = _variables._playerFeedback.easeBar.transform.parent.gameObject.GetComponent<Slider>();
       
        if (!Mathf.Approximately(_variables._playerFeedback.healthbarSlider.maxValue, _variables.maxHealth))
        {
            _variables._playerFeedback.healthbarSlider.maxValue = _variables.maxHealth; //linkeeen das man es nur einmal eingeben muss
        }
        if (!Mathf.Approximately(_variables._playerFeedback.easeBarSlider.maxValue, _variables.maxHealth))
        {
            _variables._playerFeedback.easeBarSlider.maxValue = _variables.maxHealth; //linkeeen das man es nur einmal eingeben muss
        }
        if (!Mathf.Approximately(_variables._playerFeedback.healthbarSlider.value, _variables.playerHealth))
        {
            _variables._playerFeedback.healthbarSlider.value = _variables.playerHealth; //linkeeen das man es nur einmal eingeben muss
        }
        if (!Mathf.Approximately(_variables._playerFeedback.easeBarSlider.value, _variables.playerHealth))
        {
            _variables._playerFeedback.easeBarSlider.value = _variables.playerHealth; //linkeeen das man es nur einmal eingeben muss
        }

        if (_variables._playerFeedback.healthbarSlider.value != _variables._playerFeedback.easeBarSlider.value)
        {
            _variables._playerFeedback.easeBarSlider.value = Mathf.Lerp(_variables._playerFeedback.easeBarSlider.value, _variables._playerFeedback.healthbarSlider.value, Time.deltaTime * 5f);
            //_variables.playerUI.healthBar.GetComponent<Image>().fillAmount = Mathf.Lerp(_variables.healthBeforeDamage, _variables.playerHealth, .2f);
        }
    }

    private void Update()
    {
        CheckHealth();
        if (checkPlayers)
        {
            Vector3 screenPosition = Camera.main.WorldToViewportPoint(transform.position);

            if (screenPosition.x < -0.1f || screenPosition.x > 1.1f || 
                screenPosition.y < -0.1f || screenPosition.y > 1.1f)
            {
                DamagePlayer(_variables.outOfBoundsDamage);
            }
        }
        
    }
    private void OnEnable()
    {
        _variables.playerEvents?.AddEvent("playerDeath01", Dead);
        _variables.playerEvents?.AddEvent("playerDeath", DeadAfterAnimation);
        _variables.playerEvents?.AddEvent("damagePlayer", CheckHealth);
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    private void OnDestroy()
    {
        _variables.playerEvents?.RemoveEvent("playerDeath01", Dead);
        _variables.playerEvents?.RemoveEvent("playerDeath", DeadAfterAnimation);
        _variables.playerEvents?.RemoveEvent("damagePlayer", CheckHealth);
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }
    public void Hit(float damage)
    {
        DamagePlayer(damage);
    }
    public void DamagePlayer(float damage)
    {
        if(_variables.dead) return;
        if(GameManager.instance.activePlayerList.Count <= 1) return;
        _variables.playerHealth -= damage;
        _variables.playerEvents.PublishEvent("damagePlayer");
        DamageFeedback(damage, transform.position);
    }
    public void DamageFeedback(float damage, Vector3 position)
    {
        if (damagePopupActive)
        {
            Transform damagePopupTransform = Instantiate(_gameAssets.damagePopup, position, Quaternion.identity);
            DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
            if (damage < 1) damage = 1;
            damagePopup.Setup((int) damage);
            StartCoroutine(Popup());
        }
        // Transform damagePopupTransform = Instantiate(_gameAssets.damagePopup, position, Quaternion.identity);
        // DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        // if (damage < 1) damage = 1;
        // damagePopup.Setup((int) damage);
    }
    IEnumerator Popup()
    {
        damagePopupActive = false;
        yield return new WaitForSeconds(.3f);
        damagePopupActive = true;
    }
    public void CheckHealth()
    {
        //DEAD
        if (_variables.playerHealth <= 0)
        {
            StartCoroutine(Die());
        }
        //LOW HEALTH
        if (_variables.playerHealth <= _variables.lowHealthIndicator && _variables.playerHealth != 0) 
        {
            _variables._lowHealth = true;
            _variables.playerEvents?.PublishEvent("lowHealth");
        }
    }
    public IEnumerator Die()
    {

        if (!_variables.dead && GameManager.instance.inactivePlayerList.Count < GameManager.instance.gamePlayerCount - 1)
        {
            _variables.dead = true;
            if (GameManager.instance.activePlayerList.Count > 2)
            {
                // _variables.playerEvents?.PublishEvent("LooseInstrument");
                GameManager.instance.inactivePlayerList.Add(gameObject);
                
                    _variables.playerEvents?.PublishEvent("playerDeath01");
                    Instantiate(_variables.ray, gameObject.transform.GetChild(0).transform.position, Quaternion.identity);
                    _variables.animationController.PlayDeathAnimation();
                    yield return new WaitForSeconds(GameAssets.instance.playerDeathLength);
                    _variables.playerEvents?.PublishEvent("playerDeath");
            }
            else _variables.playerEvents?.PublishEvent("playerDeath");
        }
        
    }

    public void DeadAfterAnimation()
    {
        GameManager.instance.CheckPlayersLeft();
        GameManager.instance.activePlayerList.Remove(gameObject);
        GameManager.instance.CheckPlayersLeft();
        
        // PlayerInput playerInput = GetComponentInChildren<PlayerInput>();
        // GameManager.instance.deadPlayer = gameObject;
        // if (playerInput is not null)
        // {
        //     playerInput.gameObject.SetActive(false);
        //     Destroy(playerInput);
        // }
        Destroy(gameObject);
    }
    public void Dead()
    {
        // _variables.controller._controller.
        
        _variables.controller.canMove = false;
        _variables._lowHealth = false;
        _variables.playerUI.healthBar.GetComponent<Image>().fillAmount = 0f;
        _variables.playerUI.easeBar.GetComponent<Image>().fillAmount = 0f;
        
        _variables.playerUI.deadOverlay.SetActive(true);
        foreach (var overlay in _variables.playerUI.deadOverlayDisables)
        {
            overlay.SetActive(false);
        }
        
        PlayerInput playerInput = GetComponentInChildren<PlayerInput>();
        GameManager.instance.deadPlayer = gameObject;
        if (playerInput is not null)
        {
            playerInput.gameObject.SetActive(false);
            Destroy(playerInput);
        }
        
        // GameManager.instance.activePlayerList.Remove(gameObject);
        // GameManager.instance.CheckPlayersLeft();
        //
        // PlayerInput playerInput = GetComponentInChildren<PlayerInput>();
        // GameManager.instance.deadPlayer = gameObject;
        // if (playerInput is not null)
        // {
        //     playerInput.gameObject.SetActive(false);
        //     Destroy(playerInput);
        // }
        // Destroy(gameObject);

        // GameManager.instance._events.PublishEvent("checkPlayersLeft");

        // GameManager.instance.playerList.Remove(gameObject.GetComponent<PlayerInput>());

        for (int i = 0; i < GameManager.instance.playerList.Count; i++)
        {
            // GameManager.instance.activePlayerList.Add(GameManager.instance.playerList[i].gameObject);
        }
        for (int i = 0; i < GameManager.instance.activePlayerList.Count; i++)
        {
            // if (GameManager.instance.activePlayerList[i] != null) return;
            // GameManager.instance.activePlayerList.RemoveAt(i);
        }
        
        
        // GameManager.instance.playerList.RemoveAll(item => item == null);
        // GameManager.instance.activePlayerList.Remove(gameObject);
        // for (int i = 0; i < GameManager.instance.playerList.Count; i++)
        // {
        //     if (GameManager.instance.playerList[i] != null) return;
        //     GameManager.instance.playerList.RemoveAt(i);
        // }
        // GameManager.instance.playerList.RemoveAll(item => item == null || ReferenceEquals(item, null));
        

        // GameManager.instance.RemovePlayer(gameObject);
        
        //UNCLEAN
        // List<BallGroup> ballGroups = new List<BallGroup>();
        // ballGroups.Add(FindObjectOfType<BallGroup>());
        // if (ballGroups.Count > 0)
        // {
        //     for (int i = 0; i < ballGroups.Count; i++)
        //     {
        //         //!!!!!!!!!!!
        //         // if(ballGroups[i]._playerShooting.gameObject == gameObject) ballGroups[i].DestroyGroup();
        //     }
        // }
        
    }

    
}