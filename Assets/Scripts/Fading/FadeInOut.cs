using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    public CanvasGroup canvasgroup;
    public bool fadeIn = false;
    public bool fadeOut = false;

    public float TimeToFade;


    // Update is called once per frame
    void Update()
    {
        if (fadeIn == true)
        {
            if (canvasgroup.alpha < 1)
            {
                canvasgroup.alpha += TimeToFade * Time.deltaTime;
                if (canvasgroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }

        if (fadeOut == true)
        {
            if (canvasgroup.alpha >= 0)
            {
                canvasgroup.alpha -= TimeToFade * Time.deltaTime;
                if (canvasgroup.alpha == 0)
                {
                    fadeOut = false;
                }
            }
        }
    }

    public void FadeIn()
    {
        fadeIn = true;
    }

    public void FadeOut()
    {
        fadeOut = true;
    }
}
