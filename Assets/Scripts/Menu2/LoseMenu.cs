using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseMenu : MonoBehaviour
{
    public static LoseMenu singleton;
    public bool lose;
    public GameObject losePanel;
    public Button tombol;
    public string mainMenu;


    private void Awake()
    {
        singleton = this;
    }


    public void kalah()
    {
        lose = true;
        losePanel.SetActive(true);
        tombol.Select();
            
            Time.timeScale = 0f;
        
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }
}
