using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings2 : MonoBehaviour
{

    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
        }
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);


    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");

        SetMusicVolume();
    }
}
