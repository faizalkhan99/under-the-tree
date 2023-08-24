using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_Sounds : MonoBehaviour
{
    public AudioSource GUI_SFX_AudioSource;
    public AudioClip GUI_SFX;
    public void Pay_GUI_SFX()
    {
        GUI_SFX_AudioSource.clip = GUI_SFX;
        GUI_SFX_AudioSource.Play();
    }
}
