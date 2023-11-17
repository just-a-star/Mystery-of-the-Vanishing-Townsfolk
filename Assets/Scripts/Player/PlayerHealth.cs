using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth singleton;
    public IntValue pHealth;
    public Image[] healthUI;
    public Sprite full;
    public Sprite empty;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        if (GambarDarah.instance != null)
        {
            // Mengatur nilai darah saat memulai scene
            GambarDarah.instance.SetPlayerHealth(pHealth.initialValue);
            UpdateHealthUI();
        }
        else
        {
            Debug.LogError("GameManager instance not found. Make sure GameManager object is present in the scene.");
        }
    }


    public void TakeDamage()
    {
        if(Hit.Instance.damage == 1)
        {
            GambarDarah.instance.SetPlayerHealth(GambarDarah.instance.GetPlayerHealth() - 1);
            UpdateHealthUI();
        }
            
    }

    public void GainHeart()
    {
        if(pHealth.initialValue <= healthUI.Length)
        {
            GambarDarah.instance.SetPlayerHealth(GambarDarah.instance.GetPlayerHealth() + 1);
            UpdateHealthUI();
        }
    }

    private void UpdateHealthUI()
    {
        int currentHealth = GambarDarah.instance.GetPlayerHealth();

        for (int i = 0; i < healthUI.Length; i++)
        {
            if (i < currentHealth)
            {
                healthUI[i].sprite = GambarDarah.instance.fullHeartSprite;
            }
            else
            {
                healthUI[i].sprite = GambarDarah.instance.emptyHeartSprite;
            }
        }
    }
}
