using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth singleton;
    public int pHealth;
    public Image[] healthUI;
    public Sprite full;
    public Sprite empty;

    private void Awake()
    {
        singleton = this;
    }
    public void TakeDamage()
    {
            healthUI[pHealth-1].sprite = empty;
    }

    public void GainHeart()
    {
        if(pHealth <= healthUI.Length)
        {
            healthUI[pHealth-1].sprite = full;
        }
    }


    public  void setDarah(int darah)
    {
        pHealth = darah;
    }

    public int GetPlayerHealth()
    {
        return pHealth;
    }
}
