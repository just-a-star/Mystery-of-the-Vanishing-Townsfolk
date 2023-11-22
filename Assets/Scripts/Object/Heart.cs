using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    /*public PlayerMove darahPlayer;*/

    public static Heart singleton;

    public FloatValue heart;


    private void Awake()
    {
        singleton = this;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player") && !collision.isTrigger)
        {
            if (heart.initialValue < heart.defaultValue)
            {
                heart.initialValue++;

                
                PlayerHealth.singleton.GainHeart();

                Destroy(this.gameObject);
                
            }
        }
        
        

        /*if(darahPlayer.darah < 5)
        {
            darahPlayer.darah += 1;
        }*/
        
    }
}
