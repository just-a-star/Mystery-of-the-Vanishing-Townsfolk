using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCerita : MonoBehaviour
{
    
    void OnDisable()
    {
        SceneManager.LoadScene("Intro", LoadSceneMode.Single);
    }

}
