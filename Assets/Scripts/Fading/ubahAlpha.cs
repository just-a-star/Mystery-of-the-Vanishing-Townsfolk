using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ubahAlpha : MonoBehaviour
{
    public CanvasGroup canvasgroup;
    private void Awake()
    {
        canvasgroup = GetComponent<CanvasGroup>();
        if (canvasgroup != null)
        {
            canvasgroup.alpha = 1.0f;
        }
        else
        {
            Debug.LogWarning("CanvasGroup component not found.");
        }
    }
}
