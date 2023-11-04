using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int pHealth;
    public GameObject[] healthUI;
    // Start is called before the first frame update
    
    void TakeDamage()
    {
        pHealth--;
        if(pHealth <= 0 )
        {
            pHealth = 0;
        }
        healthUI[pHealth].SetActive( false );
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            TakeDamage();
        }
    }
}
