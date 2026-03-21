using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ImageSwapPauseSreen : MonoBehaviour
{
    public GameObject[] images;
    public GameObject Button;
    int i = 0;

    void Start()
    {
      
        UpdateImageVisibility();
    }


    public void SwapImagesForward()
    {


        if (i < images.Length - 1)
        {
            images[0].SetActive(false);
            images[i].SetActive(false);
            i++;

            images[i].SetActive(true);

            //if (i == images.Length)
            //{
            //    i = 0;
            //}

        }

    }

    public void SpapImagesBackward()
    {
        if (i > 0)
        {
            images[i].SetActive(false);
            i--;


            images[i].SetActive(true);

            //if (i == images.Length)
            //{
            //    i = 0;
            //}
        }

        else
        {
            images[i].gameObject.SetActive(false);
            Button.SetActive(false);
        }

        //if (i == 0 )
        //{
        //    images[i].SetActive(false);
        //    Button.SetActive(false);
        //}

    }


    public void LastPicture()
    {
        if (i == 0)
        {
            images[i].SetActive(false);
            Button.SetActive(false);
        }
    }

    private void UpdateImageVisibility()
    {
        for (int j = 0; j < images.Length; j++)
        {
            images[i].gameObject.SetActive(j == i);
        }
        //if (i == 0)
        //{
        //    for (int l = 0; l < images.Length; l++)
        //    {
        //        images[i].gameObject.SetActive(false);
        //    }
        //}
    }


    public void SetFirstActive()
    {

        Button.SetActive(true);
        images[i].SetActive(true);
    }

}
