﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateMusic : MonoBehaviour
{
    List<AudioSource> music = new List<AudioSource>();

    // Start is called before the first frame update
    public void Start()
    {
        AudioSource[] allAudioSource = GameObject.FindWithTag("gameData").GetComponentsInChildren<AudioSource>();
        music.Add(allAudioSource[0]);

        Slider musicSlider = this.GetComponent<Slider>();

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
            UpdateMusicVolume(musicSlider.value);
        }
        else
        {
            musicSlider.value = 1;
            UpdateMusicVolume(1);
        }
    }
        
    public void UpdateMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("musicVolume", value);
        foreach(AudioSource m in music)
        {
            m.volume = value;
        }
    }
}
