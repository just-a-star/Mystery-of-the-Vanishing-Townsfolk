using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicUp : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MagicManager.singleton.AddMagic();
            Destroy(this.gameObject);
        }
    }
}
