using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class AudioManager : MonoBehaviour
{  
    public Sound[] sounds;
    public static AudioManager instance;
    public Slider volumeSlider;
    void Awake()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            Load();
        }
        else
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
        }

        #region singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
             Destroy(gameObject);
             return;
        }
        #endregion

        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        { 
            s.source=gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        Play("BackgroundMusic");
    }
    public void Play(string name)
    {
        Sound s=Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Sound" + name +" not found");
            return;
        }
        else
        {
            s.source.Play();
        }
    }
    //FindObjectOfType<AudioManager>().Play("SoundName");

    
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        save();
    }
    public void Load()
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }
    }
    public void save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        PlayerPrefs.Save();
    }
    public void ButtonHover()
    {
        PlayButtonSound("ButtonHover");
    }
    public void StartGameButton()
    {
        PlayButtonSound("PlayButton");

    }
    public void PlayButtonSound(string name)
    {
        Sound s=Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Sound" + name +" not found");
            return;
        }
        else
        {
            s.source.PlayOneShot(s.clip);
        }
    }

}
