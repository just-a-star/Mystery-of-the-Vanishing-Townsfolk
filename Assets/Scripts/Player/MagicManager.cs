using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicManager : MonoBehaviour
{
    public static MagicManager singleton;
    public Slider magicSlider;/*
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
        GambarDarah.instance.SetPlayerMagic(pMana.initialValue);
        magicSlider.maxValue = pMana.defaultValue;
        magicSlider.value = GambarDarah.instance.GetPlayerMagic();
        
    }

    public void AddMagic()
    {
        
        if (magicSlider.value > magicSlider.maxValue)
        {
            magicSlider.value = magicSlider.maxValue;
            pMana.initialValue = pMana.defaultValue;
        } else if (pMana.initialValue < pMana.defaultValue)
        {
            magicSlider.value += 1;
            pMana.initialValue += 1;
            GambarDarah.instance.SetPlayerMagic(pMana.initialValue);
        }
    }

    public void DecreaseMagic()
    {
        magicSlider.value -= 1;
        pMana.initialValue -= 1;
        GambarDarah.instance.SetPlayerMagic(pMana.initialValue);

        if (magicSlider.value < 0)
        {
            magicSlider.value = 0;
            pMana.initialValue = 0;
            
        }

    }

}
