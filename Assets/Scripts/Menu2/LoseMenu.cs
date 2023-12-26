using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseMenu : MonoBehaviour
{
    public static LoseMenu singleton;
    public bool lose;
    public GameObject losePanel;
    public string mainMenu;


    private void Awake()
    {
        singleton = this;
    }


    public void kalah()
    {
        Time.timeScale = 0f;
        lose = true;
        losePanel.SetActive(true);
            
        
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }
}
