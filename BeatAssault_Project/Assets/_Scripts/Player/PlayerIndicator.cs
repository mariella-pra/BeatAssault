using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using UnityEngine.Serialization;

public class PlayerIndicator : MonoBehaviour
{
    private PlayerVariables _variables;
    [SerializeField] GameObject indicator;
    public List<Transform> curIndicators = new List<Transform>();
    public List<Transform> nextIndicators = new List<Transform>();
    private void Awake()
    {
        _variables = transform.parent.gameObject.GetComponent<PlayerVariables>();
        _variables.playerEvents?.AddEvent("PlaysInstrumentIndicator", StartMoving);
        _variables.playerEvents?.AddEvent("Indicator", Move);
        _variables.playerEvents?.AddEvent("IndicatorRemove", Remove);
    }
    private void OnDisable()
    {
        _variables.playerEvents?.RemoveEvent("PlaysInstrumentIndicator", StartMoving);
        _variables.playerEvents?.RemoveEvent("Indicator", Move);
        _variables.playerEvents?.RemoveEvent("IndicatorRemove", Remove);
    }
    public void StartMoving()
    {
        transform.DOLocalMoveY(-15, 5.333333333333333f).SetEase(Ease.Linear);
    }
    
    public void Move()
    {
        //Remove();
        // Invoke("Remove", .2f);
    }

    public void Remove()
    {
        // if(!curIndicators.Contains(nextIndicators[0])) curIndicators.Add(nextIndicators[0]);
        // curIndicators[curIndicators.Count - 1].gameObject.SetActive(true);
        
        
        if(curIndicators.Count > 0 && nextIndicators.Count > 0) curIndicators.Add(nextIndicators[0]);
        
        if(curIndicators.Count > 0) curIndicators[curIndicators.Count - 1].gameObject.SetActive(true);
        
        nextIndicators.RemoveAt(0);
        curIndicators[0].gameObject.SetActive(false);
        curIndicators.RemoveAt(0);
        
        
    }

    // public void RemoveIndicator()
    // {
    //     curIndicators[0].gameObject.SetActive(false);
    //     curIndicators.RemoveAt(0);
    //     
    //     
    // }
    // IEnumerator MoveIndicators()
    // {
    //     yield return new WaitForSeconds(moveDuration);
    //     transform.position = new Vector3(transform.position.x, transform.position.y - 10f, transform.position.z);
    //     
    // }
    //
    // public void UpdateList()
    // {
    //     
    // }
    private void OnEnable()
    {
        
        // // Sammle alle Kinder als RectTransform
        // foreach (Transform child in transform)
        // {
        //     if (child.TryGetComponent(out RectTransform rectTransform))
        //     {
        //         balls.Add(rectTransform);
        //     }
        // }
        //
        // // Setze die Sichtbarkeit der unteren vier Bälle
        // for (int i = 0; i < balls.Count; i++)
        // {
        //     balls[i].gameObject.SetActive(i < visibleBallCount);
        // }

        // Starte die Bewegung
        // StartMovement();
        
        
        // // Initialisiere die Liste der Kinderobjekte
        // foreach (Transform child in transform)
        // {
        //     if (child.TryGetComponent(out RectTransform rectTransform))
        //     {
        //         indicators.Add(rectTransform);
        //     }
        // }
        //
        // // Initiale Sichtbarkeit der Bälle
        // for (int i = 0; i < indicators.Count; i++)
        // {
        //     indicators[i].gameObject.SetActive(i < visibleIndicatorsCount);
        // }
        
        
        for (int i = 0; i < transform.childCount; i++)
        {
            if(i < 4) curIndicators.Add(transform.GetChild(i).gameObject.GetComponent<Transform>());
            else nextIndicators.Add(transform.GetChild(i).gameObject.GetComponent<Transform>());
            // UpdateList();
        }
        // 4 balls get added - works
    }
    
    // private void StartMovement()
    // {
    //     // Bewege alle Bälle nach unten, beginnend mit den unteren vier Bällen
    //     for (int i = 0; i < visibleBallCount; i++)
    //     {
    //         int index = (currentBottomIndex + i) % balls.Count;
    //         RectTransform ball = balls[index];
    //
    //         // Bewege den Ball nach unten mit DOTween (lokale Bewegung)
    //         ball.DOLocalMoveY(ball.localPosition.y - moveDistance, moveDuration)
    //             .SetEase(Ease.Linear)
    //             .OnComplete(() => HandleBallReachedBottom());
    //     }
    // }

