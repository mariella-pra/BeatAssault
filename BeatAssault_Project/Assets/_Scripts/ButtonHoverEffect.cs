using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{

    public Image background;

    void Start()
    {
       

        if (background != null)
        {
            background.enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        

        if (background != null)
        {
            background.enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        if (background != null)
        {
            background.enabled = false;
        }
    }
}
