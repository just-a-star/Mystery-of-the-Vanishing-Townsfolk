using UnityEngine;

public class GambarDarah : MonoBehaviour
{
    public static GambarDarah instance;

    // Menambahkan variabel untuk menyimpan gambar hati penuh dan kosong
    public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;

    [SerializeField] private int playerHealth;  // Nilai aktual darah
    [SerializeField] private float playerMagic; 

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

    public void SetPlayerMagic(float magic)
    {
        playerMagic = magic;
    }

    public float GetPlayerMagic()
    {
        return playerMagic;
    }
}
