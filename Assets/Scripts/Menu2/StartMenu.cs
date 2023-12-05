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
        AudioManager.singleton.PlaySound(16);
        SaveData.singleton.ResetGame();
        SceneManager.LoadScene(NewGame);

    }

    public void loadGame()
    {
        SaveData.singleton.LoadGame();
        if (SaveData.singleton.AdaFile == true)
        {
            AudioManager.singleton.PlaySound(16);
            SceneManager.LoadScene(LoadGame.initialValue);
        } else
        {
            AudioManager.singleton.PlaySound(16);
            StartCoroutine(notifCo());
        }
        
    }

    public void Quit()
    {
        AudioManager.singleton.PlaySound(16);
        Application.Quit();
    }

    IEnumerator notifCo()
    {
        notif.gameObject.SetActive(true);
        yield return new WaitForSeconds(waktuNotif);
        notif.gameObject.SetActive(false);
    }
}
