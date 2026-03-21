using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum CurInstrument
{
    None,
    HasViolin,
    HasSnare,
    HasSaxophone,
    HasPiano,
    HasKick,
    HasBass
}
public class PlayInstrument : MonoBehaviour
{
    //REFERENCES
    public CurInstrument curInstrumentEnum;
    private PlayerVariables _variables;
    private AudioManager _audioManager;
    private PlayerController player;
    private AbilityManager _abilityManager;
    
    
    private uint patternIndicator;
    private void Awake()
    {
        player = gameObject.GetComponent<PlayerController>();
        _abilityManager = FindObjectOfType<AbilityManager>();
        _variables = gameObject.GetComponent<PlayerVariables>();
        _audioManager = FindObjectOfType<AudioManager>();
       
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level1")
        {
            player = gameObject.GetComponent<PlayerController>();
            _abilityManager = FindObjectOfType<AbilityManager>();
            _variables = gameObject.GetComponent<PlayerVariables>();
            _audioManager = FindObjectOfType<AudioManager>();
        }
    }

    private void OnEnable()
    {
        _variables.playerEvents?.AddEvent("LooseInstrument", LooseInstrument);
        _variables.playerEvents?.AddEvent("PickupInstrument", OnPickupAbility);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDestroy()
    {
        _variables.playerEvents?.RemoveEvent("LooseInstrument", LooseInstrument);
        _variables.playerEvents?.RemoveEvent("PickupInstrument", OnPickupAbility);
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void HasInstrument()
    {
        
        if (_variables._hasInstrument && !_variables._playsInstrument && !_variables.dead)
        {
            _variables._playsInstrument = true;
            _variables.playerEvents.PublishEvent("PlayInstrument");
            print("Blai: " + Time.time);
            StartCoroutine(SpawnPatternRoutine());
        }
    }
    public void SpawnPattern()
    {
        if (_variables._curPattern is not null)
        {
            _variables.round = 0;
            patternIndicator = AkSoundEngine.PostEvent(_variables._curPattern.ToString(), gameObject,
                (uint)AkCallbackType.AK_MusicSyncUserCue, CheckInput,
                this);
        }
    }
    IEnumerator SpawnPatternRoutine()
    {
        yield return new WaitForSeconds(1 - (AudioManager.instance.hitInterval / 2));
        print("Blai: " + Time.time);

        
        if (_variables._curPattern is not null)
        {
            _variables.round = 0;
            patternIndicator = AkSoundEngine.PostEvent(_variables._curPattern.ToString(), gameObject,
                (uint)AkCallbackType.AK_MusicSyncUserCue, CheckInput,
                this);
        }
        yield return new WaitForSeconds(AudioManager.instance.hitInterval / 2);
        _variables.playerEvents?.PublishEvent("PlaysInstrumentIndicator");
        if (_variables._curPattern == AudioManager.instance._pianoPattern && _variables._curPattern is not null)
        {
            AudioManager.instance._pianoPatternAudible.Post(gameObject);
        }
        //both work to this point - ich liebe dich ella du bist so toll
    }
    void CheckInput(object in_cookie, AkCallbackType in_type, AkCallbackInfo info)
    {
        if (in_type == AkCallbackType.AK_MusicSyncUserCue)
        {
            var markerInfo = info as AkMusicSyncCallbackInfo;
            if (markerInfo is not null && _variables is not null)
            {
                if(markerInfo.userCueName is not "PatternEnd") _variables.playerEvents?.PublishEvent("PatternCue");
                if(_variables._curPattern != null) AkSoundEngine.SetSwitch(_variables._curPattern.ToString(), "_" + markerInfo.userCueName, gameObject);
                if(_variables._hasInstrument & _variables._playsInstrument)StartCoroutine(HitInterval());
                
                CheckInstrumentAbilities();

                if (markerInfo.userCueName == "BigHit" && _variables._hasBigHitPattern) _variables.selectedAbilityIndex += 1;
                
                if (markerInfo.userCueName == "PatternEnd")
                {
                    _variables._pianoWallIndicatorActive = false;
                    _variables.playerEvents.PublishEvent("LooseInstrument");
                }
            }
        }
    }

    public void CheckInstrumentAbilities()
    {
        if (_variables._playsPiano)
        {
            if (!_variables._pianoWallIndicatorActive) _variables.selectedAbilityIndex = 5;
            if (_variables._pianoWallIndicatorActive) _variables.selectedAbilityIndex = 6;
        }
       
    }
    public void LooseInstrument()
    {
        //destroy all balls
        AkSoundEngine.PostEvent(_variables._stopCurPattern.ToString(), gameObject);
        _variables._hasInstrument = false;
        _variables._playsInstrument = false;
        ChangeEnum(Instruments.None);
        _variables.playerUI.ChangePatternUI();
        _variables.round = 0;
        _variables._playsPiano = false;
        _variables._currentPianoWall = null;
    }
    IEnumerator HitInterval()
    {
        _variables._hitIntervalActive = true;
        _variables.allowedHits = 1;
        yield return new WaitForSeconds(AudioManager.instance.hitInterval);
        _variables._hitIntervalActive = false;
        _variables.allowedHits = 0;
    }

    public void ResetInstrument()
    {
        ChangeEnum(Instruments.None);
    }
    public void ChangeEnum(Enum @enum)
    {
        switch (@enum)
        {
            case Instruments.None:
                curInstrumentEnum = CurInstrument.None;
                break;
            case Instruments.Violin:
                curInstrumentEnum = CurInstrument.HasViolin;
                break;
            case Instruments.Snare:
                curInstrumentEnum = CurInstrument.HasSnare;
                break;
            case Instruments.Saxophone:
                curInstrumentEnum = CurInstrument.HasSaxophone;
                break;
            case Instruments.Piano:
                curInstrumentEnum = CurInstrument.HasPiano;
                break;
            case Instruments.Kick:
                curInstrumentEnum = CurInstrument.HasKick;
                break;
            case Instruments.Bass:
                curInstrumentEnum = CurInstrument.HasBass;
                break;
        }

        switch (curInstrumentEnum)
        {
            case CurInstrument.HasViolin:
                _variables._curPattern = _audioManager._violinPattern;
                _variables._stopCurPattern= _audioManager._stopViolinPattern;
                _variables._currentDeathSound = _audioManager._violinDeathSound;
                break;
            case CurInstrument.HasSaxophone:
                _variables._hasBigHitPattern = true;
                _variables._curPattern = _audioManager._saxophonePattern;
                _variables._stopCurPattern= _audioManager._stopSaxophonePattern;
                _variables._currentDeathSound = _audioManager._saxDeathSound;
                break;
            case CurInstrument.HasSnare:
                _variables._curPattern = _audioManager._snarePattern;
                _variables._stopCurPattern= _audioManager._stopSnarePattern;
                _variables._currentDeathSound = _audioManager._snareDeathSound;
                break;
            case CurInstrument.HasPiano:
                // _variables._hasBigHitPattern = true;
                _variables._curPattern = _audioManager._pianoPattern;
                _variables._stopCurPattern= _audioManager._stopPianoPattern;
                _variables._currentDeathSound = _audioManager._pianoDeathSound;
                _variables._playsPiano = true;
                break;
            case CurInstrument.HasKick:
                // _variables._hasBigHitPattern = true;
                _variables._curPattern = _audioManager._kickPattern;
                _variables._stopCurPattern= _audioManager._stopKickPattern;
                _variables._currentDeathSound = _audioManager._kickDeathSound;
                break;
            case CurInstrument.HasBass:
                _variables._curPattern = _audioManager._bassPattern;
                _variables._stopCurPattern= _audioManager._stopBassPattern;
                _variables._currentDeathSound = _audioManager._bassDeathSound;
                break;
            case CurInstrument.None:
                _variables._hasBigHitPattern = false;
                _variables._curPattern = null;
                _variables.selectedAbilityIndex = 0;
                _variables._currentDeathSound = _audioManager._deathSound;
                break;
        }
    }
    public void UseAbility(int abilityIndex, PlayerController _player)
    {
        if (abilityIndex >= _abilityManager.AbilitiesList.Count) return;
        var ability = _abilityManager.AbilitiesList[abilityIndex];
        ability.Use(gameObject, _player, 0);
    }

    public void OnPickupAbility()
    {
        if (curInstrumentEnum == CurInstrument.HasPiano)
        {
            UseAbility(_variables.selectedAbilityIndex, player);
        }
    }
    public void OnPlayInstrument(InputAction.CallbackContext context)
    {
        if (_variables._hasInstrument & context.started)
        {
            if (_variables._hitIntervalActive && _variables.allowedHits > 0)
            {
                if(_variables.allowedHits != 0) _variables.allowedHits--;
                PlaySound();
                _variables.misses = false;
                UseAbility(_variables.selectedAbilityIndex, player);
            }
            else
            {
                _variables.misses = true;
                _variables.playerEvents.PublishEvent("PlayerMisses");
                // gameObject.GetComponent<PlayerHealthControl>().DamagePlayer(5);
            }
        }
    }
    public void PlaySound()
    {
        //UNCLEAN
        switch (curInstrumentEnum)
        {
            case CurInstrument.HasViolin:
                AudioManager.instance._violinSound.Post(gameObject);
                break;
            case CurInstrument.HasSnare:
                AudioManager.instance._snareSound.Post(gameObject);
                break;
            case CurInstrument.HasSaxophone:
                AudioManager.instance._saxophoneSound.Post(gameObject);
                break;
            case CurInstrument.HasPiano:
                AudioManager.instance._pianoSound.Post(gameObject);
                break;
            case CurInstrument.HasKick:
                AudioManager.instance._kickSound.Post(gameObject);
                break;
            case CurInstrument.HasBass:
                AudioManager.instance._bassSound.Post(gameObject);
                break;
        }
    }
}
