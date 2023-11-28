using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenghalangGaib : MonoBehaviour
{

    [Header("Kondisi Boss")]
    public BoolValue KonPocong;
    public BoolValue KonKunti;

    [Header("GO yg nampil")]
    public GameObject pocong;
    public GameObject kunti;


    // Update is called once per frame
    void Update()
    {
        if(KonPocong.initialValue == true)
        {
            pocong.SetActive(true);
        } else if (KonKunti.initialValue == true)
        {
            kunti.SetActive(true);
        }
        
    }
}
