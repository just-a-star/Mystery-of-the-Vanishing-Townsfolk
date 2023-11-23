using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [Header("Scene To Load")]
    public string NewGame;
    public StringValue LoadGame;

    [Header("Notifikasi")]
    public GameObject notif;
    [SerializeField] float waktuNotif;
    public void StartGame()
    {
        SaveData.singleton.ResetGame();
        SceneManager.LoadScene(NewGame);
    }

    public void loadGame()
    {
        SaveData.singleton.LoadGame();
        if (SaveData.singleton.AdaFile == true)
        {
            SceneManager.LoadScene(LoadGame.initialValue);
        } else
        {
            StartCoroutine(notifCo());
        }
        
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator notifCo()
    {
        notif.gameObject.SetActive(true);
        yield return new WaitForSeconds(waktuNotif);
        notif.gameObject.SetActive(false);
    }
}
