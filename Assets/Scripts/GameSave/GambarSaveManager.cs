using System.Collections.Generic;
using UnityEngine;

public class GambarDarah : MonoBehaviour
{
    public static GambarDarah instance;

    // Menambahkan variabel untuk menyimpan gambar hati penuh dan kosong
    /*public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;*/
/*
    [SerializeField] private float playerHealth;  // Nilai aktual darah
    [SerializeField] private float playerMagic;*/ 
public List<ScriptableObject> objects = new List<ScriptableObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

   /* public void SetPlayerHealth(float health)
    {
        playerHealth = health;
    }

    public float GetPlayerHealth()
    {
        return playerHealth;
    }

    public void SetPlayerMagic(float magic)
    {
        playerMagic = magic;
    }

    public float GetPlayerMagic()
    {
        return playerMagic;
    }*/
}
