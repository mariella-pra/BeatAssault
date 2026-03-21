using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro _text;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float dieSpeed = 3f;
    [SerializeField] float dieTime = .5f;
    [SerializeField] float scaleAmount = .8f;
    private Color _txtColor;
    private Vector3 moveVector;
    private int sortingOrder;
    private Camera mainCamera;
    private Vector3 origianlScale;
    private void Awake()
    {
        _text = transform.GetComponent<TextMeshPro>();
        mainCamera = Camera.main;
    }

    public void Setup(int damageAmount)
    {
        Scale();
        _text.SetText(damageAmount.ToString());
        _txtColor = _text.color;
        moveVector = new Vector3(0, 2) * 10f;
        _text.sortingOrder++;
        origianlScale = transform.localScale;
    }

    private void Update()
    {
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
            mainCamera.transform.rotation * Vector3.up);
        
        // transform.position += new Vector3(0, moveSpeed) * Time.deltaTime;
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;
        
        
        Invoke("Die", dieTime);

        
    }

    public void Scale()
    {
        Sequence scaleSequence = DOTween.Sequence();
        scaleSequence.Append(transform.DOScale(scaleAmount, dieTime).SetEase(Ease.OutQuad))
                     .Append(transform.DOScale(origianlScale, dieSpeed).SetEase(Ease.InQuad));
    }

    public void Die()
    {
        _txtColor.a -= dieSpeed * Time.deltaTime;
        _text.color = _txtColor;
        if(_txtColor.a <= 0f)
            Destroy(gameObject);
        
        // if(_txtColor.a <= .5f)
        // {
        //     transform.localScale += Vector3.one * (scaleAmount * Time.deltaTime);
        // }
        // else if(_txtColor.a > .5f)
        // {
        //     transform.localScale -= Vector3.one * (scaleAmount * Time.deltaTime);
        // }
    }
}
