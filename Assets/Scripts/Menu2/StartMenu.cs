using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public string NewGame;
    public void StartGame()
    {
        SceneManager.LoadScene(NewGame);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
