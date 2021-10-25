using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSound : MonoBehaviour
{
    List<AudioSource> sfx = new List<AudioSource>();

    // Start is called before the first frame update
    public void Start()
    {
        AudioSource[] allAudioSource = GameObject.FindWithTag("gameData").GetComponentsInChildren<AudioSource>();
        for (int i = 1; i < allAudioSource.Length;i++)
        {
            sfx.Add(allAudioSource[i]);
        }

        Slider sfxSlider = this.GetComponent<Slider>();

        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
            UpdateSoundVolume(sfxSlider.value);
        }
        else
        {
            sfxSlider.value = 1;
            UpdateSoundVolume(1);
        }
    }

    public void UpdateSoundVolume(float value)
    {
        PlayerPrefs.SetFloat("sfxVolume", value);
        foreach (AudioSource s in sfx)
        {
            s.volume = value;
        }
    }
}
