using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class skipStory : MonoBehaviour
{
    [SerializeField] string LoadKe;


    private void Update()
    
    {
        if (Input.GetButtonDown("interact") && !PauseManager.singleton.pausePanel.activeSelf)
        {
                SceneManager.LoadScene(LoadKe);
            
        }


    }
}
