using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicManager : MonoBehaviour
{
    public static MagicManager singleton;
    public Image magicSlider;/*
    public float maxMagic = 10;
    public float currentMagic;*/
    public FloatValue pMana;
    private void Awake()
    {
        singleton = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        /*GambarDarah.instance.SetPlayerMagic(pMana.initialValue);
*/
        UpdateMagicUI();
        
    }

    public void AddMagic()
    {

        if(pMana.initialValue < pMana.defaultValue)
        {
            AudioManager.singleton.PlaySound(0);
            pMana.initialValue += 1;
            UpdateMagicUI();
        }
    }

    public void DecreaseMagic()
    {
        pMana.initialValue -= 1;

        if(pMana.initialValue < 0)
        {
            pMana.initialValue = 0;
        }

        UpdateMagicUI();

    }

    void UpdateMagicUI()
    {
        float fillAmount = pMana.initialValue / pMana.defaultValue;
        magicSlider.fillAmount = fillAmount;
    }

}
