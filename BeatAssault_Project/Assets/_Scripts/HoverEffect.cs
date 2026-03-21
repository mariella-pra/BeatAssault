using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
    
{
    private TextMeshProUGUI text;
    private Vector3 originalScale;
    public float scaleFactor = 1.2f;
    public float animationTime = 0.2f;

   

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        originalScale = transform.localScale;

       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, originalScale * scaleFactor, animationTime).setEaseOutQuad();
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, originalScale, animationTime).setEaseOutQuad();
    }
}
