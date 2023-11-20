using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicUp : MonoBehaviour
{
    public FloatValue mana;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collision.isTrigger)
        {
            if(mana.initialValue < mana.defaultValue) { 
            MagicManager.singleton.AddMagic();
            Destroy(this.gameObject);
            }
        }
    }
}
