using UnityEngine;

public class GambarDarah : MonoBehaviour
{
    public static GambarDarah instance;

    // Menambahkan variabel untuk menyimpan gambar hati penuh dan kosong
    public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;

    private int playerHealth;  // Nilai aktual darah

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

    public void SetPlayerHealth(int health)
    {
        playerHealth = health;
    }

    public int GetPlayerHealth()
    {
        return playerHealth;
    }
}
