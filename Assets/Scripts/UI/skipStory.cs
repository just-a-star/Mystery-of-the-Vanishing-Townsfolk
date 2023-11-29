using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class skipStory : MonoBehaviour
{
    [SerializeField] string LoadKe;


    private void Update()
    
    {
        if (Input.GetButtonDown("Submit") &&  !PauseManager.singleton.pausePanel.activeSelf)
        {
                Debug.Log("pause gk");
                SceneManager.LoadScene(LoadKe);
            
        }


    }
}