    // private void HandleBallReachedBottom()
    // {
    //     // Deaktiviere den untersten Ball, wenn er seine Zielposition erreicht hat
    //     RectTransform bottomBall = balls[currentBottomIndex];
    //     bottomBall.gameObject.SetActive(false);
    //
    //     // Berechne den Index des nächsten Balls, der oben sichtbar wird
    //     int nextIndex = (currentBottomIndex + visibleBallCount) % balls.Count;
    //
    //     // Setze den neuen Ball auf die Startposition oberhalb des höchsten sichtbaren Balles
    //     RectTransform topVisibleBall = balls[(currentBottomIndex + visibleBallCount - 1) % balls.Count];
    //     RectTransform nextBall = balls[nextIndex];
    //     nextBall.localPosition = new Vector2(nextBall.localPosition.x, topVisibleBall.localPosition.y + moveDistance);
    //     nextBall.gameObject.SetActive(true);
    //
    //     // Aktualisiere den Index des untersten Balls
    //     currentBottomIndex = (currentBottomIndex + 1) % balls.Count;
    //
    //     // Starte die Bewegung erneut, wenn der Ball den unteren Bereich erreicht
    //     StartMovement();
    // }
    //
    // private void UpdateVisibility()
    // {
    //     for (int i = 0; i < balls.Count; i++)
    //     {
    //         balls[i].gameObject.SetActive(i < visibleBallCount);
    //     }
    // }


    
    // Diese Methode kann durch ein Event aufgerufen werden
    // Diese Methode wird durch ein Event ausgelöst
    // public void StartBallMovement()
    // {
    //     // Bewege den aktuellen Ball
    //     RectTransform currentBall = indicators[currentTopIndex];
    //     currentBall.gameObject.SetActive(true);
    //
    //     // Bewege den Ball nach unten
    //     currentBall.DOAnchorPosY(currentBall.anchoredPosition.y -10f, moveSpeed)
    //         .SetEase(Ease.Linear)
    //         .OnComplete(HandleBallReachedBottom);
    // }
    // private void HandleBallReachedBottom()
    // {
    //     // Unsichtbar machen, wenn der Ball unten angekommen ist
    //     RectTransform currentBall = indicators[currentTopIndex];
    //     currentBall.gameObject.SetActive(false);
    //
    //     // Berechne den nächsten Index
    //     int newIndex = (currentTopIndex + visibleIndicatorsCount) % indicators.Count;
    //
    //     // Setze den Ball zurück an die Startposition
    //     RectTransform newBall = indicators[newIndex];
    //     newBall.anchoredPosition = new Vector2(newBall.anchoredPosition.x, indicators[currentTopIndex].anchoredPosition.y - (-10f));
    //
    //     // Aktualisiere den Index
    //     currentTopIndex = (currentTopIndex + 1) % indicators.Count;
    // }
    
    
    // public void StartPlayerIndicator()
    // {
    //     MoveIndicators();
    // }
    //
    // public void MoveIndicators()
    // {
    //     moveSpeed = AudioManager.instance.hitMoveTime;
    //     for (int i = 0; i < transform.childCount; i++)
    //     {
    //         transform.GetChild(i).DOLocalMoveY(transform.GetChild(i).localPosition.y -10f, moveSpeed * 4f)
    //             .SetEase(Ease.Linear);
    //     }
    //     Invoke("RemoveIndicator", moveSpeed * 4f);
    // }
    //
    // public void RemoveIndicator()
    // {
    //     // curIndicators.RemoveAt(0);
    //     // Debug.Log(curIndicators.Count);
    //     // AddToQueue();
    // }
    // public void AddToQueue()
    // {
    //     // curIndicators.Add(nextIndicators[0]);
    //     // nextIndicators.RemoveAt(0);
    //     // UpdateList();
    //     // Debug.Log(curIndicators.Count);
    // }
    // public void UpdateList()
    // {
    //     // for (int i = 0; i < nextIndicators.Count; i++)
    //     // {
    //     //     nextIndicators[i].SetActive(false);
    //     // }
    //     // for (int i = 0; i < transform.childCount; i++)
    //     // {
    //     //     if (curIndicators.Contains(transform.GetChild(i).gameObject)) curIndicators[i].SetActive(true);
    //     //     else curIndicators[i].SetActive(false);
    //     // }
    // }
}