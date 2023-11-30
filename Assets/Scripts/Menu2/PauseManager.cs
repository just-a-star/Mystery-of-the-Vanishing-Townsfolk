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
    public Button resumeButton;
    public Button quitButton;
    public string mainMenu;

    private void Awake()
    {
        singleton = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        resumeButton.Select(); // Fokus pertama pada tombol resume saat panel pause aktif
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("pause") && !LoseMenu.singleton.lose)
        {
            ChangesPause();
        }

        // Periksa apakah panel pause aktif dan tombol "Enter" ditekan
        if (isPaused && Input.GetKeyDown(KeyCode.Return))
        {
            // Dapatkan tombol yang sedang fokus
            GameObject selectedObject = EventSystem.current.currentSelectedGameObject;

            // Periksa jika tombol yang sedang fokus adalah tombol resume
            if (selectedObject != null && selectedObject.GetComponent<Button>() == resumeButton)
            {
                // Panggil fungsi atau lakukan tindakan yang diinginkan untuk resume
                ResumeGame();
            }
            else if (selectedObject != null && selectedObject.GetComponent<Button>() == quitButton)
            {
                // Panggil fungsi atau lakukan tindakan yang diinginkan untuk keluar ke menu utama
                QuitToMain();
            }
        }
    }

    public void ChangesPause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;

            resumeButton.Select(); // Fokus pada tombol resume saat panel pause aktif
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void ResumeGame()
    {
        // Lakukan sesuatu, misalnya hanya menutup panel pause
        ChangesPause();
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }
}
