using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipIntro : MonoBehaviour
{
    [SerializeField] string LoadKe;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("interact") && !PauseManager.singleton.pausePanel.activeSelf)
        {
            SceneManager.LoadScene(LoadKe);
        }
    }
}
