using System.Collections;
using DG.Tweening;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour, IResetable
{
    public int countdownTimeMax = 3;
    public int countdownTime;
    public TMP_Text countdownText;

    public void StartCountdown()
    {
        StartCoroutine(CountdownToStart());       
    }

    IEnumerator CountdownToStart()
    {
        Time.timeScale = 0f;

        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();

            Sequence popSequence = DOTween.Sequence();
            popSequence.SetUpdate(true)
                .Append(countdownText.transform.DOScale(1.5f, 0.3f).SetEase(Ease.Flash))
                .Append(countdownText.transform.DOScale(1f, 0.3f));

            yield return new WaitForSecondsRealtime(1f);
            countdownTime--;
        }

        countdownText.text = "GO!";
        Sequence goSequence = DOTween.Sequence();
        goSequence.SetUpdate(true)
            .Append(countdownText.transform.DOScale(1.5f, 0.3f).SetEase(Ease.Flash))
            .Append(countdownText.transform.DOScale(1f, 0.3f));

        yield return new WaitForSecondsRealtime(1f);
        
        countdownText.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Reset()
    {
        countdownText.text = "";
        countdownText.gameObject.SetActive(true);
        countdownTime = countdownTimeMax;
    }
}