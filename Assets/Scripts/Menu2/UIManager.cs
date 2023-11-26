using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager singleton;
    public GameObject pausePanel;
    public GameObject losePanel;
    public string mainMenu;

    private EventSystem eventSystem;

    private void Awake()
    {
        singleton = this;
        eventSystem = FindObjectOfType<EventSystem>();
    }

    void Start()
    {
        eventSystem.SetSelectedGameObject(null);
    }

    public void SetFocusToPausePanel()
    {
        eventSystem.SetSelectedGameObject(pausePanel);
    }

    public void SetFocusToLosePanel()
    {
        eventSystem.SetSelectedGameObject(losePanel);
    }

}
