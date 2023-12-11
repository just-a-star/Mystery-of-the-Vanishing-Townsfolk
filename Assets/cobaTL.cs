using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class cobaTL : MonoBehaviour
{

    public PlayableDirector pd;

    [SerializeField] bool blm = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(blm == true)
            {
                mulai();
            }
        }
    }

    void mulai()
    {
        pd.Play();
        blm = false;
    }
}
