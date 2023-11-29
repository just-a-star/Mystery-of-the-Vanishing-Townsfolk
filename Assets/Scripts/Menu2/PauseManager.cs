using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static PauseManager singleton;

    public bool isPaused;
    public GameObject pausePanel;
    public Button res;
    public string mainMenu;


    private void Awake()
    {
        singleton = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("pause") && !LoseMenu.singleton.lose)
        {
            ChangesPause();
            /*UIManager.singleton.SetFocusToPausePanel();*/
        }
    }

    public void ChangesPause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;

            res.Select();
            
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }

}
