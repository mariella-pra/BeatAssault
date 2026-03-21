using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PlayerFeedback : MonoBehaviour
{
    private PlayerVariables _variables;
    
    // public List<MeshRenderer> meshes;
    [SerializeField] List<SkinnedMeshRenderer> playerMeshes;
    [SerializeField] SkinnedMeshRenderer _scarf;
    
    [SerializeField] Color flashColor = Color.red;
    [SerializeField] Color freezeColor = Color.grey;
    
    private Color originalColor = Color.black;
    private float flashTime = 0.15f;
    private float blinkTime = 0.35f;
    public float freezeTime = 1f;

    public CurInstrument curInstrumentEnum;
    PlayerController _contr;

    [SerializeField] private GameObject[] instruementMeshes;
    // [SerializeField] Material _blinkMat;

    public Image easeBar;
    public Image healthBar;
    
    public Slider healthbarSlider;
    public Slider easeBarSlider;

    public bool damageParticlesActive = true;

    private void Awake()
    {
        _variables = gameObject.GetComponent<PlayerVariables>();
        _contr = _variables.controller;
    }
    private void OnEnable()
    {
        RegisterEvents(true);
    }

    private void OnDestroy()
    {
        RegisterEvents(false);
    }

    private void RegisterEvents(bool subscribe)
    {
        var events = _variables.playerEvents;
        if (events == null) return;

        if (subscribe)
        {
            events.AddEvent("LooseInstrument", HideIndicator);
            events.AddEvent("PlayInstrument", ShowIndicator);
            events.AddEvent("playerDeath01", DropInstrument);
            events.AddEvent("playerDeath01", HideIndicator);
            events.AddEvent("playerDeath01", HideInstrument);
            events.AddEvent("playerDeath01", PostDeathSound);
            events.AddEvent("playerDeath01", HealthUI);
            events.AddEvent("damagePlayer", DamageIndicator);
            events.AddEvent("damagePlayer", HealthUI);
            events.AddEvent("damagePlayer", DamageParticles);
            events.AddEvent("lowHealth", LowHealth);
            events.AddEvent("PickupInstrument", ShowInstrument);
            events.AddEvent("LooseInstrument", HideInstrument);
            events.AddEvent("PlayerMisses", Freeze);
            events.AddEvent("Win", Win);
        }
        else
        {
            events.RemoveEvent("LooseInstrument", HideIndicator);
            events.RemoveEvent("PlayInstrument", ShowIndicator);
            events.RemoveEvent("playerDeath01", DropInstrument);
            events.RemoveEvent("playerDeath01", PostDeathSound);
            events.RemoveEvent("playerDeath01", HideIndicator);
            events.RemoveEvent("playerDeath01", HideInstrument);
            events.RemoveEvent("playerDeath01", HealthUI);
            events.RemoveEvent("damagePlayer", DamageIndicator);
            events.RemoveEvent("damagePlayer", HealthUI);
            events.RemoveEvent("damagePlayer", DamageParticles);
            events.RemoveEvent("lowHealth", LowHealth);
            events.RemoveEvent("PickupInstrument", ShowInstrument);
            events.RemoveEvent("LooseInstrument", HideInstrument);
            events.RemoveEvent("PlayerMisses", Freeze);
            events.RemoveEvent("Win", Win);
        }
    }

    private void ShowIndicator()
    {
        _variables.indicator.SetActive(true);
    }

    private void HideIndicator()
    {
        _variables.indicator.SetActive(false);
        Debug.Log(gameObject.name);
    }

    private void Win()
    {
        GameManager.instance.winnerIndex = gameObject.GetComponent<PlayerController>().playerIndex;
        GameManager.instance.winner = gameObject;
        //either here so that he can move
    }

    public void DropInstrument()
    {
        int random = UnityEngine.Random.Range(0, GameAssets.instance.instruments.Count);
        Vector3 pos = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
        Instantiate(GameAssets.instance.instruments[random].gameObject, pos, Quaternion.identity);
    }

    public void Freeze()
    {
        StartCoroutine(FreezePlayer());
    }
    IEnumerator FreezePlayer()
    {
        _contr.canMove = false;
        yield return new WaitForSeconds(freezeTime);
        _contr.canMove = true;
    }
    private void ShowInstrument()
    {
        curInstrumentEnum = _variables.gameObject.GetComponent<PlayInstrument>().curInstrumentEnum;

        HideInstrument();

        switch (curInstrumentEnum)
        {
            case CurInstrument.HasViolin:
                instruementMeshes[0].SetActive(true);
                break;
            case CurInstrument.HasSnare:
                instruementMeshes[1].SetActive(true);
                break;
            case CurInstrument.HasSaxophone:
                instruementMeshes[2].SetActive(true);
                break;
            case CurInstrument.HasPiano:
                instruementMeshes[3].SetActive(true);
                break;
            case CurInstrument.HasKick:
                instruementMeshes[4].SetActive(true);
                break;
            case CurInstrument.HasBass:
                instruementMeshes[5].SetActive(true);
                break;
        }
    }
    public void HideInstrument()
    {
        for(int i = 0; i < instruementMeshes.Length; i++)
        {
            instruementMeshes[i].SetActive(false);
        }
    }
    public void HealthUI()
    {
        _variables.playerUI.healthBar.GetComponent<Image>().fillAmount = _variables.playerHealth / 100f;


        if (healthbarSlider.value != _variables.playerHealth)
        {
            healthbarSlider.value = _variables.playerHealth;
        }


        if (healthbarSlider.value != easeBarSlider.value)
        {
            easeBarSlider.value = Mathf.Lerp(easeBarSlider.value, _variables.playerHealth, Time.deltaTime * 2f);
            //_variables.playerUI.healthBar.GetComponent<Image>().fillAmount = Mathf.Lerp(_variables.healthBeforeDamage, _variables.playerHealth, .2f);
        }




        // float startValue = _variables.playerUI.easeBar.GetComponent<Image>().fillAmount;
        // float endValue = _variables.playerHealth / 100f;
        // float duration = _variables.lerpVal;
        //
        // LeanTween.value(gameObject, startValue, endValue, duration)
        //     .setEase(LeanTweenType.easeInOutQuad);

        // Invoke("EaseBar", _variables.lerpVal);
        // _variables.playerUI.healthBar.GetComponent<Image>().fillAmount = _variables.playerHealth / 100f;

        // _variables.playerUI.healthBar.GetComponent<Image>().fillAmount = Mathf.Lerp(_variables.healthBeforeDamage, _variables.playerHealth, .2f);

        // easeBar = _variables.playerUI.easeBar.GetComponent<Image>();
        // healthBar = _variables.playerUI.healthBar.GetComponent<Image>();

        // if (!Mathf.Approximately(healthBar.fillAmount, easeBar.fillAmount))
        // {
        //     easeBar.fillAmount = Mathf.Lerp(easeBar.fillAmount, _variables.playerHealth, .2f);
        //     // StartCoroutine(UpdateHealthBar());
        // }

        // easeHealthbar.value = Mathf.Lerp(easeHealthbar.value, health, lerpSpeed);
    }
    // public void EaseBar()
    // {
    //     _variables.playerUI.easeBar.GetComponent<Image>().fillAmount = _variables.playerHealth / 100f;
    // }
    // IEnumerator UpdateHealthBar()
    // {
    //     float duration = _variables.lerpVal;
    //     float elapsed = 0f;
    //     float startFill = easeBar.fillAmount;
    //     float targetFill = _variables.playerHealth;
    //
    //     while (elapsed < duration)
    //     {
    //         elapsed += Time.deltaTime;
    //         easeBar.fillAmount = Mathf.Lerp(startFill, targetFill, elapsed / _variables.lerpVal);
    //         yield return null;
    //     }
    //
    //     easeBar.fillAmount = targetFill;
    // }

    public void DamageParticles()
    {
        if (damageParticlesActive)
        {
            Instantiate(GameAssets.instance.damageParticles, transform.position, Quaternion.identity);
            StartCoroutine(Particles());
        }
    }
    IEnumerator Particles()
    {
        damageParticlesActive = false;
        yield return new WaitForSeconds(.3f);
        damageParticlesActive = true;
    }
    public void DamageIndicator()
    {
        for (int j = 0; j < playerMeshes.Count; j++)
        {
            playerMeshes[j].materials[0].SetInt("_redOn", 1);
        }
        Invoke("RemoveLowHealth", flashTime);
    }
    public void LowHealth()
    {
        for (int j = 0; j < playerMeshes.Count; j++)
        {
            playerMeshes[j].materials[0].SetInt("_redOn", 1);
        }
    }
    public void RemoveLowHealth()
    {
        for (int j = 0; j < playerMeshes.Count; j++)
        {
            playerMeshes[j].materials[0].SetInt("_redOn", 0);
        }
    }
    public void PostDeathSound()
    {
        _variables._currentDeathSound.Post(gameObject);
    }
}