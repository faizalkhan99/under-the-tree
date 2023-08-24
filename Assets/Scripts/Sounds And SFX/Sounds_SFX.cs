using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Sounds_SFX : MonoBehaviour
{
    private static Sounds_SFX _instance;
    public static Sounds_SFX Instance 
    {
        get 
            { 
                if(_instance == null) Debug.LogError("Sound Instance NULL Error");
                return _instance;
            }    
    }
    public AudioSource CatchSFX;
    public AudioSource ShootSFX;
    public AudioSource BGM;
    public AudioSource rottenFruitDestroyClip;
    public AudioSource TeleportFromSideSFX;
    public void Awake()
    {
        _instance = this;
    }
    public void PlayCatchSFX(AudioClip audio)
    {
        CatchSFX.clip = audio;
        CatchSFX.Play();
    }
    public void PlayShootSFX(AudioClip audio)
    {
        ShootSFX.clip = audio;
        ShootSFX.Play();
    }
    public void PlayTeleportFromSideSFX()
    {
        TeleportFromSideSFX.Play();
    }
    public void PlayGameOverMusic(AudioClip audio)
    {
        BGM.clip = audio;
        BGM.PlayDelayed(0.5f);
    }
    public void PlayRottenFruitDestroyMusic(AudioClip audio)
    {
        rottenFruitDestroyClip.clip = audio;
        rottenFruitDestroyClip.Play();
    }
    public void PauseBGM()
    {
        BGM.Pause();
    }
    public void UnPauseBGM()
    {
        BGM.UnPause();
    }
}