using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunculNeraka : MonoBehaviour
{

    public BoolValue gendeMati;
    public GameObject neraka;

    // Update is called once per frame
    void Update()
    {
        if(gendeMati.initialValue == true)
        {
            neraka.SetActive(true);
        }
        
    }
}
