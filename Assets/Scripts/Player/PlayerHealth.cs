using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static int pHealth;
    public GameObject[] healthUI;
    // Start is called before the first frame update

    public void TakeDamage()
    {
        Debug.Log("ini adalah pHealth awal : " + pHealth);
            
        
            pHealth --;
            healthUI[pHealth].SetActive(false);
        
        
        Debug.Log("ini adalah pHealth akhir : " + pHealth);
    }


    public static  void setDarah(int darah)
    {
        pHealth = darah;
    }

    public static int GetPlayerHealth()
    {
        return pHealth;
    }
}
