using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth singleton;
    public FloatValue pHealth;
    public Image healthUI;
    public GameObject player;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        if (GambarDarah.instance != null)
        {
            // Mengatur nilai darah saat memulai scene
            /*GambarDarah.instance.SetPlayerHealth(pHealth.initialValue);*/
            UpdateHealthUI();
        }
        else
        {
            Debug.LogError("GameManager instance not found. Make sure GameManager object is present in the scene.");
        }
    }


    public void TakeDamage(float damage)
    {
        pHealth.initialValue -= damage;
        AudioManager.singleton.PlaySound(8);

        if (pHealth.initialValue <= 0 )
        {
            
            pHealth.initialValue = 0;
            player.SetActive(false);
            LoseMenu.singleton.kalah();
            AudioManager.singleton.PlaySound(9);

        }

            UpdateHealthUI();
            
    }

    public void GainHeart()
    {
        if(pHealth.initialValue <= pHealth.defaultValue)
        {
            pHealth.initialValue += 1;
            UpdateHealthUI();
            AudioManager.singleton.PlaySound(0);
        }
    }

    private void UpdateHealthUI()
    {
        
        float fillAmount = pHealth.initialValue / pHealth.defaultValue;
        healthUI.fillAmount = fillAmount;
    }
}
