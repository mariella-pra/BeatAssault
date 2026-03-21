using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using AK.Wwise;
public class PlayerRythmIndicator : MonoBehaviour
{
    PlayInstrument _playInstrument;
    public PlayerVariables _variables;
    
    public float growScale = 1.5f;
    public float growDuration;
    public float shrinkDuration = 0.01f;
    public float delay = 0.05f;

    public Vector3 endPos;
    public Vector3 originalScale;

    public Material _normalMat;
    public Material _redMat;
    public float redIndicationDuration = .25f;

    private void Awake()
    {
        originalScale = transform.localScale;
        if(AudioManager.instance is not null) growDuration = AudioManager.instance.hitInterval / 2;
        _variables = transform.parent.transform.parent.gameObject.GetComponent<PlayerVariables>();
    }
    private void OnEnable()
    {
        _variables.playerEvents?.AddEvent("PatternCue", Grow);
    }
    private void OnDisable()
    {
        _variables.playerEvents?.RemoveEvent("PatternCue", Grow);
    }
    public void Grow()
    {
        transform.DOKill();

        transform.DOScale(endPos, growDuration)
            .OnComplete(() =>
            {
                transform.DOScale(originalScale, shrinkDuration);
            })
            .SetEase(Ease.Linear);
        // transform.DOKill();
        //
        // transform.DOScale(growScale, growDuration)
        //     .OnComplete(() =>
        //     {
        //         transform.DOScale(originalScale, shrinkDuration);
        //     })
        //     .SetEase(Ease.Linear);
    }
    public void NoHitFeedback()
    {
        StartCoroutine(RedFeedback());
    }

    IEnumerator RedFeedback()
    {
        GetComponent<Renderer>().material = _redMat;
        yield return new WaitForSeconds(redIndicationDuration);
        GetComponent<Renderer>().material = _normalMat;
    }
}