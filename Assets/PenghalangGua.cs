using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenghalangGua : MonoBehaviour
{
    [Header("Kondisi Boss")]
    public BoolValue KonPocong;

    [Header("GO yg nampil")]
    public GameObject pocong;

    void Update()
    {
        if(KonPocong.initialValue == true)
        {
            pocong.SetActive(true);
        }
    }
}
