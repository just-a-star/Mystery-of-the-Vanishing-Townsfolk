using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenghalangGaib : MonoBehaviour
{

    [Header("Kondisi Boss")]
    public BoolValue KonKunti;

    public GameObject kunti;


    // Update is called once per frame
    void Update()
    {
        if (KonKunti.initialValue == true)
        {
            kunti.SetActive(true);
        }
        
    }
}
