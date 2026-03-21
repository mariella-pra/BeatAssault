using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEffects : MonoBehaviour
{
    public void OnHover()
    {
        AudioManager.instance.buttonHoverSfx.Post(AudioManager.instance.gameObject);   
    }

    public void OnClick()
    {
        AudioManager.instance.buttonClickSfx.Post(AudioManager.instance.gameObject);   
    }
}
