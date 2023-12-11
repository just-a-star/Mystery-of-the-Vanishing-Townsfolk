using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TlGaib : MonoBehaviour
{
    public PlayableDirector pd;

    public BoolValue blm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (blm.initialValue == false)
            {
                mulai();
            }
        }
    }

    void mulai()
    {
        pd.Play();
        blm.initialValue = true;
    }
}
