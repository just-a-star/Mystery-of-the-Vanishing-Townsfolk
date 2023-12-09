using Cinemachine;
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

    public CinemachineImpulseSource impulseSource;
    [SerializeField] float shakeForce = 1f;


    // Start is called before the first frame update
    void Start()
    {
        gambar = GetComponent<SpriteRenderer>();
        active = storedValue.initialValue;

        if(active)
        {
            ActiveTombol();
        }
    }

    public void ActiveTombol()
    {

        if (active == false)
        {
            impulseSource.GenerateImpulseWithForce(shakeForce);
        }

        active = true;
        storedValue.initialValue = active;
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

}
