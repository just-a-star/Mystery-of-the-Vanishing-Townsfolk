using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Ending : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    bool isPaused = false;
    double pausedTime;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void Update()
    {
        // Menyimpan status pause game
        isPaused = Time.timeScale == 0f;

        // Menjalankan video jika game tidak di-pause
        if (!isPaused)
        {
            videoPlayer.Play();
        }
        else
        {
            videoPlayer.Pause();
        }


    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SaveData.singleton.ResetGame();
        SceneManager.LoadScene("StartMenu");
    }
}
