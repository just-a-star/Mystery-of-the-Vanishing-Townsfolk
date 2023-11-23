using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tombol : MonoBehaviour
{
    public bool active;
    public BoolValue storedValue;
    public Sprite activeSprite;
    SpriteRenderer gambar;
    public BatuPenutup penutup;

    // Start is called before the first frame update
    void Start()
    {
        gambar = GetComponent<SpriteRenderer>();
        active = storedValue.defaultValue;

        if(active)
        {
            ActiveTombol();
        }
    }

    public void ActiveTombol()
    {
        active = true;
        storedValue.defaultValue = active;
        penutup.Open();
        gambar.sprite = activeSprite;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            ActiveTombol();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
