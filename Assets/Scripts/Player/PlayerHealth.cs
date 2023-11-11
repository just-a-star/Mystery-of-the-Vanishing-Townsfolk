using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth singleton;
    public int pHealth;
    public GameObject[] healthUI;

    private void Awake()
    {
        singleton = this;
    }
    public void TakeDamage()
    {
            healthUI[pHealth-1].SetActive(false);
    }

    public void GainHeart()
    {
        if(pHealth <= healthUI.Length)
        {
            healthUI[pHealth-1].SetActive(true);
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
