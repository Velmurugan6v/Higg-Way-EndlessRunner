using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    [Header("----------Sound Clips----------")]

    public Sound[] musicSounds;
    public Sound[] sfxSounds;

    [Header("----------Audio Source----------")]

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    private void Awake()
    {
        MakeInstatite();
        
        DontDestroyOnLoad(this.gameObject);
    }


    private void Start()
    {
        PlayMusic("Theme");
    }

    public void MakeInstatite()    //Make its single Copy
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }    

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
            print("Sound Not Found");
        else
            musicSource.clip = s.clip;
            musicSource.Play();
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
            print("Sound Not Found");
        else
            sfxSource.PlayOneShot(s.clip);
    }


    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
